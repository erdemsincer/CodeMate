using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using UserProfileService.Dtos;
using UserProfileService.Services;

namespace UserProfileService.Controllers
{
    [Authorize]
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
            var userId = GetUserId();
            await _service.AddSkillAsync(userId, dto);
            return Ok();
        }

        [HttpGet("my-skills")]
        public async Task<IActionResult> GetMySkills()
        {
            var userId = GetUserId();
            var skills = await _service.GetSkillsAsync(userId);
            return Ok(skills);
        }

        [HttpDelete("{skillId}")]
        public async Task<IActionResult> DeleteSkill(int skillId)
        {
            await _service.DeleteSkillAsync(skillId);
            return NoContent();
        }

        private Guid GetUserId()
        {
            var userIdStr = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return Guid.Parse(userIdStr!);
        }
        [HttpGet("only-if-exists/{userId}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetSkillsIfExists(Guid userId)
        {
            var skills = await _service.GetSkillsIfExistsAsync(userId);

            if (skills == null)
                return NotFound(new { message = "Skill bulunamadı." });

            return Ok(skills);
        }


    }
}
