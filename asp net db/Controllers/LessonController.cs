using asp_net_db.Data;
using asp_net_db.Models;
using asp_net_db.Models.Dto;
using asp_net_db.Utilities;
using AutoMapper;
using AutoMapper.Configuration.Conventions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;

namespace asp_net_db.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LessonController : ControllerBase
    {
        private readonly ApplicationContext _context;
        private readonly IMapper _mapper;

        public LessonController(ApplicationContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        /// <summary>
        /// Получить занятие
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetLesson(int id, string token)
        {
            if (!TokenUtility.ValidateToken(token)) return StatusCode(401);

            var lesson = await _context.Lessons.FirstOrDefaultAsync(x => x.Id == id);

            if (lesson == null)
            {
                return NotFound();
            }

            lesson.Script = "дозывайте отдельно";

            return Ok(lesson);
        }

        /// <summary>
        /// Может ли пройти курс юзер
        /// </summary>
        [HttpGet("canEnter")]
        public async Task<IActionResult> CanEnter(int userId, int lessonId, string token)
        {
            if (!TokenUtility.ValidateToken(token)) return StatusCode(401);

            var lesson =  await _context.Lessons.FirstOrDefaultAsync(l => l.Id == lessonId);
            var course = _context.Courses.FirstOrDefault(c => c.Lessons.Any(l => l.Id == lessonId));

            if (course == null)
            {
                return NotFound();
            }

            if (!lesson.canRetry || (_context.Trackers.FirstOrDefault(x => x.UserId == userId && x.LessonId == lessonId)) != null)
            {
                return StatusCode(403);
            } 

            var result = course.StudentsIds.Contains(userId);

            return (result) ? Ok() : StatusCode(403);
        }

        /// <summary>
        /// Получить сценарий занятия
        /// </summary>
        [HttpGet("script")]
        public async Task<IActionResult> GetScript(int id, string token)
        {
            if (!TokenUtility.ValidateToken(token)) return StatusCode(401);

            var lesson = await _context.Lessons.FirstOrDefaultAsync(x => x.Id == id);

            if (lesson == null)
            {
                return NotFound();
            }

            return Ok(lesson.Script);
        }

        /// <summary>
        /// Изменить сценарий занятия
        /// </summary>
        [HttpPut("script")]
        public async Task<IActionResult> EditScript(int id, string script, string token)
        {
            if (!TokenUtility.ValidateToken(token)) return StatusCode(401);

            var lesson = await _context.Lessons.FirstOrDefaultAsync(x => x.Id == id);

            if (lesson == null)
            {
                return NotFound();
            }

            lesson.Script = script;
            _context.SaveChanges();

            return Ok(lesson.Script);
        }


        /// <summary>
        /// Создать занятие
        /// </summary>
        [HttpPost("Create")]
        public async Task<IActionResult> AddLesson([FromBody] LessonDto lesson, int courseId, string token)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors)
                                      .Select(e => e.ErrorMessage)
                                      .ToList();

                return BadRequest(errors);
            }

            if (!TokenUtility.ValidateToken(token)) return StatusCode(401);

            var course = await _context.Courses.FirstOrDefaultAsync(x => x.Id == courseId);
            if (course == null)
            {
                return NotFound();
            }

            var mapped = _mapper.Map<Lesson>(lesson);

            var addedLesson = _context.Lessons.Add(mapped);
            course.Lessons.Add(mapped);

            _context.SaveChanges();

            return Ok(mapped);
        }

        /// <summary>
        /// Удалить (мягко) занятие
        /// </summary>
        [HttpDelete]
        public async Task<IActionResult> DeleteLesson(int id, string token)
        {
            if (!TokenUtility.ValidateToken(token)) return StatusCode(401);

            var lesson = await _context.Lessons.FirstOrDefaultAsync(x => x.Id == id);
            lesson.isDeleted = true;

            _context.SaveChanges();
            return Ok();
        }
    }
}
