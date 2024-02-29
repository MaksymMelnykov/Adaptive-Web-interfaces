using Lab7.Models;
using Lab7.Services;
using Lab7.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Lab7.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MatchesController : ControllerBase
    {
        private readonly IMatchesService _matchesService;

        public MatchesController(IMatchesService matchesService)
        {
            _matchesService = matchesService;
        }

        [HttpGet("matches/")]
        public async Task<IActionResult> GetFootballMatches()
        {
            await Task.Delay(2000);
            var matches = await _matchesService.GetFootballMatchesAsync();
            return Ok(matches);
        }

        [HttpGet("matches/{id}")]
        public async Task<IActionResult> GetFootballPlayerById(int id)
        {
            await Task.Delay(2000);
            var matches = await _matchesService.GetFootballMatchByIdAsync(id);
            if (matches == null)
                return NotFound($"Football Match with ID {id} not found");
            return Ok(matches);
        }

        [HttpPost]
        public async Task<IActionResult> AddFootballMatch([FromBody] Matches matches)
        {
            await Task.Delay(2000);
            await _matchesService.AddFootballMatchAsync(matches);
            return CreatedAtAction(nameof(GetFootballMatches), new { id = matches.Id }, matches);
        }

        [HttpPut("matches/{id}")]
        public async Task<IActionResult> UpdateFootballMatch(int id, [FromBody] Matches matches)
        {

            await Task.Delay(2000); 
            var updatedMatch = await _matchesService.UpdateFootballMatchAsync(id, matches);
            if (updatedMatch == null)
                return NotFound($"Football Match with ID {id} not found");
            return Ok($"Football Match with ID {id} updated successfully");
        }

        [HttpDelete("matches/{id}")]
        public async Task<IActionResult> DeleteFootballMatch(int id)
        {
            await Task.Delay(2000);
            var deletedMatch = await _matchesService.DeleteFootballMatchAsync(id);
            if (deletedMatch == null)
                return NotFound($"Football Match with ID {id} not found");
            return Ok($"Football Match with ID {id} deleted successfully");
        }
    }
}
