using asp_net_db.Data;
using asp_net_db.Models;
using asp_net_db.Models.Dto;
using asp_net_db.Utilities;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace asp_net_db.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrackerController : ControllerBase
    {
        private readonly ApplicationContext _context;
        private readonly IMapper _mapper;

        public TrackerController(ApplicationContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        /// <summary>
        /// Успеваемость (статистика) всех курсов преподавателя
        /// </summary>
        [HttpGet("AllTeacherStats")]
        public async Task<IActionResult> AddTrack(int id, string token)
        {
            var courses = await _context.Courses.Include(x => x.Lessons).Where(x => x.OwnerId == id).ToListAsync();

            Dictionary<string, AllTeacherStatsCourcesModel> result = new Dictionary<string, AllTeacherStatsCourcesModel>();

            foreach(var course in courses)
            {
                foreach(var lesson in course.Lessons)
                {
                    var trackers = await _context.Trackers.Where(t => t.LessonId ==  lesson.Id).ToListAsync();
                    var marks = new List<double>();
                    
                    foreach(var track in trackers)
                    {
                        marks.Add(track.ScoreOf5);
                    }

                    var lessonStat = new AllTeacherStatsCourcesModel(marks);
                    result.Add(lesson.Title, lessonStat);
                }
            }

            return Ok(result);
        }

        /// <summary>
        /// Успеваемость курса
        /// </summary>
        [HttpGet("TeacherCourseStats")]
        public async Task<IActionResult> CursesStats(int courseId, string token)
        {
            var course = await _context.Courses.Include(x => x.Lessons).FirstOrDefaultAsync(x => x.Id == courseId);

            Dictionary<int, List<double>> result = new Dictionary<int, List<double>>();
            // id ученика и его оценки
            
            foreach(var lesson in course.Lessons)
            {
                var lessonId = lesson.Id;
                var marks = _context.Trackers.Where(x => x.LessonId == lesson.Id).ToList();

                // оценки
                foreach(var mark in marks)
                {
                    var studentId = mark.Id;

                    if (result.ContainsKey(studentId)){
                        result[studentId].Add(mark.ScoreOf5);
                    }
                    else
                    {
                        result.Add(studentId, new List<double>());
                        result[studentId].Add(mark.ScoreOf5);
                    }
                }
            }

            return Ok(result);
        }

        /// <summary>
        /// Успеваемость (статистика) всех курсов всех преподавателей
        /// </summary>
        [HttpGet("AllTeachersStats")]
        public async Task<IActionResult> AllTeachersStats(string token)
        {
            var result = new List<AllTeachersAllCourses>();
            var courses = await _context.Courses.Include(x => x.Lessons).ToListAsync();

            foreach (var course in courses)
            {
                foreach (var lesson in course.Lessons)
                {
                    var trackers = await _context.Trackers.Where(t => t.LessonId == lesson.Id).ToListAsync();
                    var marks = new List<double>();

                    foreach (var track in trackers)
                    {
                        marks.Add(track.ScoreOf5);
                    }

                    var lessonStat = new AllTeacherStatsCourcesModel(marks);
                    result.Add(new AllTeachersAllCourses(course.Id, lesson.Title, lessonStat));
                }
            }

            return Ok(result);
        }

        /// <summary>
        /// Статистика преподавателей
        /// </summary>
        [HttpGet("TeachersStats")]
        public async Task<IActionResult> All2TeachersStats(string token)
        {
            var result = new List<TeachersStats>();

            var teachersCourses = new Dictionary<int, List<Course>>();

            // разбор учителей
            foreach(var course in _context.Courses.Include(x => x.Lessons).Include(x => x.Contents).Include(x => x.Homeworks).ToList())
            {
                var ownerId = course.Id;
                if (teachersCourses.ContainsKey(ownerId))
                {
                    teachersCourses[ownerId].Add(course);
                }
                else
                {
                    teachersCourses.Add(ownerId, new List<Course>());
                    teachersCourses[ownerId].Add(course);
                }
            }

            //foreach (var teacher in teachersCourses.Values)
            //{
                
            //    foreach(var courses in _context.Courses.Where(x => x.OwnerId == teacher).ToList())
            //}


            return Ok(teachersCourses);
        }

        /// <summary>
        /// Прогресс школьника в курсе
        /// </summary>
        [HttpGet("courseProgress")]
        public async Task<IActionResult> CourseProgress(int courseId, int userId)
        {
            var course = await _context.Courses.Include(x => x.Lessons).FirstOrDefaultAsync(x => x.Id == courseId);
            var lessonsIds = new List<int>();

            foreach (var lesson in course.Lessons)
            {
                lessonsIds.Add(lesson.Id);
            }


            if (course == null)
            {
                return NotFound("Такого курса не было найдено");
            }

            var allCount = course.Lessons.Count();
            var completedCount = await _context.Trackers.Where(x => x.UserId == userId && lessonsIds.Contains(x.LessonId)).CountAsync();

            return Ok(new CourseProgressModel(allCount, completedCount)); 
        }


        /// <summary>
        /// Добавить новый результат теста
        /// </summary>
        [HttpPost("NewTrack")]
        public async Task<IActionResult> AddTrack(TrackerDto dto, string token)
        {
            if (!TokenUtility.ValidateToken(token)) return StatusCode(401);


            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors)
                                      .Select(e => e.ErrorMessage)
                                      .ToList();

                return BadRequest("ошибка валидации");
            }

            var tracker = _mapper.Map<Tracker>(dto);

            // проверка на то есть ли доступ у ученика к лекции
            var course = _context.Courses.FirstOrDefault(c => c.Lessons.Any(l => l.Id == tracker.LessonId));

            if (course == null)
            {
                return NotFound("Курс не найден");
            }

            var result = course.StudentsIds.Contains(tracker.UserId);

            if (!result)
            {
                return BadRequest("Ученик не имеет доступ к курсу");
            }

            var lesson = await _context.Lessons.FirstOrDefaultAsync(l => l.Id == tracker.LessonId);

            // проверка нет ли такой оценки уже
            var findedTracker = _context.Trackers.FirstOrDefault(x => x.UserId == tracker.UserId && x.LessonId == tracker.LessonId);
            if (findedTracker != null)
            {
                if (lesson.canRetry)
                {
                    findedTracker.ScoreOf100 = tracker.ScoreOf100;
                    findedTracker.ScoreOf5 = (5 * tracker.ScoreOf100) / 100;
                    findedTracker.RetryCount++;
                    _context.SaveChanges();
                    return Ok();
                }

                return BadRequest();
            }

            tracker.ScoreOf5 = (5 * tracker.ScoreOf100) / 100;
            _context.Trackers.Add(tracker);
            _context.SaveChanges();

            return Ok();
        }

        /// <summary>
        /// Получить все уроки для админки
        /// </summary>
        [HttpGet("allLessonsStats")]
        public async Task<IActionResult> AllLessonsStats(string token)
        {
            var result = await _context.Lessons.ToListAsync();

            return Ok(result);
        }
    }
}
