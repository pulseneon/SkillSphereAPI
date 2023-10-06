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
    public class ContentController : ControllerBase
    {
        private readonly ApplicationContext _context;
        private readonly IMapper _mapper;
        public ContentController(ApplicationContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        /// <summary>
        /// Получить контент
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetContent(int id, string token)
        {
            var result = TokenUtility.ValidateToken(token);
            if (!result) return StatusCode(401);

            var content = await _context.Contents.FirstOrDefaultAsync(x => x.Id == id);

            return Ok(content);
        }

        /// <summary>
        /// Получить контент по курсу
        /// </summary>
        [HttpGet("course")]
        public async Task<IActionResult> AllContent(int courseId, string token)
        {
            var result = TokenUtility.ValidateToken(token);
            if (!result) return StatusCode(401);

            var course = await _context.Courses.Include(x => x.Contents).FirstOrDefaultAsync(x => x.Id == courseId);
            var content = course.Contents.ToList();

            return Ok(content);
        }

        /// <summary>
        /// Добавить контент
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> AddContent([FromBody] ContentDto content, int courseId, string token)
        {
            var result = TokenUtility.ValidateToken(token);
            if (!result) return StatusCode(401);

            var mapped = _mapper.Map<Content>(content);

            _context.Contents.Add(mapped);

            var course = await _context.Courses.FirstOrDefaultAsync(x => x.Id == courseId);

            if (course == null)
            {
                return NotFound("Не найден курс");
            }

            course.Contents.Add(mapped);

            _context.SaveChanges();
            return Ok(mapped);
        }
    }
}
