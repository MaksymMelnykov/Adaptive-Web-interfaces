using Lab7.Models;

namespace Lab7.Services.Interfaces
{
    public interface IFootballClubService
    {
        Task<IEnumerable<FootballClub>> GetFootballClubsAsync();
        Task<FootballClub> GetFootballClubByIdAsync(int id);
        Task<FootballClub> AddFootballClubAsync(FootballClub footballClub);
        Task<FootballClub> UpdateFootballClubAsync(int id, FootballClub footballClub);
        Task<FootballClub> DeleteFootballClubAsync(int id);
    }
}
