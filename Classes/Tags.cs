using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TicTacToe.Classes
{
    public class TagCollection
    {
        public HashSet<string> Tags { get; set; }

        public TagCollection()
        {
            Tags = new HashSet<string>();
            Tags.Add("easy");
            Tags.Add("medium");
            Tags.Add("hard");
        }
    }
}
