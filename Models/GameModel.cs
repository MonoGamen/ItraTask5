using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TicTacToe.Models
{
    public class GameModel
    {
        public int GameId { get; set; }
        public string Name { get; set; }
        public HashSet<string> Tags { get; set; }
        public GameModel()
        {
           Tags = new HashSet<string>();
        }
    }
}
