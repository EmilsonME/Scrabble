using System;
using System.Data;
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
    class AI: Player
    {
		Board board1 = new Board();
		Tile nTiles = new Tile();
    	Search search = new Search();
    	List<string> board = new List<string>();
    	int size,  blankTileCtr;

   	    public available[] squares ;
   	    private List<int>[] _rowsAndColumns = new List<int>[30];
   	    List<int> _temp = new List<int>();
   	    List<int> vowel1, vowel2, consonant2, consonant1, sIndex;
   	    List<string> letterMoves  = new List<string>();
       	List<int> moves = new List<int>();


       	List<int> sideRight = new List<int>{224, 209, 194, 179, 164, 149, 134, 119, 104, 89, 74, 59, 44, 29, 14 };
       	List<int> sideLeft = new List<int> {210, 195, 180, 165, 150, 135, 120, 105, 90, 75, 60, 45, 30, 15,  0 };
       	List<string> one = new List<string>();
		List<string> two = new List<string>();
		List<string> three = new List<string>();
		List<string> four = new List<string>();
		List<string> five = new List<string>();
		List<string> six = new List<string>();
		List<string> seven = new List<string>();
	 	


   	    public static List<string>  _playerTiles = new List<string>();
   	    Random rand = new Random();




    	public struct available
        {
            public int flag  {get; set;}
            public int left {get; set;}
            public int right {get; set;}
            public int up {get; set;}
            public int down {get; set;}
            public int diagonal1 { get; set;}
            public int diagonal2 { get; set;}
            public int diagonal3 { get; set;}
            public int diagonal4 { get; set;}
            public int diagonal5 { get; set;}
            public int diagonal6 { get; set;}
            public int diagonal7 { get; set;}
            public int diagonal8 { get; set;}
            public string desc {get; set;}
            public string align {get; set;}
           
        }
        public List<string> fillRacks(List<string> bag){
            List<string> temp = new List<string>();
            if(bag.Count != 0){
	        	temp =	nTiles.getLetters(7 -  _playerTiles.Count, bag);
	        	_playerTiles.AddRange(nTiles.rackLetters);
        	}else{
        		board1.winnerChecker();
        	}
        	return temp;

        }
        public void updateTiles(String used){
        	foreach(char chr in used){
        		_playerTiles.Remove(chr.ToString());
        	}
        }
        public List<string> generateValidWords(){
		
			List<string> vowels = new List<string>();
			List<string>[] validWords = new List<string>[7];
			List<string> tempTiles = new List<string>();
			List<string> gotcha = new List<string>();
			List<string> subs = new List<string>();

			Random rand = new Random();


			foreach(string lett in _playerTiles){
				bool isVowel = "AEIOU".IndexOf(lett[0]) >= 0;
				if(isVowel){
					vowels.Add(lett);
				}
			}

			if(vowels.Count != 0){
				foreach(string lett1 in _playerTiles){
					string sub = lett1 + vowels[0];
					subs.Add(sub);
				}

			}else{
				foreach(string lett1 in _playerTiles){
					string sub = lett1 + _playerTiles[0];
					subs.Add(sub);
				}
			}

					validWords = search.findPattern(subs, 7, "full");
					foreach(List<string> valids in validWords ){
					   if(valids.Count != 0){

						    for(int looper = 0; looper <valids.Count; looper++){
				        		string got = "";
				        		tempTiles.AddRange(_playerTiles);

				        		for(int inner = 0; inner < valids[looper].Length; inner ++){
				        			if(tempTiles.Contains(valids[looper][inner].ToString())) {
				        				tempTiles.Remove(valids[looper][inner].ToString());
				        				got = got + valids[looper][inner].ToString();
				        			}
				        		}
				        		tempTiles.Clear();
				        		if(got.Length == valids[looper].Length){
				        			gotcha.Add(valids[looper]);
				           		}
			        		}

			        		if(gotcha.Count != 0){
			        			break;
			        		}

		        		}

		        	}
					
			


		Console.WriteLine("words " + string.Join(",", gotcha));
		Console.WriteLine("ply " + string.Join(",", _playerTiles));

		return gotcha;    	
			
        }
        public List<string> firstMover(){
        	List<string> gotWords = new List<string>();

        	gotWords = generateValidWords();

        	return gotWords;
        }
      
        
	
		public override void makeMove(List<string> b, List<string> s){
			board.AddRange(b);
			_rowsAndColumns = board1.rowsAndColumns;
			vowel1 = new List<int>();
			vowel2 = new List<int>();
			consonant1 = new List<int>();
			consonant2 = new List<int>();
			sIndex = new List<int>();
 
			List<string> invalid = new List<string>();
		//	bool isVowel;
		    int[] direction = { -15, 15 , 1, -1, -16, -14, 16, 14, 17 , 18 , 19 , 20};

			for(int ctr = 0; ctr < board.Count; ctr++){

				if(!board[ctr].Equals("empty")){
					
					_temp.Add(ctr);
					/*for(int ctr2 = 0; ctr2  < 4; ctr2++){

						if(board[ctr + direction[ctr2]].Equals("empty")){

							if(!_temp.Contains(ctr + direction[ctr2])){
								_temp.Add(ctr + direction[ctr2]);
							}

						}
					}*/
				}
			}

			
			size = _temp.Count;
			squares = new available[size];

			int counter1 = 0;
			foreach ( int x in _temp){
				squares[counter1].flag = x;
				counter1 ++;
			}

			for(int counter = 0; counter < _temp.Count ; counter ++){
				for(int ctr2 = 0; ctr2 <  12; ctr2 ++){
					int ctr = checkAvailableSquares(direction[ctr2], squares[counter].flag);

					if(direction[ctr2] == -15){
						squares[counter].up = ctr;
					}else if(direction[ctr2] == 15){
						squares[counter].down = ctr;
					}else if(direction[ctr2] == 1){
						squares[counter].right = ctr;
					}else if(direction[ctr2] == -1){
						squares[counter].left = ctr;
					}else if(direction[ctr2] == -16){
						squares[counter].diagonal1 = ctr;
					}else if(direction[ctr2] == 16){
						squares[counter].diagonal4 = ctr;
					}else if(direction[ctr2] == 14){
						squares[counter].diagonal3 = ctr;
					}else if(direction[ctr2] == -14){
						squares[counter].diagonal2 = ctr;
					}else if(direction[ctr2] == 17){
						squares[counter].diagonal5 = ctr;
					}else if(direction[ctr2] == 18){
						squares[counter].diagonal6 = ctr;
					}else if(direction[ctr2] == 19){
						squares[counter].diagonal7 = ctr;
					}else if(direction[ctr2] == 20){
						squares[counter].diagonal8 = ctr;
					}

				}
				
			}
			
			for(int ctr0 = 0; ctr0 < _playerTiles.Count; ctr0 ++){
				bool isVowel = "AEIOU".IndexOf(_playerTiles[ctr0][0]) >= 0;
					if(isVowel){
						vowel2.Add(ctr0);
					}else{
						consonant2.Add(ctr0);
				}
			}  
			filter();
  			
  		/*	for(int x = 0; x < size; x ++){
				Console.Write("flag: " + squares[x].flag);Console.Write(" ");
				Console.Write("left: " + squares[x].left);Console.Write(" ");
				Console.Write("right: " + squares[x].right);Console.Write(" ");
				Console.Write("up: " + squares[x].up);Console.Write(" ");
				Console.Write("down: " + squares[x].down);Console.Write(" ");
				Console.Write("diag1: " + squares[x].diagonal1);Console.Write(" ");
				Console.Write("diag2: " + squares[x].diagonal2);Console.Write(" ");
				Console.Write("diag3: " + squares[x].diagonal3);Console.Write(" ");
				Console.Write("diga4: " + squares[x].diagonal4);Console.Write(" ");
				Console.Write("diag5: " + squares[x].diagonal5);Console.Write(" ");
				Console.Write("diag6: " + squares[x].diagonal6);Console.Write(" ");
				Console.Write("diag7: " + squares[x].diagonal7);Console.Write(" ");
				Console.Write("diga8: " + squares[x].diagonal8);Console.Write(" ");
				Console.Write("align: " + squares[x].align);Console.Write(" ");
				Console.Write("desc: " + squares[x].desc); Console.Write(" ");
				Console.WriteLine("====================");

			}*/
			if(sIndex.Count == 0){
				Console.WriteLine("empty sindex.");
			}
			else{
				for(int ctr = 0; ctr < sIndex.Count; ctr++){
					Random rand = new Random();
					int holyIndex = rand.Next(sIndex.Count-1);
  					bool ok = algo(sIndex[holyIndex]);
  					sIndex.Remove(sIndex[holyIndex]);
  					if(ok){
  						break;
  					}
  				}
    		}
        }
        private int checkAvailableSquares(int dir, int index){

        	int counter = 0;
        	
        	if(dir == -15 ){
        		if( index < 15){
        			counter = 0;
	        	}else{
	        		for(int ctr = 15; ctr < 30; ctr ++){

	        			if(_rowsAndColumns[ctr].Contains(index)){

	        				for(int inner = 0; inner < 15; inner ++){

	        					if(_rowsAndColumns[ctr][inner] == index){

									for(int ctr2 = inner - 1; ctr2 >=0; ctr2--){

										if(board[_rowsAndColumns[ctr][ctr2]].Equals("empty") && counter != 7){
											counter ++;
										}else{
											break;
										}					
									}    						

	        					}
	        				}
	        			}
	        		}
	        	}
        	}else if(dir == 15 ){

        		if( index < 225 && index >= 210){
        			counter = 1;
	        	}else{
	        		for(int ctr = 15; ctr < 30; ctr ++){

	        			if(_rowsAndColumns[ctr].Contains(index)){

	        				for(int inner = 0; inner < 15; inner ++){

	        					if(_rowsAndColumns[ctr][inner] == index){

									for(int ctr2 = inner + 1; ctr2 < 15; ctr2++){
										
										if(board[_rowsAndColumns[ctr][ctr2]].Equals("empty")&& counter != 7){
											counter ++;
										}else{
											break;
										}					
									}    
	        					}
	        				}
	        			}
	        		}
	        	}
        	}else if(dir == 1 ){

        		if( sideRight.Contains(index)){
        			counter = 1; 
 	        	}else{
	        		for(int ctr = 0; ctr < 15; ctr ++){

	        			if(_rowsAndColumns[ctr].Contains(index)){

	        				for(int inner = 0; inner < 15; inner ++){

	        					if(_rowsAndColumns[ctr][inner] == index){
	        						
									for(int ctr2 = inner + 1; ctr2 < 15; ctr2++){
										
										if(board[_rowsAndColumns[ctr][ctr2]].Equals("empty") && counter != 7){
											counter ++;
										}else{
											break;
										}					
									}    						

	        					}
	        				}
	        			}
	        		}
		        }
        	}else if(dir == -1 ){

        		if( sideLeft.Contains(index)){
        			counter = 1;
	        	}else{
	        		for(int ctr = 0; ctr < 15; ctr ++){

	        			if(_rowsAndColumns[ctr].Contains(index)){

	        				for(int inner = 0; inner < 15; inner ++){

	        					if(_rowsAndColumns[ctr][inner] == index){

									for(int ctr2 = inner - 1; ctr2 >=0; ctr2--){
										
										if(board[_rowsAndColumns[ctr][ctr2]].Equals("empty") && counter != 7){
											counter ++;
										}else{
											break;
										}					
									}    						
	        						
	        					}
	        				}
	        			}
	        		}
	        	}
        	}else if(dir == -16 ){	

        		if( index < 15 || sideLeft.Contains(index)){
        			counter = 1;
	        	}else{
	        		for(int ctr = 15; ctr < 30; ctr ++){

	        			if(_rowsAndColumns[ctr].Contains(index)){
	        				if(ctr == 15){
	        					break;
	        				}else{

		        				for(int inner = 0; inner < 15; inner ++){

		        					if(_rowsAndColumns[ctr][inner] == index){

										for(int ctr2 = inner ; ctr2 >0; ctr2--){

											if(board[_rowsAndColumns[ctr - 1 ][ctr2 - 1 ]].Equals("empty") && counter != 7){
												counter ++;
											}else{
												break;
											}					
										}    						
		        						
		        					}
		        				}
		        			}
	        			}
	        		}
	        	}
        	}else if(dir == 17 ){	


        		if( sideLeft.Contains(index) || index < 15){
        			counter = 1;
	        	}else{
	        		for(int ctr = 0; ctr < 15; ctr ++){

	        			if(_rowsAndColumns[ctr].Contains(index)){
	        				
		        				for(int inner = 0; inner < 15; inner ++){

		        					if(_rowsAndColumns[ctr][inner] == index){
		        						
		        						if(inner == 0){
	        								break;
	        							}else{

											for(int ctr2 = inner ; ctr2 >0; ctr2--){

												if(board[_rowsAndColumns[ctr - 1 ][ctr2 - 1 ]].Equals("empty") && counter != 7){
													counter ++;
												}else{
													break;
												}					
											}
										}    						
		        						
		        					}
		        				}
	        			}
	        		}
	        	}
        	}else if(dir == -14 ){

        		if( sideLeft.Contains(index) || index < 15){
        			counter = 1;Console.WriteLine("asdasdasdddd77777dd" + index);
	        	}else{
	        		for(int ctr = 15; ctr < 30; ctr ++){

	        			if(_rowsAndColumns[ctr].Contains(index)){

	        				if(ctr == 29){
	        					break;
	        				}else{
		        				for(int inner = 0; inner < 15; inner ++){

		        					if(_rowsAndColumns[ctr][inner] == index){
		        					
										for(int ctr2 = inner ; ctr2 >0; ctr2--){
												
											if(board[_rowsAndColumns[ctr + 1][ctr2 - 1]].Equals("empty") && counter != 7){
												counter ++;
												
											}else{
												break;
											}					
										}  
		        					}
		        				}
	        				}
	        			}
	        		}
	        	}
        	}else if(dir == 18 ){

        		if( sideRight.Contains(index) || index < 15){
        			counter = 1;Console.WriteLine("asdasdasdddddd" + index);
	        	}else{
	        		for(int ctr = 0; ctr < 15; ctr ++){

	        			if(_rowsAndColumns[ctr].Contains(index)){

		        				for(int inner = 0; inner < 15; inner ++){

		        					if(_rowsAndColumns[ctr][inner] == index){
		        						if(inner == 14){
	        								break;
	        							}else{
											for(int ctr2 = inner ; ctr2 < 14; ctr2++){
													
												if(board[_rowsAndColumns[ctr - 1][ctr2 + 1]].Equals("empty") && counter != 7){
													counter ++;
												}else{
													break;
												}					
											} 
										} 
		        					}
		        				}
	        				
	        			}
	        		}
	        	}
        	}else if(dir == 14 ){
        		
        		if( sideLeft.Contains(index) || (index < 225 && index >= 210) ){
        			counter = 1;
	        	}else{
	        		for(int ctr = 15; ctr < 30; ctr ++){

	        			if(_rowsAndColumns[ctr].Contains(index)){
	        				if(ctr == 15){
	        					break;
	        				}else{

		        				for(int inner = 0; inner < 15; inner ++){

		        					if(_rowsAndColumns[ctr][inner] == index){

										for(int ctr2 = inner ; ctr2 < 14; ctr2++){
											
											if(board[_rowsAndColumns[ctr - 1 ][ctr2 + 1]].Equals("empty") && counter != 7){
												counter ++;
											}else{
												break;
											}					
										}    						
		        						
		        					}
		        				}
	        				}
	        			}
	        		}
	        	}
        	}else if(dir == 19 ){

        		if( sideLeft.Contains(index) || (index >= 210 && index < 225)){
        			counter = 1;
	        	}else{
	        		for(int ctr = 0; ctr < 15; ctr ++){

	        			if(_rowsAndColumns[ctr].Contains(index)){
	        				
		        				for(int inner = 0; inner < 15; inner ++){

		        					if(_rowsAndColumns[ctr][inner] == index){
		        						if(inner == 0){
	        								break;
	        							}else{
											for(int ctr2 = inner ; ctr2  > 0; ctr2--){
												
												if(board[_rowsAndColumns[ctr + 1 ][ctr2 - 1]].Equals("empty") && counter != 7){
													counter ++;
												}else{
													break;
												}					
											}
										}    						
		        						
		        					}
		        				}
	        				
	        			}
	        		}
	        	}
        	}else if(dir == 16 ){

        		if( sideRight.Contains(index) || (index < 225 && index >= 210)){
        			counter = 1; 
	        	}else{
	        		for(int ctr = 15; ctr < 30; ctr ++){

	        			if(_rowsAndColumns[ctr].Contains(index)){
	        				if(ctr == 29){
	        					break;
	        				}else{
		        				for(int inner = 0; inner < 15; inner ++){

		        					if(_rowsAndColumns[ctr][inner] == index){

										for(int ctr2 = inner ; ctr2 < 14; ctr2 ++){
											
											if(board[_rowsAndColumns[ctr + 1 ][ctr2  + 1]].Equals("empty") && counter != 7){
												counter ++;
											}else{
												break;
											}					
										}    						
		        						
		        					}
		        				} 
	        				}
	        			}
	        		}
	        	}
        	}else if(dir == 20 ){

        		if( sideRight.Contains(index) || (index >=210 && index < 225)){
        			counter = 1;
	        	}else{
	        		for(int ctr = 0; ctr < 15; ctr ++){

	        			if(_rowsAndColumns[ctr].Contains(index)){
	        			
		        				for(int inner = 0; inner < 15; inner ++){

		        					if(_rowsAndColumns[ctr][inner] == index){
		        						if(inner == 14){
	        								break;
	        							}else{
											for(int ctr2 = inner ; ctr2 < 14; ctr2 ++){
												
												if(board[_rowsAndColumns[ctr + 1 ][ctr2  + 1]].Equals("empty") && counter != 7){
													counter ++;
												}else{
													break;
												}					
											}    						
		        						}
		        					}
		        				} 
	        			
	        			}
	        		}
	        	}
        	}
        	return counter;

        }

        public void filter(){
        	for(int looper = 0; looper < size ; looper++ ){
        		if(squares[looper].up > 0 && squares[looper].down > 0 && 
        			(squares[looper].diagonal1 > 1 && squares[looper].diagonal2 > 1 && squares[looper].diagonal3 > 1 && squares[looper].diagonal4 > 1) ){
        			
        			squares[looper].desc = "full";
        			squares[looper].align = "vertical";
        			sIndex.Add(looper);
        	
        		}else if(squares[looper].right > 0 && squares[looper].left > 0 && 
        			(squares[looper].diagonal5 > 1 && squares[looper].diagonal6 > 1 && squares[looper].diagonal7 > 1 && squares[looper].diagonal8 > 1)){
        			
        			squares[looper].desc = "full";
        			squares[looper].align = "horizontal";
        			sIndex.Add(looper);

        		}else if(squares[looper].right > 1 && (squares[looper].up == 0 || squares[looper].down == 0 ) && squares[looper].left >= 1 && 
        			(squares[looper].diagonal6 > 1 && squares[looper].diagonal8 > 1) ){
        			
        			squares[looper].desc = "right";
        			squares[looper].align = "horizontal";
        			sIndex.Add(looper);
        		
        		}else if(squares[looper].left > 1 && (squares[looper].up == 0 || squares[looper].down == 0)  && squares[looper].right  >= 1 && 
        			(squares[looper].diagonal5 > 1 && squares[looper].diagonal7 > 1) ){
        			
        			squares[looper].desc = "left";
        			squares[looper].align = "horizontal";
        			sIndex.Add(looper);
        		
        		}else if(squares[looper].up > 1 && (squares[looper].left == 0 || squares[looper].right == 0)  && squares[looper].down >= 1 && 
        			(squares[looper].diagonal1 > 1 && squares[looper].diagonal2 > 1) ){
        			
        			squares[looper].desc = "top";
        			squares[looper].align = "vertical";
        			sIndex.Add(looper);
        		
        		}else if(squares[looper].down > 1 && (squares[looper].left == 0 || squares[looper].right == 0)  && squares[looper].up >= 1 && 
        			(squares[looper].diagonal4 > 1 && squares[looper].diagonal3 > 1) ){
        			
        			squares[looper].desc = "down";
        			squares[looper].align = "vertical";
        			sIndex.Add(looper);
        		}
        	}
        }

        private bool doCheck(List<string> tBaord){
        	List<string> invalid = new List<string>();
        	List<string> listOfWords = new List<string>();
        	for(int x = 0; x < 30; x ++){
					listOfWords.AddRange(checkRowsAndColumns(_rowsAndColumns[x], tBaord));
				}
				Console.WriteLine(": "+ string.Join(".", listOfWords));

				invalid = search.searchForValidity(listOfWords);
				Console.WriteLine("invalid words: "+ string.Join(".", invalid));
				if(invalid.Count == 0){
                     return true;

				}else{
					 return false;
				}
			}
        
        public List<string> checkRowsAndColumns(List<int> _rowsAndColumns, List<string> _board){
           // int[] direction = { -14, 15 , 1, -1};
            int looper;
            int ctr = 0;
            string temp = "";
            List<string> tempStr= new List<string>();

            for(int counter= 0; counter < 15; counter++){
                if(_board[_rowsAndColumns[counter]] != "empty"){
                    looper = counter;
                    while(_board[_rowsAndColumns[looper]] != "empty"){
                      temp = temp + _board[_rowsAndColumns[looper]];
                      
                      if(looper != 14){
                         looper++;
                      }else{
                         break;
                      }
                    }
                    if(temp.Length != 1){
                        tempStr.Add(temp);
                    }
                    temp= "";
                    counter = looper ;
                    ctr++;

                }
                
            }
            return tempStr;            
         } 
        private bool algo(int _index){
        	try{
	       	List<string> gotchaCollection = new List<string>();
	       	List<string> subs = new List<string>();
			int limit = 0;
			 blankTileCtr = 0;
				Console.WriteLine("tiles "+ string.Join(",",_playerTiles ));

			 	for(int ctr2 = 0; ctr2 < 7; ctr2++){
					string sub = "";
		        	
		        	if(_playerTiles[ctr2] == " "){
		        		blankTileCtr++;
		        		sub = "ZQ";
		        	}
		        	else{
			        	if(squares[_index].desc == "full"){

			        		sub = board[squares[_index].flag] + _playerTiles[ctr2];
			        		if(squares[_index].align == "horizontal"){
			        			limit = ((squares[_index].left + squares[_index].right) > 8 )?  8 : (squares[_index].left + squares[_index].right) ;
			        		}else{
			        			limit = squares[_index].up + squares[_index].down;
			        		}

			        	}else if(squares[_index].desc == "right" || squares[_index].desc == "down"){

			        		sub = board[squares[_index].flag] + _playerTiles[ctr2];

			        		if(squares[_index].desc == "right"){
			        			limit = squares[_index].right + 1;
			        		}else{
			        			limit = squares[_index].down + 1;
			        		}


			        	}else if(squares[_index].desc == "left" || squares[_index].desc == "top"){

			        		sub =  _playerTiles[ctr2] + board[squares[_index].flag] ;

			        		if(squares[_index].desc == "left"){
			        			limit = squares[_index].left;
			        		}else{
			        			limit = squares[_index].up;
			        		}

			        	}
			        }
		        	Console.WriteLine(sub +"Indexsd"+ squares[_index].flag);
		        	subs.Add(sub);
		        }
					List<string>[] possibleWords= new List<string>[7];
					possibleWords =  search.findPattern(subs, limit, squares[_index].desc );
					
		        	List<string> tempTiles = new List<string>();
		        	
		        	tempTiles.AddRange(_playerTiles);
			        tempTiles.Add(board[squares[_index].flag]);

		        	Thread t1 = new Thread(()=>{
		        		 one = doThis(possibleWords[0],tempTiles);
		        	});
		        	t1.Start();

		        	Thread t2 = new Thread(()=>{
		        		 two = doThis(possibleWords[1],tempTiles);
		        	});
		        	t2.Start();

		        	Thread t3 = new Thread(()=>{
		        		 three.AddRange(doThis(possibleWords[2],tempTiles));
		        	});
		        	t3.Start();

		        	Thread t4 = new Thread(()=>{
		        		 four = doThis(possibleWords[3],tempTiles);
		        	});
		        	t4.Start();
		        	Thread t5 = new Thread(()=>{
		        		 five.AddRange(doThis(possibleWords[4],tempTiles));
		        	});
		        	t5.Start();

		        	Thread t6 = new Thread(()=>{
		        		 six = doThis(possibleWords[5],tempTiles);
		        	});
		        	t6.Start();
		        	Thread t7 = new Thread(()=>{
		        		 seven.AddRange(doThis(possibleWords[6],tempTiles));
		        	});
		        	t7.Start();


			        t1.Join();
			        t2.Join();
					t3.Join();
			        t4.Join();
					t5.Join();
			        t6.Join();
					t7.Join();
			      
					
		        //	Console.WriteLine("Gotchaa "+ string.Join(",", gotcha));
		        	gotchaCollection.AddRange(one);
	      		 	gotchaCollection.AddRange(two);
	      		 	gotchaCollection.AddRange(three);
	      		 	gotchaCollection.AddRange(four);
	      		 	gotchaCollection.AddRange(five);
	      		 	gotchaCollection.AddRange(six);
	      		 	gotchaCollection.AddRange(seven);
	      		 
	      		Console.WriteLine("gotchaCollection: "+ string.Join(",", gotchaCollection));
      			
      			if(gotchaCollection.Count != 0){
		      		var result = gotchaCollection.OrderBy(x => x.Length);
		      		
		      		gotchaCollection = result.ToList();
	     			bool valid = false;

		      		for(int counter = gotchaCollection.Count - 1; counter >= 0; counter--){
		      			int left =0 ;
		      			int right = 0;
		      			List<int> foundIndex = new List<int>();
		      			        	//Console.WriteLine("asd");
		      			Console.WriteLine(gotchaCollection[counter]);
		      			valid = false;

			      		if(squares[_index].desc == "full"){
			      			for(int counter2 = 0; counter2 < gotchaCollection[counter].Length -1 ; counter2++){
			      				
			      				if(gotchaCollection[counter][counter2].ToString().Equals(board[squares[_index].flag])){
			      					foundIndex.Add(counter2);
			      					Console.WriteLine("POTANFAINAF 0");

			      				}

			      				
			      			}	

			      			
			      			foreach(int ind in foundIndex){
			      				Console.WriteLine("S: "+ ind);
			      				left = ind;
			      				right = gotchaCollection[counter].Length - 1 - ind;
			      				valid = putToBoard(left, right, gotchaCollection[counter], _index);

			      				if(valid){
			      					break;
			      				}

			      			}
			      		}else{

			      			if(squares[_index].desc == "right" || squares[_index].desc == "down"){
			      				left = 0;
			      				right =  gotchaCollection[counter].Length - 1; 
			      			}else if( squares[_index].desc == "left" || squares[_index].desc == "top"){
			      				right = 0;
			      				left = gotchaCollection[counter].Length - 1;
			      				Console.WriteLine("sdf");
			      			}
			      				
			      			valid = putToBoard(left, right, gotchaCollection[counter], _index);

			      			

			      		}
						if(valid){
		      				break;
		      			}

		      		}

		      		if(valid){
		      			return true;
		      		}else{
		      			return false;
		      		}

	      		}else{
	      			return false;
	      		}
	      		}catch(Exception e){
	      			 //Console.WriteLine("Fuck");
	      			 return false;
	      		}

        }
        public List<string> doThis(List<string> foo, List<string> fee){
        		List<string>gotcha = new List<string>();
        		List<string>faa = new List<string>();
        		for(int looper = 0 ; looper < foo.Count; looper ++){
        			string got = "";
        			faa.AddRange(fee);
				    for(int inner = 0; inner < foo[looper].Length; inner ++){
				        if(faa.Contains(foo[looper][inner].ToString())) {
				        	faa.Remove(foo[looper][inner].ToString());
				        	got = got + foo[looper][inner].ToString();
				        	}
				        }
				        faa.Clear();
				        if(got.Length == foo[looper].Length){
				        	gotcha.Add(foo[looper]);

				     }
			 	}
			 	Console.WriteLine("GOUTA"+ string.Join(",", gotcha));
			 	return gotcha;
        }

        private bool putToBoard(int _left, int _right, string word, int index){
        	try{
	        	List<string>tempBoard1 = new List<string>(board);
	      		
	        	int[] coor = new int[2]; 
	        	int adder = (squares[index].align == "horizontal") ? 1 : 15;
	        	
	       		coor = findCoor(squares[index].align, squares[index].flag);
	       		    Console.WriteLine(_left + "s" + _right+ ";" + coor[0] + ";"+ coor[1] + ";" + adder);

	        		
	       			int start = _rowsAndColumns[coor[0]][coor[1] - _left];
	        		foreach(char letter in word){

						if(start == index){
				
							start +=adder;
	        		
	        					continue;
	        			}

						tempBoard1[start] = letter.ToString();
						moves.Add(start);
						letterMoves.Add(tempBoard1[start]);

						start += adder;
						Console.WriteLine(start);

	     			}
					
	        	//}
	        			


	        	bool valid = doCheck(tempBoard1);
	        	Console.WriteLine(valid);
	        	if(valid){
	      			transfer();
	      			transfer2();	
	        		

	        	}else{
	        		letterMoves.Clear();
	        		moves.Clear();
	        	}
	        	return valid;	
	      	}catch(Exception e){
	      		Console.WriteLine("Fail");
	      		return false;
	      	}
        	
        }

        private int[] findCoor(string rowOrColumn, int _index){
        	int[] coor = new int[2];
        	if(rowOrColumn == "horizontal"){
        		for(int looper = 0; looper < 15 ; looper++){
        			if(_rowsAndColumns[looper].Contains(_index)){
        				for(int inner = 0; inner < 15; inner ++){
        					 if(_rowsAndColumns[looper][inner].Equals(_index)){
        					 	coor =  new int[2]{looper, inner};
        					 }

        				}
        			}
        		}
        	}else{
        		for(int looper = 15; looper < 30; looper++){
        			if(_rowsAndColumns[looper].Contains(_index)){
        				for(int inner = 0; inner < 15; inner ++){
        					 if(_rowsAndColumns[looper][inner].Equals(_index)){
        					 	coor =  new int[2]{looper, inner};
        					 }

        				}
        			}
        		}
        	}

        	return coor;
        }

        public List<int> transfer(){
        	List<int> temp101 = new List<int>();
        
        	temp101.AddRange(moves);
        	return temp101;
        } 
        public List<string> transfer2(){
        	List<string> temp101 = new List<string>();
        	temp101.AddRange(letterMoves);

        	return temp101;
        }
       
    }
}
        		