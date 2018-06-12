using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Iskrabol
{
    /// <summary>
    /// Player blueprint for AI-type and Human-type players.
    /// </summary>
    public abstract class Player
    {
        public int playerScore { get; set; }
        public List<Tile> playerTiles { get; set; }
        public abstract void makeMove(List<string> list, List<string> list1); 
        
    }
}
