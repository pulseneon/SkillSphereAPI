using asp_net_db.Data;
using asp_net_db.Models;
using asp_net_db.Models.Dto;
using asp_net_db.Utilities;
using AutoMapper;
using Humanizer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace asp_net_db.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeworkController : ControllerBase
    {
        private readonly ApplicationContext _context;
        private readonly IMapper _mapper;

        public HomeworkController(ApplicationContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        /// <summary>
        /// Получить дз по ид
        /// </summary>
        [HttpGet("homework")]
        public async Task<IActionResult> GetHomework (int id, string token)
        {
            var result = TokenUtility.ValidateToken(token);
            if (!result) return StatusCode(401);

            var homework = await _context.Homeworks.FirstOrDefaultAsync(x => x.Id == id);

            if (homework == null)
            {
                return NotFound();
            }

            return Ok(homework);
        }

        /// <summary>
        /// Добавить дз
        /// </summary>
        [HttpPost("homework")]
        public async Task<IActionResult> AddHomework([FromBody] HomeworkDto dto, int teacherId, int courseId, string token)
        {
            var result = TokenUtility.ValidateToken(token);
            if (!result) return StatusCode(401);

            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors)
                                      .Select(e => e.ErrorMessage)
                                      .ToList();

                return BadRequest(errors);
            }

            var course = await _context.Courses.FirstOrDefaultAsync(x => x.Id == courseId);

            if (course == null)
            {
                return NotFound("Курс не найден");
            }

            if (course.OwnerId != teacherId)
            {
                return BadRequest("У препода нет доступа к курсу!");
            }

            var homework = _mapper.Map<Homework>(dto);
            _context.Homeworks.Add(homework);
            course.Homeworks.Add(homework);
            _context.SaveChanges();

            return Ok(homework);
        }

        /// <summary>
        /// Добавить решение дз
        /// </summary>
        [HttpPost("suggest")]
        public async Task<IActionResult> AddSuggest([FromBody] SuggestHomeworkDto dto, int homeworkId, int userId, string token)
        {
            var result = TokenUtility.ValidateToken(token);
            if (!result) return StatusCode(401);

            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors)
                                      .Select(e => e.ErrorMessage)
                                      .ToList();

                return BadRequest(errors);
            }

            var course = _context.Courses.FirstOrDefault(c => c.Homeworks.Any(l => l.Id == homeworkId));
            var access = course.StudentsIds.Contains(userId);

            if (!access)
            {
                return NotFound("У ученика нет доступа к курсу!");
            }

            var homeworkIsExist = await _context.SolvedHomeworks.FirstOrDefaultAsync(x => x.Id == homeworkId);
            if (homeworkIsExist != null)
            {
                return BadRequest("Решение уже загружено");
            }

            SolvedHomework homework = new SolvedHomework();
            homework.StudentId = userId;
            homework.Comment = dto.Comment;

            _context.SolvedHomeworks.Add(homework);
            _context.SaveChanges();

            return Ok();
        }

        /// <summary>
        /// Добавить проверку дз
        /// </summary>
        [HttpPost("check")]
        public async Task<IActionResult> AddChech([FromBody] CheckHomeworkDto dto, int solvedId, string token)
        {
            var result = TokenUtility.ValidateToken(token);
            if (!result) return StatusCode(401);

            var solved = await _context.SolvedHomeworks.FirstOrDefaultAsync(x => x.Id == solvedId);

            if (solved == null)
            {
                return NotFound();
            }

            solved.ScoreOf5 = dto.ScoreOf5;
            solved.Comment = dto.CheckedComment;
            solved.isChecked = true;

            _context.SaveChanges();

            return Ok(solved);
        }

        /// <summary>
        /// Получить решение дз
        /// </summary>
        [HttpGet("solved")]
        public async Task<IActionResult> GetSolved(int solvedId, string token)
        {
            var result = TokenUtility.ValidateToken(token);
            if (!result) return StatusCode(401);

            var solved = await _context.SolvedHomeworks.FirstOrDefaultAsync(x => x.Id == solvedId);

            if (solved == null)
            {
                return NotFound();
            }

            return Ok(solved);
        }
    }
}
