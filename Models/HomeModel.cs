using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TicTacToe.Models
{
    public class HomeModel
    {
        public List<GameModel> Games { get; set; }
        public HashSet<string> Tags { get; set; }
    }
}
