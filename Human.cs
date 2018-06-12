using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Text;
using System.Collections;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Threading;
namespace Iskrabol
{
    class Human : Player
    {
        public Board board = new Board();

        

        public override void makeMove(List<string> list, List<string> s)
        {
            newTiles.getLetters(7 - board.rackTiles.Count , board.tileBag);

            board.rackTiles.AddRange(newTiles.rackLetters);

            for(int counter= 0; counter< board.rackTiles.Count; counter++){

                    board.humanLetter[counter].pictureBox2.BackgroundImage = Image.FromFile("Tiles/Scrabble/"+ board.tileImage(board.rackTiles[counter]));
                    board.humanLetter[counter].letter = board.rackTiles[counter];
                    Console.WriteLine(board.humanLetter[counter]);

            }

           // board.getTilesButton.Enabled = false;  //Console.WriteLine("asdf");

        }

    }
}
