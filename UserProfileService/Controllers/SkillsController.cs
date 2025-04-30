using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserProfileService.Dtos;
using UserProfileService.Services;

namespace UserProfileService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SkillsController : ControllerBase
    {
        private readonly IUserSkillService _service;

        public SkillsController(IUserSkillService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> AddSkill([FromBody] UserSkillDto dto)
        {
            await _service.AddSkillAsync(dto);
            return Ok();
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetSkills(Guid userId)
        {
            var skills = await _service.GetSkillsAsync(userId);
            return Ok(skills);
        }

        [HttpDelete("{skillId}")]
        public async Task<IActionResult> DeleteSkill(int skillId)
        {
            await _service.DeleteSkillAsync(skillId);
            return NoContent();
        }
    }
}
