using CourseService.Data;
using CourseService.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CourseService.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class CoursesController : ControllerBase
    {
        private readonly CourseDbContext _context;

        public CoursesController(CourseDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Course course)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId is null) return Unauthorized();

            course.OwnerId = Guid.Parse(userId);
            _context.Courses.Add(course);
            await _context.SaveChangesAsync();

            return Ok(course);
        }

        [HttpGet]
        public IActionResult GetAll() => Ok(_context.Courses.ToList());

        [HttpGet("my")]
        public IActionResult GetMyCourses()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId is null) return Unauthorized();

            var guid = Guid.Parse(userId);
            var myCourses = _context.Courses.Where(c => c.OwnerId == guid).ToList();

            return Ok(myCourses);
        }
    }
}
