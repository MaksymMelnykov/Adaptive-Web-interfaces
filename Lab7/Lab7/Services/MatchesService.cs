using Lab7.Models;
using Lab7.Services.Interfaces;
using Microsoft.Extensions.FileSystemGlobbing;

namespace Lab7.Services
{
    public class MatchesService : IMatchesService
    {
        private readonly List<Matches> _matches;

        public MatchesService()
        {
            _matches = new List<Matches> 
            {
                new Matches { Id = 1, Location = "Camp Nou", Date = new DateTime(2024, 2, 1), HomeTeamId = 1, AwayTeamId = 2 },
                new Matches { Id = 2, Location = "Santiago Bernabeu", Date = new DateTime(2024, 2, 8), HomeTeamId = 2, AwayTeamId = 9 },
                new Matches { Id = 3, Location = "Old Trafford", Date = new DateTime(2024, 2, 15), HomeTeamId = 3, AwayTeamId = 1 },
                new Matches { Id = 4, Location = "Anfield", Date = new DateTime(2024, 2, 22), HomeTeamId = 4, AwayTeamId = 5 },
                new Matches { Id = 5, Location = "Allianz Arena", Date = new DateTime(2024, 2, 29), HomeTeamId = 5, AwayTeamId = 10 },
                new Matches { Id = 6, Location = "Allianz Stadium", Date = new DateTime(2024, 3, 7), HomeTeamId = 6, AwayTeamId = 3 },
                new Matches { Id = 7, Location = "Parc des Princes", Date = new DateTime(2024, 3, 14), HomeTeamId = 7, AwayTeamId = 4 },
                new Matches { Id = 8, Location = "Stamford Bridge", Date = new DateTime(2024, 3, 21), HomeTeamId = 8, AwayTeamId = 7 },
                new Matches { Id = 9, Location = "San Siro", Date = new DateTime(2024, 3, 28), HomeTeamId = 9, AwayTeamId = 6 },
                new Matches { Id = 10, Location = "Johan Cruyff Arena", Date = new DateTime(2024, 4, 4), HomeTeamId = 10, AwayTeamId = 8 }
            };
        }

        public async Task<IEnumerable<Matches>> GetFootballMatchesAsync()
        {
            return _matches;
        }

        public async Task<Matches> GetFootballMatchByIdAsync(int id)
        {
            return _matches.FirstOrDefault(club => club.Id == id);
        }

        public async Task<Matches> AddFootballMatchAsync(Matches matches)
        {
            matches.Id = _matches.Max(match => match.Id) + 1;
            _matches.Add(matches);
            return matches;
        }

        public async Task<Matches> UpdateFootballMatchAsync(int id, Matches matches)
        {
            var existingFootballMatch = _matches.FirstOrDefault(match => match.Id == id);
            if (existingFootballMatch != null)
            {
                existingFootballMatch.Location = matches.Location;
                existingFootballMatch.Date = matches.Date;
                existingFootballMatch.HomeTeamId = matches.HomeTeamId;
                existingFootballMatch.AwayTeamId = matches.AwayTeamId;
                return existingFootballMatch;
            }
            return null;
        }

        public async Task<Matches> DeleteFootballMatchAsync(int id)
        {
            var matchToRemove = _matches.FirstOrDefault(match => match.Id == id);
            if (matchToRemove != null)
            {
                _matches.Remove(matchToRemove);
                return matchToRemove;
            }
            return null;
        }
    }
}
