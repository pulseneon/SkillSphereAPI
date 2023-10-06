using asp_net_db.Data;
using asp_net_db.Utilities;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace asp_net_db.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RepositoryController : ControllerBase
    {
        private readonly ApplicationContext _context;
        private readonly IMapper _mapper;
        public RepositoryController(ApplicationContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        /// <summary>
        /// Получить все курсы
        /// </summary>
        [HttpGet("allcourses")]
        public async Task<IActionResult> GetCourses(string token, int? count = 10)
        {
            var result = TokenUtility.ValidateToken(token);
            if (!result) return StatusCode(401);

            var returned = await _context.Courses.Take((int)count).ToListAsync();

            return Ok(returned);
        }
    }
}
