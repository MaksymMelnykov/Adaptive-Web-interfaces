using Lab7.Models;
using Lab7.Services;
using Lab7.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lab7.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class FootballPlayerController : ControllerBase
    {
       private readonly IFootballPlayerService _footballPlayerService;

        public FootballPlayerController(IFootballPlayerService footballPlayerService)
        {
            _footballPlayerService = footballPlayerService;
        }

        [HttpGet("players/")]
        public async Task<IActionResult> GetFootballPlayers()
        {
            await Task.Delay(2000);
            var players = await _footballPlayerService.GetFootballPlayersAsync();
            return Ok(players);
        }

        [HttpGet("players/{id}")]
        public async Task<IActionResult> GetFootballPlayerById(int id)
        {
            await Task.Delay(2000);
            var players = await _footballPlayerService.GetFootballPlayerByIdAsync(id);
            if (players == null)
                return NotFound($"Football Player with ID {id} not found");
            return Ok(players);
        }

        [HttpPost]
        public async Task<IActionResult> AddFootballPlayer([FromBody] FootballPlayer footballPlayer)
        {
            await Task.Delay(2000);
            await _footballPlayerService.AddFootballPlayerAsync(footballPlayer);
            return CreatedAtAction(nameof(GetFootballPlayers), new { id = footballPlayer.Id }, footballPlayer);
        }

        [HttpPut("players/{id}")]
        public async Task<IActionResult> UpdateFootballPlayer(int id, [FromBody] FootballPlayer footballPlayer)
        {
            await Task.Delay(2000);
            var updatedPlayer = await _footballPlayerService.UpdateFootballPlayerAsync(id, footballPlayer);
            if (updatedPlayer == null)
                return NotFound($"Football Player with ID {id} not found");
            return Ok($"Football Player with ID {id} updated successfully");
        }

        [HttpDelete("players/{id}")]
        public async Task<IActionResult> DeleteFootballPlayer(int id)
        {
            await Task.Delay(2000);
            var deletedPlayer = await _footballPlayerService.DeleteFootballPlayerAsync(id);
            if (deletedPlayer == null)
                return NotFound($"Football Player with ID {id} not found");
            return Ok($"Football Player with ID {id} deleted successfully");
        }
    }
}
