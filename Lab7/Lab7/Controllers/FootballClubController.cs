using Lab7.Models;
using Lab7.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Lab7.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class FootballClubController : ControllerBase
    {
        private readonly IFootballClubService _footballClubService;

        public FootballClubController(IFootballClubService footballClubService)
        {
            _footballClubService = footballClubService;
        }

        [HttpGet("clubs/")]
        public async Task<IActionResult> GetFootballClubs()
        {
            await Task.Delay(2000);
            var clubs = await _footballClubService.GetFootballClubsAsync();
            return Ok(clubs);
        }

        [HttpGet("clubs/{id}")]
        public async Task<IActionResult> GetFootballClubById(int id)
        {
            await Task.Delay(2000);
            var clubs = await _footballClubService.GetFootballClubByIdAsync(id);
            if (clubs == null)
                return NotFound($"Football Club with ID {id} not found");
            return Ok(clubs);
        }

        [HttpPost]
        public async Task<IActionResult> AddFootballClub([FromBody] FootballClub footballClub)
        {
            await Task.Delay(2000);
            await _footballClubService.AddFootballClubAsync(footballClub);
            return CreatedAtAction(nameof(GetFootballClubs), new { id = footballClub.Id }, footballClub);
        }

        [HttpPut("clubs/{id}")]
        public async Task<IActionResult> UpdateFootballClub(int id, [FromBody] FootballClub footballClub)
        {
            await Task.Delay(2000);
            var updatedClub = await _footballClubService.UpdateFootballClubAsync(id, footballClub);
            if (updatedClub == null)
                return NotFound($"Football Club with ID {id} not found");
            return Ok($"Football Club with ID {id} updated successfully");
        }

        [HttpDelete("clubs/{id}")]
        public async Task<IActionResult> DeleteFootballClub(int id)
        {
            await Task.Delay(2000);
            var deletedClub = await _footballClubService.DeleteFootballClubAsync(id);
            if (deletedClub == null)
                return NotFound($"Football Club with ID {id} not found");
            return Ok($"Football Club with ID {id} deleted successfully");
        }
    }
}
