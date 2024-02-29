using Lab7.Models;

namespace Lab7.Services.Interfaces
{
    public interface IMatchesService
    {
        Task<IEnumerable<Matches>> GetFootballMatchesAsync();
        Task<Matches> GetFootballMatchByIdAsync(int id);
        Task<Matches> AddFootballMatchAsync(Matches matches);
        Task<Matches> UpdateFootballMatchAsync(int id, Matches matches);
        Task<Matches> DeleteFootballMatchAsync(int id);
    }
}
