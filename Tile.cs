using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Iskrabol
{
    public class Tile : PictureBox
    {
        public List<string> rackLetters;
        private int tilePoints { get; set; }
        Random rand = new Random();
        public List<string> blankTile = new List<string>(){
            " ", " "
        };

        public List<string> onePointer = new List<string>(){
            "E",
            "A",
            "I",
            "O",
            "N",
            "R",
            "T",
            "L",
            "S",
            "U"
        };
        public List<string> twoPointer = new List<string>(){
            "D",
            "G"
        };
        public List<string> threePointer = new List<string>(){
            "B","C","M","P"
        };
        public List<string> fourPointer = new List<string>(){
            "F","H","V","W","Y"
        };
        public List<string> fivePointer = new List<string>(){
            "K"
        };
        public List<string> eightPointer = new List<string>(){
            "J","X"
        };
        public List<string> tenPointer = new List<string>(){
            "Q","Z"
        };

        public List<string> tiles = new List<string>(){
            "E","E","E","E","E","E","E","E","E","E","E","E",
            "A","A","A","A","A","A","A","A","A",
            "I","I","I","I","I","I","I","I","I",
            "O","O","O","O","O","O","O","O",
            "N","N","N","N","N","N",
            "R","R","R","R","R","R",
            "T","T","T","T","T","T",
            "L","L","L","L",
            "S","S","S","S",
            "U","U","U","U",
            "D","D","D","D",
            "G","G","G",
            "B","B","C","C","M","M","P","P",
            "F","F","H","H","V","V","W","W","Y","Y","K",
            "J","X", "Q","Z"," "," "
        };
        public List<string> bag(){
            return tiles;
        }
        
        public  List<string> getLetters(int numOfLetters, List<string> bag){
            rackLetters = new List<string>();
            if(numOfLetters <= bag.Count && bag.Count != 0){
                for(int counter = 0; counter < numOfLetters; counter++){
                    
                    rackLetters.Add(bag[rand.Next(bag.Count - 1)]);

                    bag.Remove(rackLetters[rackLetters.Count - 1]);
                    //rackLetters.Add(" ");
                   /// Console.WriteLine("Tiles Get: {0}", string.Join(",", rackLetters));
                }

            }else{
                for(int counter = 0; counter < bag.Count; counter ++){
                    rackLetters.Add(bag[counter]);
                    bag.Remove(rackLetters[rackLetters.Count -1 ]);
                }
            }

             return bag;
            
            
        }
        
    }

}
