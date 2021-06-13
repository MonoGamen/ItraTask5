using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace TicTacToe.Classes
{
     public class GameCollection
    {
        public List<Game> Games { get; set; }
        public GameCollection()
        {
            Games = new List<Game>();
        }
        public int GenerateGameId()
        {
            int id = 0;
            while (true)
            {
                id = RandomNumberGenerator.GetInt32(1, int.MaxValue);
                if (!Games.Select(g => g.GameId).Contains(id))
                    return id;
            }
        }

        public void DeleteGame(int id)
        {
            Game game = Games.FirstOrDefault(g => g.GameId == id);
            if (game != null)
                Games.Remove(game);
        }
    }
}
