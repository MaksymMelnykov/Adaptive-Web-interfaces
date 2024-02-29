using Lab7.Models;
using Lab7.Services.Interfaces;

namespace Lab7.Services
{
    public class FootballPlayerService : IFootballPlayerService
    {
        private readonly List<FootballPlayer> _footballPlayers;

        public FootballPlayerService()
        {
            _footballPlayers = new List<FootballPlayer>
            {
                // Гравці для Barcelona
                new FootballPlayer { Id = 1, Name = "Lionel Messi", Age = 34, Position = "Forward", ClubId = 1 },
                new FootballPlayer { Id = 2, Name = "Gerard Pique", Age = 35, Position = "Defender", ClubId = 1 },
                new FootballPlayer { Id = 3, Name = "Jordi Alba", Age = 32, Position = "Defender", ClubId = 1 },
                new FootballPlayer { Id = 4, Name = "Sergio Busquets", Age = 33, Position = "Midfielder", ClubId = 1 },
                new FootballPlayer { Id = 5, Name = "Antoine Griezmann", Age = 30, Position = "Forward", ClubId = 1 },
                new FootballPlayer { Id = 6, Name = "Frenkie de Jong", Age = 24, Position = "Midfielder", ClubId = 1 },
                new FootballPlayer { Id = 7, Name = "Pedri", Age = 19, Position = "Midfielder", ClubId = 1 },
                new FootballPlayer { Id = 8, Name = "Ousmane Dembélé", Age = 24, Position = "Forward", ClubId = 1 },
                new FootballPlayer { Id = 9, Name = "Marc-André ter Stegen", Age = 29, Position = "Goalkeeper", ClubId = 1 },
                new FootballPlayer { Id = 10, Name = "Ronald Araújo", Age = 22, Position = "Defender", ClubId = 1 },
                new FootballPlayer { Id = 11, Name = "Robert Lewandowski", Age = 35, Position = "Forward", ClubId = 1 },

                // Гравці для Real Madrid
                new FootballPlayer { Id = 12, Name = "Karim Benzema", Age = 34, Position = "Forward", ClubId = 2 },
                new FootballPlayer { Id = 13, Name = "Thibaut Courtois", Age = 29, Position = "Goalkeeper", ClubId = 2 },
                new FootballPlayer { Id = 14, Name = "Vinícius Júnior", Age = 21, Position = "Forward", ClubId = 2 },
                new FootballPlayer { Id = 15, Name = "Luka Modrić", Age = 36, Position = "Midfielder", ClubId = 2 },
                new FootballPlayer { Id = 16, Name = "Toni Kroos", Age = 32, Position = "Midfielder", ClubId = 2 },
                new FootballPlayer { Id = 17, Name = "Sergio Ramos", Age = 35, Position = "Defender", ClubId = 2 },
                new FootballPlayer { Id = 18, Name = "Raphaël Varane", Age = 28, Position = "Defender", ClubId = 2 },
                new FootballPlayer { Id = 19, Name = "Marco Asensio", Age = 26, Position = "Forward", ClubId = 2 },
                new FootballPlayer { Id = 20, Name = "Ferland Mendy", Age = 26, Position = "Defender", ClubId = 2 },
                new FootballPlayer { Id = 21, Name = "Casemiro", Age = 29, Position = "Midfielder", ClubId = 2 },
                new FootballPlayer { Id = 22, Name = "Éder Militão", Age = 23, Position = "Defender", ClubId = 2 },

                // Гравці для Manchester United
                new FootballPlayer { Id = 23, Name = "Cristiano Ronaldo", Age = 37, Position = "Forward", ClubId = 3 },
                new FootballPlayer { Id = 24, Name = "Bruno Fernandes", Age = 27, Position = "Midfielder", ClubId = 3 },
                new FootballPlayer { Id = 25, Name = "David de Gea", Age = 31, Position = "Goalkeeper", ClubId = 3 },
                new FootballPlayer { Id = 26, Name = "Paul Pogba", Age = 29, Position = "Midfielder", ClubId = 3 },
                new FootballPlayer { Id = 27, Name = "Aaron Wan-Bissaka", Age = 25, Position = "Defender", ClubId = 3 },
                new FootballPlayer { Id = 28, Name = "Marcus Rashford", Age = 24, Position = "Forward", ClubId = 3 },
                new FootballPlayer { Id = 29, Name = "Luke Shaw", Age = 26, Position = "Defender", ClubId = 3 },
                new FootballPlayer { Id = 30, Name = "Jadon Sancho", Age = 21, Position = "Forward", ClubId = 3 },
                new FootballPlayer { Id = 31, Name = "Harry Maguire", Age = 28, Position = "Defender", ClubId = 3 },
                new FootballPlayer { Id = 32, Name = "Fred", Age = 28, Position = "Midfielder", ClubId = 3 },
                new FootballPlayer { Id = 33, Name = "Edinson Cavani", Age = 35, Position = "Forward", ClubId = 3 },

                // Гравці для Liverpool
                new FootballPlayer { Id = 34, Name = "Virgil van Dijk", Age = 30, Position = "Defender", ClubId = 4 },
                new FootballPlayer { Id = 35, Name = "Mohamed Salah", Age = 29, Position = "Forward", ClubId = 4 },
                new FootballPlayer { Id = 36, Name = "Sadio Mané", Age = 29, Position = "Forward", ClubId = 4 },
                new FootballPlayer { Id = 37, Name = "Alisson Becker", Age = 29, Position = "Goalkeeper", ClubId = 4 },
                new FootballPlayer { Id = 38, Name = "Trent Alexander-Arnold", Age = 23, Position = "Defender", ClubId = 4 },
                new FootballPlayer { Id = 39, Name = "Roberto Firmino", Age = 30, Position = "Forward", ClubId = 4 },
                new FootballPlayer { Id = 40, Name = "Fabinho", Age = 28, Position = "Midfielder", ClubId = 4 },
                new FootballPlayer { Id = 41, Name = "Andrew Robertson", Age = 27, Position = "Defender", ClubId = 4 },
                new FootballPlayer { Id = 42, Name = "Diogo Jota", Age = 25, Position = "Forward", ClubId = 4 },
                new FootballPlayer { Id = 43, Name = "Jordan Henderson", Age = 31, Position = "Midfielder", ClubId = 4 },
                new FootballPlayer { Id = 44, Name = "Thiago Alcântara", Age = 30, Position = "Midfielder", ClubId = 4 },

                // Гравці для Bayern Munich
                new FootballPlayer { Id = 45, Name = "Manuel Neuer", Age = 35, Position = "Goalkeeper", ClubId = 5 },
                new FootballPlayer { Id = 46, Name = "Joshua Kimmich", Age = 26, Position = "Midfielder", ClubId = 5 },
                new FootballPlayer { Id = 47, Name = "Harry Kane", Age = 30, Position = "Forward", ClubId = 5 },
                new FootballPlayer { Id = 48, Name = "Thomas Müller", Age = 32, Position = "Forward", ClubId = 5 },
                new FootballPlayer { Id = 49, Name = "Leroy Sané", Age = 26, Position = "Forward", ClubId = 5 },
                new FootballPlayer { Id = 50, Name = "Leon Goretzka", Age = 27, Position = "Midfielder", ClubId = 5 },
                new FootballPlayer { Id = 51, Name = "Serge Gnabry", Age = 26, Position = "Forward", ClubId = 5 },
                new FootballPlayer { Id = 52, Name = "David Alaba", Age = 29, Position = "Defender", ClubId = 5 },
                new FootballPlayer { Id = 53, Name = "Alphonso Davies", Age = 21, Position = "Defender", ClubId = 5 },
                new FootballPlayer { Id = 54, Name = "Kingsley Coman", Age = 25, Position = "Forward", ClubId = 5 },
                new FootballPlayer { Id = 55, Name = "Jerome Boateng", Age = 33, Position = "Defender", ClubId = 5 },

                // Гравці для Juventus
                new FootballPlayer { Id = 56, Name = "Cristiano Ronaldo", Age = 36, Position = "Forward", ClubId = 6 },
                new FootballPlayer { Id = 57, Name = "Paulo Dybala", Age = 28, Position = "Forward", ClubId = 6 },
                new FootballPlayer { Id = 58, Name = "Giorgio Chiellini", Age = 37, Position = "Defender", ClubId = 6 },
                new FootballPlayer { Id = 59, Name = "Leonardo Bonucci", Age = 34, Position = "Defender", ClubId = 6 },
                new FootballPlayer { Id = 60, Name = "Federico Chiesa", Age = 24, Position = "Midfielder", ClubId = 6 },
                new FootballPlayer { Id = 61, Name = "Aaron Ramsey", Age = 31, Position = "Midfielder", ClubId = 6 },
                new FootballPlayer { Id = 62, Name = "Weston McKennie", Age = 23, Position = "Midfielder", ClubId = 6 },
                new FootballPlayer { Id = 63, Name = "Dejan Kulusevski", Age = 21, Position = "Forward", ClubId = 6 },
                new FootballPlayer { Id = 64, Name = "Juan Cuadrado", Age = 33, Position = "Midfielder", ClubId = 6 },
                new FootballPlayer { Id = 65, Name = "Arthur", Age = 25, Position = "Midfielder", ClubId = 6 },
                new FootballPlayer { Id = 66, Name = "Alvaro Morata", Age = 29, Position = "Forward", ClubId = 6 },

                // Гравці для Paris Saint-Germain
                new FootballPlayer { Id = 67, Name = "Kylian Mbappe", Age = 23, Position = "Forward", ClubId = 7 },
                new FootballPlayer { Id = 68, Name = "Neymar Jr", Age = 30, Position = "Forward", ClubId = 7 },
                new FootballPlayer { Id = 69, Name = "Goncalo Ramos", Age = 22, Position = "Forward", ClubId = 7 },
                new FootballPlayer { Id = 70, Name = "Marco Verratti", Age = 29, Position = "Midfielder", ClubId = 7 },
                new FootballPlayer { Id = 71, Name = "Angel Di Maria", Age = 33, Position = "Midfielder", ClubId = 7 },
                new FootballPlayer { Id = 72, Name = "Gianluigi Donnarumma", Age = 23, Position = "Goalkeeper", ClubId = 7 },
                new FootballPlayer { Id = 73, Name = "Marquinhos", Age = 27, Position = "Defender", ClubId = 7 },
                new FootballPlayer { Id = 74, Name = "Achraf Hakimi", Age = 23, Position = "Defender", ClubId = 7 },
                new FootballPlayer { Id = 75, Name = "Presnel Kimpembe", Age = 26, Position = "Defender", ClubId = 7 },
                new FootballPlayer { Id = 76, Name = "Leandro Paredes", Age = 27, Position = "Midfielder", ClubId = 7 },
                new FootballPlayer { Id = 77, Name = "Idrissa Gueye", Age = 32, Position = "Midfielder", ClubId = 7 },

                // Гравці для Chelsea
                new FootballPlayer { Id = 78, Name = "Romelu Lukaku", Age = 28, Position = "Forward", ClubId = 8 },
                new FootballPlayer { Id = 79, Name = "Mason Mount", Age = 23, Position = "Midfielder", ClubId = 8 },
                new FootballPlayer { Id = 80, Name = "N'Golo Kanté", Age = 31, Position = "Midfielder", ClubId = 8 },
                new FootballPlayer { Id = 81, Name = "Thiago Silva", Age = 37, Position = "Defender", ClubId = 8 },
                new FootballPlayer { Id = 82, Name = "Ben Chilwell", Age = 25, Position = "Defender", ClubId = 8 },
                new FootballPlayer { Id = 83, Name = "Kai Havertz", Age = 22, Position = "Forward", ClubId = 8 },
                new FootballPlayer { Id = 84, Name = "Reece James", Age = 22, Position = "Defender", ClubId = 8 },
                new FootballPlayer { Id = 85, Name = "Jorginho", Age = 30, Position = "Midfielder", ClubId = 8 },
                new FootballPlayer { Id = 86, Name = "Christian Pulisic", Age = 23, Position = "Forward", ClubId = 8 },
                new FootballPlayer { Id = 87, Name = "Edouard Mendy", Age = 29, Position = "Goalkeeper", ClubId = 8 },
                new FootballPlayer { Id = 88, Name = "Mykhailo Mudryk", Age = 23, Position = "Forward", ClubId = 8 },

                // Гравці для AC Milan
                new FootballPlayer { Id = 89, Name = "Zlatan Ibrahimovic", Age = 40, Position = "Forward", ClubId = 9 },
                new FootballPlayer { Id = 90, Name = "Franck Kessié", Age = 25, Position = "Midfielder", ClubId = 9 },
                new FootballPlayer { Id = 91, Name = "Gianluigi Donnarumma", Age = 23, Position = "Goalkeeper", ClubId = 9 },
                new FootballPlayer { Id = 92, Name = "Theo Hernandez", Age = 24, Position = "Defender", ClubId = 9 },
                new FootballPlayer { Id = 93, Name = "Sandro Tonali", Age = 21, Position = "Midfielder", ClubId = 9 },
                new FootballPlayer { Id = 94, Name = "Simon Kjaer", Age = 32, Position = "Defender", ClubId = 9 },
                new FootballPlayer { Id = 95, Name = "Rafael Leao", Age = 22, Position = "Forward", ClubId = 9 },
                new FootballPlayer { Id = 96, Name = "Davide Calabria", Age = 25, Position = "Defender", ClubId = 9 },
                new FootballPlayer { Id = 97, Name = "Ismael Bennacer", Age = 24, Position = "Midfielder", ClubId = 9 },
                new FootballPlayer { Id = 98, Name = "Ante Rebic", Age = 28, Position = "Forward", ClubId = 9 },
                new FootballPlayer { Id = 99, Name = "Alessio Romagnoli", Age = 27, Position = "Defender", ClubId = 9 },

                // Гравці для Ajax
                new FootballPlayer { Id = 100, Name = "Dusan Tadic", Age = 33, Position = "Forward", ClubId = 10 },
                new FootballPlayer { Id = 101, Name = "Sebastien Haller", Age = 27, Position = "Forward", ClubId = 10 },
                new FootballPlayer { Id = 102, Name = "Ryan Gravenberch", Age = 19, Position = "Midfielder", ClubId = 10 },
                new FootballPlayer { Id = 103, Name = "Davy Klaassen", Age = 28, Position = "Midfielder", ClubId = 10 },
                new FootballPlayer { Id = 104, Name = "Jurrien Timber", Age = 20, Position = "Defender", ClubId = 10 },
                new FootballPlayer { Id = 105, Name = "Antony", Age = 21, Position = "Forward", ClubId = 10 },
                new FootballPlayer { Id = 106, Name = "Nico Tagliafico", Age = 29, Position = "Defender", ClubId = 10 },
                new FootballPlayer { Id = 107, Name = "David Neres", Age = 25, Position = "Forward", ClubId = 10 },
                new FootballPlayer { Id = 108, Name = "Edson Alvarez", Age = 24, Position = "Defender", ClubId = 10 },
                new FootballPlayer { Id = 109, Name = "Maarten Stekelenburg", Age = 39, Position = "Goalkeeper", ClubId = 10 },
                new FootballPlayer { Id = 110, Name = "Lisandro Martinez", Age = 24, Position = "Defender", ClubId = 10 }
            };
        }

        public async Task<IEnumerable<FootballPlayer>> GetFootballPlayersAsync()
        {
            return _footballPlayers;
        }

        public async Task<FootballPlayer> GetFootballPlayerByIdAsync(int id)
        {
            return _footballPlayers.FirstOrDefault(club => club.Id == id);
        }

        public async Task<FootballPlayer> AddFootballPlayerAsync(FootballPlayer footballPlayer)
        {
            footballPlayer.Id = _footballPlayers.Max(player => player.Id) + 1;
            _footballPlayers.Add(footballPlayer);
            return footballPlayer;
        }

        public async Task<FootballPlayer> UpdateFootballPlayerAsync(int id, FootballPlayer footballPlayer)
        {
            var existingFootballPlayer = _footballPlayers.FirstOrDefault(player => player.Id == id);
            if (existingFootballPlayer != null)
            {
                existingFootballPlayer.Age = footballPlayer.Age;
                existingFootballPlayer.Position = footballPlayer.Position;
                existingFootballPlayer.ClubId = footballPlayer.ClubId;
                return existingFootballPlayer;
            }
            return null;
        }

        public async Task<FootballPlayer> DeleteFootballPlayerAsync(int id)
        {
            var playerToRemove = _footballPlayers.FirstOrDefault(player => player.Id == id);
            if (playerToRemove != null)
            {
                _footballPlayers.Remove(playerToRemove);
                return playerToRemove;
            }
            return null;
        }
    }
}
