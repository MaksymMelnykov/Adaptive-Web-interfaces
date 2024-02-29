using Lab7.Models;

namespace Lab7.Services.Interfaces
{
    public interface IFootballPlayerService
    {
        Task<IEnumerable<FootballPlayer>> GetFootballPlayersAsync();
        Task<FootballPlayer> GetFootballPlayerByIdAsync(int id);
        Task<FootballPlayer> AddFootballPlayerAsync(FootballPlayer footballPlayer);
        Task<FootballPlayer> UpdateFootballPlayerAsync(int id, FootballPlayer footballPlayer);
        Task<FootballPlayer> DeleteFootballPlayerAsync(int id);
    }
}
