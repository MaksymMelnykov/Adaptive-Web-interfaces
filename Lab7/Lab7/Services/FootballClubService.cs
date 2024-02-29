using Lab7.Models;
using Lab7.Services.Interfaces;

namespace Lab7.Services
{
    public class FootballClubService : IFootballClubService
    {
        private readonly List<FootballClub> _clubs;

        public FootballClubService()
        {
            _clubs = new List<FootballClub>
            {
                new FootballClub { Id = 1, Name = "Barcelona", City = "Barcelona", Stadium = "Spotify Camp Nou", YearOfCreation = 1899 },
                new FootballClub { Id = 2, Name = "Real Madrid", City = "Madrid", Stadium = "Santiago Bernabeu", YearOfCreation = 1902 },
                new FootballClub { Id = 3, Name = "Manchester United", City = "Manchester", Stadium = "Old Trafford", YearOfCreation = 1878 },
                new FootballClub { Id = 4, Name = "Liverpool", City = "Liverpool", Stadium = "Anfield", YearOfCreation = 1892 },
                new FootballClub { Id = 5, Name = "Bayern Munich", City = "Munich", Stadium = "Allianz Arena", YearOfCreation = 1900 },
                new FootballClub { Id = 6, Name = "Juventus", City = "Turin", Stadium = "Allianz Stadium", YearOfCreation = 1897 },
                new FootballClub { Id = 7, Name = "Paris Saint-Germain", City = "Paris", Stadium = "Parc des Princes", YearOfCreation = 1970 },
                new FootballClub { Id = 8, Name = "Chelsea", City = "London", Stadium = "Stamford Bridge", YearOfCreation = 1905 },
                new FootballClub { Id = 9, Name = "AC Milan", City = "Milan", Stadium = "San Siro", YearOfCreation = 1899 },
                new FootballClub { Id = 10, Name = "Ajax", City = "Amsterdam", Stadium = "Johan Cruyff Arena", YearOfCreation = 1900 }
            };
        }

        public async Task<IEnumerable<FootballClub>> GetFootballClubsAsync()
        {
            return _clubs;
        }

        public async Task<FootballClub> GetFootballClubByIdAsync(int id)
        {
            return _clubs.FirstOrDefault(club => club.Id == id);
        }

        public async Task<FootballClub> AddFootballClubAsync(FootballClub footballClub)
        {
            footballClub.Id = _clubs.Max(club => club.Id) + 1;
            _clubs.Add(footballClub);
            return footballClub;
        }

        public async Task<FootballClub> UpdateFootballClubAsync(int id, FootballClub footballClub)
        {
            var existingFootballClub = _clubs.FirstOrDefault(club => club.Id == id);
            if (existingFootballClub != null)
            {
                existingFootballClub.Name = footballClub.Name;
                existingFootballClub.City = footballClub.City;
                existingFootballClub.YearOfCreation = footballClub.YearOfCreation;
                existingFootballClub.Stadium = footballClub.Stadium;
                return existingFootballClub;
            }
            return null;
        }

        public async Task<FootballClub> DeleteFootballClubAsync(int id)
        {
            var clubToRemove = _clubs.FirstOrDefault(club => club.Id == id);
            if (clubToRemove != null)
            {
                _clubs.Remove(clubToRemove);
                return clubToRemove;
            }
            return null;
        }
    }
}
