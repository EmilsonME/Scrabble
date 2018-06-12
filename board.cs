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
using System.Text.RegularExpressions; 

namespace Iskrabol{
    /// <summary>
    /// 
    /// Memmory class for board.
    /// Manages in-game board data as the game progresses.
    /// 
    /// </summary>
    class Board : Form
    {
       // private Timer timerHuman;
      //  private Timer timerAI;
        public squareData[] squares = new squareData[225];
        public Panel boardUI,humanRack, whoToPlayFirstPanel;
        public Panel[] panel = new Panel[7];
        public humanTiles[] humanLetter = new humanTiles[7];
        public List<string> rackTiles, boardMemoryCopy,allWords, tLetMultiplier, dLetMultiplier, blankTile, tileBag;
        public List<int> playingTiles, arrTempIndex, tWordMultiplier, dWordMultiplier, aiMoveIndices;
        public Button  playButton, drawButton, okButton, passButton, undoButton;
        public Label lblGetTiles, pbHuman, pbComputer, lblInput, lblTotal, lblHuman, lblComputer, lblHScore, lblCScore,lblStatus, tilesLeft;
        public TextBox letterInput;
        public PictureBox gif;
        public bool okButtonIsClick;
        public DataTable table;
        public DataGridView dataGridView;

        private Stack<int> tripleWordIndices = new Stack<int>(new[] { 224, 217, 210, 119, 105, 14, 7, 0 });                                                                        
        private Stack<int> tripleLetterIndices = new Stack<int>(new []{204, 200, 148, 144, 140, 136, 88, 84, 80, 76, 24, 20});
        private Stack<int> doubleWordIndices = new Stack<int>(new []{208, 196, 192, 182, 176, 168, 160, 154, 70, 64, 56, 48, 42, 32, 28, 16});
        private Stack<int> doubleLetterIndices = new Stack<int>(new[] { 221, 213, 188, 186, 179, 172, 165, 132, 128, 126, 122, 116, 108, 102, 98, 96, 92, 59, 52, 45, 38, 36, 11, 3 });

        public int  tempIndex, indexer, turnCounter;
        public static int  hTotalScore = 0;
        public static int cTotalScore = 0;
        public Tile newTiles;
        public Search search;
        public AI ai;
        public bool firstTurn = true;
        Form form;

       
        public List<int>[] rowsAndColumns = new List<int>[30];

        public struct squareData
        {
            public PictureBox pictureBox{ get; set;}
            public Label label { get; set; }
            public bool isOccupied { get; set; }
            public int squarePoints { get; set; }
            public string boardMemory { get; set; }
        }
        public struct humanTiles{
            public PictureBox pictureBox2{ get; set;}
            public string letter {get; set;}
            public int index {get; set;}


        }

        public Board(){
            InitializeComponent();

        }
       
      
        private void InitializeComponent()
        {
            try{
                this.Size       = new Size(1130,740);
                this.StartPosition = FormStartPosition.CenterScreen;
                this.BackColor = Color.FromArgb(233, 234, 220);

                boardUI = new Panel();
                boardUI.BackColor = Color.FromArgb(200,200,200);
                boardUI.SetBounds(40,40, 650,650);

                humanRack = new Panel();
                //humanRack.BackColor =Color.FromArgb(12, 122, 11);
                humanRack.BackgroundImage = Image.FromFile("Tiles/rack.png");
                humanRack.SetBounds(705, 530, 380, 65);
                humanRack.Enabled = false; 

                int xPos2 = 15;
                for(int x =0; x < 7; x++){
                    panel[x] = new Panel();
                    panel[x].SetBounds(xPos2,0, 48,48);
                    panel[x].BackColor = Color.FromArgb(100, 88, 44, 55);

                    humanRack.Controls.Add(panel[x]);
                    xPos2 += 50;

                }
               
                lblStatus = new Label();
                lblStatus.SetBounds(740, 350, 340, 115);
                lblStatus.Font = new Font("Consolas", 25, FontStyle.Bold);


                //gif = new PictureBox();
              //  gif.SetBounds(932, 335, 120, 115);

                
                playButton = new Button();
                playButton.BackgroundImage = Image.FromFile("Tiles/playButton normal.png");
                playButton.Click += new EventHandler(playTileButtonClick);
                playButton.MouseEnter += new EventHandler(mouseEnter);
                playButton.MouseLeave += new EventHandler(mouseLeave);
                playButton.FlatStyle = FlatStyle.Flat;
                playButton.SetBounds(824, 445, 160, 65);
                playButton.Enabled = false; 

                undoButton = new Button();
                undoButton.BackgroundImage = Image.FromFile("Tiles/undo normal.png");
                undoButton.Click += new EventHandler(undoButtonClick);
                undoButton.MouseEnter += new EventHandler(mouseEnter);
                undoButton.MouseLeave += new EventHandler(mouseLeave);
                undoButton.FlatStyle = FlatStyle.Flat;
                undoButton.SetBounds(987, 445, 74, 65);
                undoButton.Enabled = false;

                passButton = new Button();
                passButton.BackgroundImage = Image.FromFile("Tiles/pass normal.png");
                passButton.Click += new EventHandler(playTileButtonClick);
                passButton.MouseEnter += new EventHandler(mouseEnter);
                passButton.MouseLeave += new EventHandler(mouseLeave);
                passButton.FlatStyle = FlatStyle.Flat;
                passButton.SetBounds(747, 445, 74, 65);
                passButton.Enabled = false;

                whoToPlayFirstPanel = new Panel();
                whoToPlayFirstPanel.SetBounds( 732, 50, 342, 183 );
                whoToPlayFirstPanel.BackColor = Color.LightBlue;

                lblGetTiles = new Label();
                lblGetTiles.Text = "The player with the letter closest to 'A' plays First but the Blank Tile beats all letters";                
                lblGetTiles.SetBounds(70, 10, 215, 40);

                drawButton = new Button();
                drawButton.Text = "Get tiles";
                drawButton.SetBounds(113, 145, 115, 35);
                drawButton.FlatStyle = FlatStyle.Flat;
                drawButton.Click += new EventHandler(whoToPlayFirst);

                pbHuman = new Label();
                pbHuman.SetBounds(60, 55, 80,85 );
                pbHuman.BackgroundImage = Image.FromFile("Tiles/Scrabble/xx.jpg");
                pbHuman.Font = new Font("Consolas", 60,FontStyle.Bold);
                pbHuman.TextAlign = ContentAlignment.MiddleCenter;

                pbComputer = new Label();
                pbComputer.SetBounds(180, 55, 80,85 );
                pbComputer.BackgroundImage = Image.FromFile("Tiles/Scrabble/xx.jpg");
                pbComputer.Font = new Font("Consolas", 60,FontStyle.Bold);
                pbComputer.TextAlign = ContentAlignment.MiddleCenter;


                lblInput = new Label();
                lblInput.Text = "Input the letter of the blank tile.";
                lblInput.Font = new Font("Segoe UI", 14, FontStyle.Bold);
                lblInput.SetBounds(20, 20, 600, 35);

                okButton = new Button();
                okButton.Text = "OK";
                okButton.FlatStyle = FlatStyle.Flat;
                okButton.Click += new EventHandler(okButtonClicked);
                okButton.SetBounds(143, 100, 60, 30);


                letterInput = new TextBox();
                letterInput.Text = "Enter a letter.";
                letterInput.SetBounds(130, 70, 90, 100);

                
                table = new DataTable();
                table.Columns.Add("Turn");
                table.Columns.Add("You").DataType=typeof(int);
                table.Columns.Add("Computer").DataType=typeof(int);

                lblTotal = new Label();
                lblTotal.Text = "Total";
                lblTotal.Font = new Font("Consolas", 14,FontStyle.Bold);
                lblTotal.SetBounds(732, 260, 70, 20);
                
                lblHuman = new Label();
                lblHuman.Text = "You:";
                lblHuman.Font = new Font("Consolas", 14,FontStyle.Bold);
                lblHuman.SetBounds(800, 260, 50, 35);

                lblHScore = new Label();
                lblHScore.Text = "0";
                lblHScore.Font = new Font("Consolas", 14,FontStyle.Bold);
                lblHScore.SetBounds(850, 260, 50, 35);

                lblComputer = new Label();
                lblComputer.Text = "Computer:";
                lblComputer.Font = new Font("Consolas", 14,FontStyle.Bold);
                lblComputer.SetBounds(905, 260, 104, 35);

                lblCScore = new Label();
                lblCScore.Text = "0";
                lblCScore.Font = new Font("Consolas", 14,FontStyle.Bold);
                lblCScore.SetBounds(1005, 260, 50, 35);

                tilesLeft = new Label();
                tilesLeft.Text = "Tiles Left: 100";
                tilesLeft.Font = new Font("Consolas", 14, FontStyle.Bold);
                tilesLeft.SetBounds(732, 290, 170, 35);

                for(int i = 0; i < 50; i++)
                {
                     DataRow row = table.NewRow();
                 
                     table.Rows.Add(row);
                 }

                dataGridView = new DataGridView();
                dataGridView.DataSource = table;
                dataGridView.Size = new Size(342,200);


                form = new Form();
                form.Size = new Size(355,190);
                form.StartPosition = FormStartPosition.CenterScreen;
                form.Controls.Add(lblInput);
                form.Controls.Add(letterInput);
                form.Controls.Add(okButton);

                for(int counter = 0; counter < 7; counter++){
                    humanLetter[counter].pictureBox2 = new PictureBox();
                    humanLetter[counter].pictureBox2.BackColor = Color.FromArgb(153, 102, 51);
                    humanLetter[counter].pictureBox2.SetBounds(3,3, 43,43);
                    humanLetter[counter].pictureBox2.DragEnter += new DragEventHandler(dragEnter);
                    humanLetter[counter].pictureBox2.MouseDown += new MouseEventHandler(mouseDown);
                    humanLetter[counter].pictureBox2.DragDrop += new DragEventHandler(dragDrop);
                    humanLetter[counter].pictureBox2.MouseEnter += new EventHandler(mouseEnter);
                    humanLetter[counter].pictureBox2.MouseLeave += new EventHandler(mouseLeave);

                 //   humanLetter[counter].pictureBox2.AllowDrop = true;
                    humanLetter[counter].pictureBox2.Tag = counter; 


                    panel[counter].Controls.Add(humanLetter[counter].pictureBox2);

                }
                for(int ctr = 0; ctr < 30; ctr++){
                    rowsAndColumns[ctr] = new List<int>();
                }

                this.Controls.Add(boardUI);
                this.Controls.Add(humanRack);
               // this.Controls.Add(getTilesButton);
                this.Controls.Add(playButton);
                this.Controls.Add(undoButton); 
                this.Controls.Add(passButton);
                this.Controls.Add(tilesLeft);
                this.Controls.Add(lblStatus);
              //  this.Controls.Add(gif);                

                this.loadBoard();

                this.Controls.Add(whoToPlayFirstPanel);
                whoToPlayFirstPanel.Controls.Add(lblGetTiles);
                whoToPlayFirstPanel.Controls.Add(drawButton);
                whoToPlayFirstPanel.Controls.Add(pbComputer);
                whoToPlayFirstPanel.Controls.Add(pbHuman);

                //this.Controls.Add(lblTotal);
                this.Controls.Add(lblHuman);
                this.Controls.Add(lblHScore);
                this.Controls.Add(lblComputer);
                this.Controls.Add(lblCScore);
                


                rackTiles = new List<string>();
                boardMemoryCopy = new List<string>();
                allWords = new List<string>();

                tWordMultiplier  = new List<int>();
                dWordMultiplier = new List<int>();
                tLetMultiplier = new List<string>();
                dLetMultiplier = new List<string>();
                blankTile = new List<string>();
                playingTiles =  new List<int>();
                arrTempIndex  = new List<int>();
                aiMoveIndices = new List<int>();

                newTiles = new Tile();
                search = new Search();
                tileBag = new List<string>();
                tileBag.AddRange(newTiles.bag());
                turnCounter = 0;



            }catch(Exception ej){
                MessageBox.Show(ej.Message); 
            }


        }
      

        private void loadBoard()
        {
            try{
                // indexes that tells the board walker to move on to a new row
                Stack<int> traverser = new Stack<int>(new int[] { 224, 209, 194, 179, 164, 149, 134, 119, 104, 89, 74, 59, 44, 29, 14 });

                int xPos = 0; // column walker
                int yPos = 0; // row walker
                
                for(int boardWalker = 0; boardWalker < 225; boardWalker++){

                    squares[boardWalker].pictureBox = new PictureBox();
                    squares[boardWalker].pictureBox.SetBounds(xPos,yPos,43,43);
                    squares[boardWalker].pictureBox.AllowDrop = true;
                    squares[boardWalker].pictureBox.DragEnter += new DragEventHandler(dragEnter);
                    squares[boardWalker].pictureBox.DragDrop += new DragEventHandler(dragDrop);

                    squares[boardWalker].pictureBox.Tag = boardWalker.ToString();
                    squares[boardWalker].pictureBox.BackgroundImage = Image.FromFile("Tiles/board/Board.jpg");
                    squares[boardWalker].squarePoints = 0;

                    squares[boardWalker].boardMemory = "empty";

                    boardUI.Controls.Add(squares[boardWalker].pictureBox);

                    if( boardWalker == traverser.Peek() ){
                        traverser.Pop();
                        yPos += 43;
                        xPos = 0;
                    }
                    else{
                        xPos += 43;
                    }

                    if(boardWalker == 112){
                       squares[boardWalker].pictureBox.BackgroundImage = Image.FromFile("Tiles/board/Star.jpg");

                    }
                    if (tripleWordIndices.Count != 0 && boardWalker == tripleWordIndices.Peek() ){
                        squares[boardWalker].pictureBox.BackgroundImage = Image.FromFile("Tiles/board/TripleWord.jpg");
                        tripleWordIndices.Pop();
                        squares[boardWalker].squarePoints = 4;
                    }

                    if (doubleWordIndices.Count != 0 && boardWalker == doubleWordIndices.Peek()){
                        squares[boardWalker].pictureBox.BackgroundImage = Image.FromFile("Tiles/board/DoubleWord.jpg");
                        doubleWordIndices.Pop();
                        squares[boardWalker].squarePoints = 2;

                    }

                    if (doubleLetterIndices.Count != 0 && boardWalker == doubleLetterIndices.Peek()){
                        squares[boardWalker].pictureBox.BackgroundImage = Image.FromFile("Tiles/board/DoubleLetter.jpg");
                        doubleLetterIndices.Pop();
                        squares[boardWalker].squarePoints = 1;
                    }

                    if (tripleLetterIndices.Count != 0 && boardWalker == tripleLetterIndices.Peek()){
                        squares[boardWalker].pictureBox.BackgroundImage = Image.FromFile("Tiles/board/TripleLetter.jpg");
                        tripleLetterIndices.Pop();
                        squares[boardWalker].squarePoints = 3;
                    }

                    if(boardWalker >= 0 && boardWalker < 15){
                        rowsAndColumns[0].Add(boardWalker);
                    }

                    else if(boardWalker >= 15 && boardWalker < 30){
                        rowsAndColumns[1].Add(boardWalker);
                    }

                    else if(boardWalker >= 30 && boardWalker < 45){
                        rowsAndColumns[2].Add(boardWalker);
                    }

                    else if(boardWalker >= 45 && boardWalker < 60){
                        rowsAndColumns[3].Add(boardWalker);
                    }

                    else if(boardWalker >= 60 && boardWalker < 75){
                        rowsAndColumns[4].Add(boardWalker);
                    }

                    else if(boardWalker >= 75 && boardWalker < 90){
                        rowsAndColumns[5].Add(boardWalker);
                    }

                    else if(boardWalker >= 90 && boardWalker < 105){
                        rowsAndColumns[6].Add(boardWalker);
                    }

                    else if(boardWalker >= 105 && boardWalker < 120){
                        rowsAndColumns[7].Add(boardWalker);
                    }
                    else if(boardWalker >= 120 && boardWalker < 135){
                        rowsAndColumns[8].Add(boardWalker);
                    }

                    else if(boardWalker >= 135 && boardWalker < 150){
                        rowsAndColumns[9].Add(boardWalker);
                    }

                    else if(boardWalker >= 150 && boardWalker < 165){
                        rowsAndColumns[10].Add(boardWalker);
                    }

                    else if(boardWalker >= 165 && boardWalker < 180){
                        rowsAndColumns[11].Add(boardWalker);
                    }

                    else if(boardWalker >= 180 && boardWalker < 195){
                        rowsAndColumns[12].Add(boardWalker);
                    }

                    else if(boardWalker >= 195 && boardWalker < 210){
                        rowsAndColumns[13].Add(boardWalker);
                    }

                    else if(boardWalker >= 210 && boardWalker < 225){
                        rowsAndColumns[14].Add(boardWalker);
                    }
                   

                }
                for(int outer = 0; outer < 15; outer++){
                    for(int inner  = 0; inner <15; inner++){
                        rowsAndColumns[inner + 15].Add(rowsAndColumns[outer][inner]);
                    }
                }


            }catch(Exception e){
                MessageBox.Show(e.Message);
            }


        }
      
        public void putToRack(){
            passButton.Enabled = true;
            if(tileBag.Count != 0){
                tileBag = newTiles.getLetters(7 - rackTiles.Count, tileBag);
                Console.WriteLine("-" + string.Join(",", newTiles.rackLetters));
                rackTiles.AddRange(newTiles.rackLetters);
     
                for(int counter= 0; counter< rackTiles.Count; counter++){

                        humanLetter[counter].pictureBox2.BackgroundImage = Image.FromFile("Tiles/Scrabble/"+ tileImage(rackTiles[counter]));
                        humanLetter[counter].letter = rackTiles[counter];
                          //Console.WriteLine(board.humanLetter[counter]);  
                }
                tilesLeft.Text = "Tiles Left: " + tileBag.Count;
            }else{
                winnerChecker();
            }
           
        }
        private void whoToPlayFirst(Object source, EventArgs evt){
            ai = new AI();
            List<string> temp = new List<string>();
            List<string> aiMove = new List<string>();

            newTiles.getLetters(1, tileBag);
            temp.Add(newTiles.rackLetters[0]);
            tileBag.Add(newTiles.rackLetters[0]);
            pbHuman.Text = newTiles.rackLetters[0];
            newTiles.getLetters(1, tileBag);
            temp.Add(newTiles.rackLetters[0]);
            tileBag.Add(newTiles.rackLetters[0]);
            pbComputer.Text = newTiles.rackLetters[0];


            temp.Sort();
            putToRack();
            //Console.WriteLine("temp {0}", string.Join(",", temp));
            if(pbHuman.Text == " "){
                lblStatus.Text = "   Your Turn.";
                lblStatus.ForeColor = Color.FromArgb(0,0,255);

                MessageBox.Show("You got the Blank Tile!. You play first.");
            }else if(temp[0] == pbHuman.Text) {
                lblStatus.Text = "   Your Turn.";
                lblStatus.ForeColor = Color.FromArgb(0,0 ,255);

                MessageBox.Show("You got " + pbHuman.Text + ". You play first.");
            }else if(temp[0] == temp[1]){
                MessageBox.Show("You two got same letters. Try it again");
            }else if(pbComputer.Text == " "){
               lblStatus.Text = "Computer's Turn.";
               lblStatus.ForeColor = Color.FromArgb(255,0,0);
 
               MessageBox.Show("Computer got the Blank Tile!. Computer plays first.");
               tileBag = ai.fillRacks(tileBag);
               aiMove = ai.firstMover();
               changeBoard(aiMove);
               

            }else{
                lblStatus.Text = "Computer's Turn.";
                lblStatus.ForeColor = Color.FromArgb(255,0,0);

                MessageBox.Show("Computer got " + pbComputer.Text + ". Computer plays first.");
                tileBag = ai.fillRacks(tileBag);
                aiMove = ai.firstMover();
                changeBoard(aiMove);
                 
            }
         
            humanRack.Enabled = true;
            whoToPlayFirstPanel.Controls.Clear();
            whoToPlayFirstPanel.Controls.Add(dataGridView);
            
        }

        private void playTileButtonClick(object source, EventArgs evt){
            if(source == playButton){
                playButton.BackgroundImage = Image.FromFile("Tiles/playButton clicked.png");
            }else if(source == passButton){
                passButton.BackgroundImage = Image.FromFile("Tiles/pass clicked.png");

            }
                playButton.Enabled = false;
                undoButton.Enabled = false;
                 foreach(int temp in arrTempIndex){
                    humanLetter[temp].pictureBox2.Enabled = true;
                }
                checkWordValidity();
            


        }

        private void undoButtonClick(object source, EventArgs evt){
            undoButton.BackgroundImage = Image.FromFile("Tiles/undo clicked.png");

            if(arrTempIndex.Count != 0 && playingTiles.Count != 0){
                humanLetter[arrTempIndex[arrTempIndex.Count-1]].letter = squares[playingTiles[playingTiles.Count-1]].boardMemory;
                humanLetter[arrTempIndex[arrTempIndex.Count-1]].pictureBox2.BackgroundImage  = Image.FromFile("Tiles/Scrabble/"+ tileImage(squares[playingTiles[playingTiles.Count-1]].boardMemory)); 
                humanLetter[arrTempIndex[arrTempIndex.Count-1]].pictureBox2.Enabled = true;
                squares[playingTiles[playingTiles.Count-1]].pictureBox.Image = null;
                squares[playingTiles[playingTiles.Count-1]].pictureBox.AllowDrop = true;
                squares[playingTiles[playingTiles.Count-1]].boardMemory = "empty";
                arrTempIndex.RemoveAt(arrTempIndex.Count-1);
                playingTiles.RemoveAt(playingTiles.Count-1);
            }
            if(playingTiles.Count == 0 && arrTempIndex.Count == 0){
                undoButton.Enabled = false;
                passButton.Enabled = true;
            }


        }

        private void okButtonClicked(object source, EventArgs evt){
            string pattern = @"^[a-zA-Z]+$";
            Regex regex = new Regex(pattern);
            
            if(letterInput.Text.Length == 1 && regex.IsMatch(letterInput.Text) == true) {
                squares[indexer].boardMemory = letterInput.Text.ToUpper();
               // okButtonIsClick = false;
              //  blankTile.Add(squares[indexer].boardMemory.ToUpper());
                rackTiles.Remove(" ");
                form.Hide();
                
            }
            else{
                MessageBox.Show("Please enter 1 letter only.");
            }
        }
        
     
        public void mouseLeave(object source, EventArgs e){
         // panel[tempIndex].BackColor = Color.FromArgb(12, 122, 11);
             if (source == humanLetter[0].pictureBox2 || source == humanLetter[1].pictureBox2 || source == humanLetter[2].pictureBox2 ||
                source == humanLetter[3].pictureBox2  || source == humanLetter[4].pictureBox2 || source == humanLetter[5].pictureBox2 ||
                source == humanLetter[6].pictureBox2 )
              {
                panel[tempIndex].BackColor = Color.FromArgb(100, 88, 44, 55);

            }else if(source == playButton){

                playButton.BackgroundImage = Image.FromFile("Tiles/playButton normal.png");

            }else if(source == passButton){

                passButton.BackgroundImage = Image.FromFile("Tiles/pass normal.png");

            }else if(source == undoButton){

                undoButton.BackgroundImage = Image.FromFile("Tiles/undo normal.png");

            }

        }
        public void mouseEnter(object source, EventArgs e){
            if (source == humanLetter[0].pictureBox2 || source == humanLetter[1].pictureBox2 || source == humanLetter[2].pictureBox2 ||
                source == humanLetter[3].pictureBox2  || source == humanLetter[4].pictureBox2 || source == humanLetter[5].pictureBox2 ||
                source == humanLetter[6].pictureBox2 )
            {
                    PictureBox sender = source as PictureBox;
                    tempIndex = Int32.Parse(sender.Tag.ToString());
                    if(       humanLetter[tempIndex].pictureBox2.BackgroundImage != null){
                        panel[tempIndex].BackColor = Color.FromArgb(12, 122, 11);
                    }
            }else if(source == playButton){

                playButton.BackgroundImage = Image.FromFile("Tiles/playButton hover.png");

            }else if(source == passButton){

                passButton.BackgroundImage = Image.FromFile("Tiles/pass hover.png");

            }else if(source == undoButton){

                undoButton.BackgroundImage = Image.FromFile("Tiles/undo hover.png");

            }
        }

        public void mouseDown(object source, MouseEventArgs e){
            PictureBox sender = source as PictureBox;
            DoDragDrop(sender.BackgroundImage, DragDropEffects.Copy);
            
        
        }
        public void dragEnter(object source, DragEventArgs e){
            if(e.Data.GetDataPresent(DataFormats.Bitmap)){
                    e.Effect = DragDropEffects.Copy;
            }else{
                    e.Effect = DragDropEffects.None;
            }
            
        }
        public void dragDrop(object source, DragEventArgs e){

            PictureBox destination = source as PictureBox;
            destination.Image = (Bitmap)e.Data.GetData(DataFormats.Bitmap);
            
            if(rackTiles[tempIndex] == " "){
               
                form.Show();
                
                indexer = Convert.ToInt32(destination.Tag.ToString());           
            }else{
                squares[Convert.ToInt32(destination.Tag.ToString())].boardMemory = rackTiles[tempIndex];

            }
            playingTiles.Add(Convert.ToInt32(destination.Tag));
            arrTempIndex.Add(tempIndex);
            squares[Convert.ToInt32(destination.Tag.ToString())].pictureBox.AllowDrop = false;
            humanLetter[tempIndex].pictureBox2.Enabled  = false;
            removeTileFromRack(tempIndex);
            playButton.Enabled = true;
            undoButton.Enabled = true;
            passButton.Enabled = false;
            
            if( squares[Convert.ToInt32(destination.Tag.ToString())].squarePoints != 0){
                if(squares[Convert.ToInt32(destination.Tag.ToString())].squarePoints == 4){

                     tWordMultiplier.Add(Convert.ToInt32(destination.Tag.ToString()));

                }else if(squares[Convert.ToInt32(destination.Tag.ToString())].squarePoints == 2){

                    dWordMultiplier.Add(Convert.ToInt32(destination.Tag.ToString()));
                }
                else if( squares[Convert.ToInt32(destination.Tag.ToString())].squarePoints == 1){
                    
                    dLetMultiplier.Add(squares[Convert.ToInt32(destination.Tag.ToString())].boardMemory);
                
                }else if(squares[Convert.ToInt32(destination.Tag.ToString())].squarePoints == 3){
                  
                    tLetMultiplier.Add(squares[Convert.ToInt32(destination.Tag.ToString())].boardMemory);
               
                }
            }
           

        }

        public void thread(){
            Thread t = new Thread(new ThreadStart(loadBoard));
            t.SetApartmentState(ApartmentState.STA);
            t.Start( );    
        }

        public void changeBoard(List<string> moves){
            List<string> _word = new List<string>();
            string _best = "";
            _best = moves.Aggregate("", (max, cur) => max.Length > cur.Length ? max : cur);
            Console.WriteLine(_best);
            int squareIndex = (_best.Length >= 6) ? 109 : 111;
            for(int ctr = 0; ctr < _best.Length; ctr++){

                squares[squareIndex].boardMemory = _best[ctr].ToString();;
                squares[squareIndex].pictureBox.AllowDrop = false;
                Console.WriteLine(squares[squareIndex].boardMemory);
                squares[squareIndex].pictureBox.Image  = Image.FromFile("Tiles/Scrabble/"+ tileImage(_best[ctr].ToString())); 

                squareIndex++;
            }

            _word.Add(_best);
            allWords.Add(_best);
            int score = scoreMaker(_word, "ai");
            displayScore(score , "ai", turnCounter++);
            firstTurn = false;

            putToRack();
            ai.updateTiles(_best);
            lblStatus.Text = "   Your Turn.";
            lblStatus.ForeColor = Color.FromArgb(0,0 ,255);
            Thread thread = new Thread( delegate(){
                    ai.generateValidWords();
            });
      
        }
        

        public string tileImage(string letter){
            switch(letter){
                case "A"    : return "A.jpg";
                case "B"    : return "B.jpg";
                case "C"    : return "C.jpg";
                case "D"    : return "D.jpg";
                case "E"    : return "E.jpg";
                case "F"    : return "F.jpg";
                case "G"    : return "G.jpg";
                case "H"    : return "H.jpg";
                case "I"    : return "I.jpg";
                case "J"    : return "J.jpg";
                case "K"    : return "K.jpg";
                case "L"    : return "L.jpg";
                case "M"    : return "M.jpg";
                case "N"    : return "N.jpg";
                case "O"    : return "O.jpg";
                case "P"    : return "P.jpg";
                case "Q"    : return "Q.jpg";
                case "R"    : return "R.jpg";
                case "S"    : return "S.jpg";
                case "T"    : return "T.jpg";
                case "U"    : return "U.jpg";
                case "V"    : return "V.jpg";
                case "W"    : return "W.jpg";
                case "X"    : return "X.jpg";
                case "Y"    : return "Y.jpg";
                case "Z"    : return "Z.jpg";
                case " "    : return "(BLANK).jpg";
                default     : return "A.jpg";    

            }
         }
        public void removeTileFromRack(int index){
          humanLetter[index].pictureBox2.BackgroundImage = null;
          humanLetter[index].letter = null;

        }
        public void checkWordValidity(){
            List<string> listOfWords = new List<string>();
            List<string> invalidWords = new List<string>();
            List<string> newWords = new List<string>();


           
                int _ctr = 0;                    
                    if(firstTurn == true && !playingTiles.Contains(112)  ){
                        MessageBox.Show("Invalid Move");                               
                        Console.WriteLine("Asdff");
                        foreach(int invalid in playingTiles){
                            humanLetter[arrTempIndex[_ctr]].pictureBox2.BackgroundImage = Image.FromFile("Tiles/Scrabble/"+ tileImage(rackTiles[arrTempIndex[_ctr]]));
                             Console.WriteLine("Asdffss");

                            squares[invalid].boardMemory = "empty";
                            squares[invalid].pictureBox.Image = null;
                            squares[invalid].pictureBox.AllowDrop = true;
                            passButton.Enabled =true;   
                            _ctr ++;
                            
                        }
                        
                    }
                     //bool check = checkWordValidity(playingTiles);
                    bool check = true;
                    if(check == false){
                        _ctr = -1;
                       foreach(int invalid in playingTiles){
                                humanLetter[arrTempIndex[++_ctr]].pictureBox2.BackgroundImage = Image.FromFile("Tiles/Scrabble/"+ tileImage(rackTiles[arrTempIndex[_ctr]]));
                                squares[invalid].boardMemory = "empty";
                                squares[invalid].pictureBox.Image = null; 
                                squares[invalid].pictureBox.AllowDrop = true;
                                passButton.Enabled =true;                          
                        }
                        Console.WriteLine("as");
                        MessageBox.Show("Invalid Move");


                    } else{
                        for(int ctr = 0; ctr < 30; ctr++){
                            listOfWords.AddRange(checkRowsAndColumns(rowsAndColumns[ctr]));
                        }
                        
                      

                        invalidWords = search.searchForValidity(listOfWords);  
                        _ctr = -1;
                
                        if(invalidWords.Count != 0){
                            Console.WriteLine("inasdddf");
                            foreach(int invalid in playingTiles){
                                humanLetter[arrTempIndex[++_ctr]].pictureBox2.BackgroundImage = Image.FromFile("Tiles/Scrabble/"+ tileImage(squares[invalid].boardMemory));
                                squares[invalid].boardMemory = "empty";
                                squares[invalid].pictureBox.Image = null; 
                                squares[invalid].pictureBox.AllowDrop = true;
                                passButton.Enabled =true;                          
                            }
                            MessageBox.Show("Invalid Word(s): \n" + string.Join("\n",invalidWords));


                        }else{

                            lblStatus.Text = "Computer's turn.";
                            lblStatus.ForeColor = Color.FromArgb(255,0,0);

                            foreach(string word in listOfWords){
                                if(!allWords.Contains(word)){
                                    Console.WriteLine("whaa"+ word);
                                    allWords.Add(word);
                                    newWords.Add(word);
                                }
                            }
                            
                            foreach(int index in playingTiles){
                                rackTiles.Remove(squares[index].boardMemory);
                                Console.WriteLine("INDEXXX" + index);
                                squares[index].pictureBox.AllowDrop = false;
                            }

                            int turnScore = scoreMaker(newWords, "human");
                            firstTurn = false;
                            tLetMultiplier.Clear();
                            dLetMultiplier.Clear(); 
                            Console.WriteLine(turnScore);
                            displayScore(turnScore, "human",turnCounter++);

                            for(int looper = 0; looper < 225; looper++){
                                boardMemoryCopy.Add(squares[looper].boardMemory);
                            }  

                            tileBag = ai.fillRacks(tileBag);
                            ai.makeMove(boardMemoryCopy, allWords);
                            List<int> temp101 = new List<int>();
                            List<string> temp102 = new List<string>();

                            temp101  = ai.transfer();
                            temp102 = ai.transfer2();
                          
                            displayToBoard(temp101 , temp102 );
                            putToRack();
                            boardMemoryCopy.Clear();
                        }
                        arrTempIndex.Clear();
                        playingTiles.Clear();
                        
                        tWordMultiplier.Clear();
                        dWordMultiplier.Clear();
                    }


        }
         
        public List<string> checkRowsAndColumns(List<int> _rowsAndColumns){
           // int[] direction = { -15, 15 , 1, -1};
            int looper;
            int ctr = 0;
            string temp = "";
            List<string> tempStr= new List<string>();

            for(int counter= 0; counter < 15; counter++){
                if(squares[_rowsAndColumns[counter]].boardMemory != "empty"){
                    looper = counter;
                    while(squares[_rowsAndColumns[looper]].boardMemory != "empty"){
                      temp = temp + squares[_rowsAndColumns[looper]].boardMemory;
                      
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
        public bool checkWordValidity(List<int> plynTiles){
            int _ctr1= 0;
            int _ctr2 = 0;
            
            int[] direction = { -15, -1, 15 , 1};
            plynTiles.Sort();
           
            if(plynTiles.Count > 1){
                for(int x = plynTiles.Count - 1; x >0 ; x--){
                    _ctr1 += plynTiles[x]  - plynTiles[x-1];
                }
            }
            //Console.WriteLine("check"+_ctr1+ "   ddd " + (15 * (plynTiles.Count-1)) );
            if(_ctr1 == 15 * (plynTiles.Count-1) || _ctr1 == plynTiles.Count - 1){
                return true;
            }else
                return false;

        }
        
        public int scoreMaker(List<string> word, string player){
            int score = 0;
            Console.WriteLine("tlet: "+ string.Join(",", tLetMultiplier));
            Console.WriteLine("dlet: "+ string.Join(",", dLetMultiplier));

                foreach(string _word in word){
                    Console.WriteLine("WROD: " + _word);
                    int tempScore = 0;
                    for(int looper = 0; looper < _word.Length; looper++){
                        if(newTiles.onePointer.Contains(_word[looper].ToString())) {
                            if(tLetMultiplier.Contains(_word[looper].ToString())){
                                tempScore += 3; // 1 * 3 == 3;
                                tLetMultiplier.RemoveAt(tLetMultiplier.IndexOf(_word[looper].ToString()));
                            }else if(dLetMultiplier.Contains(_word[looper].ToString())){
                                tempScore += 2; // 1 * 2 == 2;
                                dLetMultiplier.RemoveAt(dLetMultiplier.IndexOf(_word[looper].ToString()));
                            }
                            else{
                                tempScore += 1;
                            }
                        }else if(newTiles.twoPointer.Contains(_word[looper].ToString()))  {
                            if(tLetMultiplier.Contains(_word[looper].ToString())){
                                tempScore += 6;// 2 * 3 == 6;
                                tLetMultiplier.RemoveAt(tLetMultiplier.IndexOf(_word[looper].ToString()));
                            }else if(dLetMultiplier.Contains(_word[looper].ToString())){
                                tempScore += 4; // 2 * 2 == 4;
                                dLetMultiplier.RemoveAt(dLetMultiplier.IndexOf(_word[looper].ToString()));
                            }else{
                                tempScore += 2;
                            }
                        }else if(newTiles.threePointer.Contains(_word[looper].ToString())) {
                            if(tLetMultiplier.Contains(_word[looper].ToString())){
                                tempScore += 9;// 3 * 3 == 9;
                                tLetMultiplier.RemoveAt(tLetMultiplier.IndexOf(_word[looper].ToString()));
                            }else if(dLetMultiplier.Contains(_word[looper].ToString())){
                                tempScore += 6; // 3 * 2 == 6;
                                dLetMultiplier.RemoveAt(dLetMultiplier.IndexOf(_word[looper].ToString()));
                            }else{
                                tempScore += 3;
                            }
                            
                        }else if(newTiles.fourPointer.Contains(_word[looper].ToString())) {
                            if(tLetMultiplier.Contains(_word[looper].ToString())){
                                tempScore += 12;// 4 * 3 == 12;
                                tLetMultiplier.RemoveAt(tLetMultiplier.IndexOf(_word[looper].ToString()));
                            }else if(dLetMultiplier.Contains(_word[looper].ToString())){
                                tempScore += 8; // 4 * 2 == 8;
                                dLetMultiplier.RemoveAt(dLetMultiplier.IndexOf(_word[looper].ToString()));
                            }else{
                                tempScore += 4;
                            }
                        }else if(newTiles.fivePointer.Contains(_word[looper].ToString())) {
                            if(tLetMultiplier.Contains(_word[looper].ToString())){
                                tempScore += 15;// 5 * 3 == 15;
                                tLetMultiplier.RemoveAt(tLetMultiplier.IndexOf(_word[looper].ToString()));
                            }else if(dLetMultiplier.Contains(_word[looper].ToString())){
                                tempScore += 10; // 5 * 2 == 10;
                                dLetMultiplier.RemoveAt(dLetMultiplier.IndexOf(_word[looper].ToString()));
                            }else{
                                tempScore += 5;
                            }
                        }else if(newTiles.eightPointer.Contains(_word[looper].ToString())) {
                            if(tLetMultiplier.Contains(_word[looper].ToString())){
                                tempScore += 24; // 8 * 3 == 24;
                                tLetMultiplier.RemoveAt(tLetMultiplier.IndexOf(_word[looper].ToString()));
                            }else if(dLetMultiplier.Contains(_word[looper].ToString())){
                                tempScore += 16; // 8 * 2 == 16;
                                dLetMultiplier.RemoveAt(dLetMultiplier.IndexOf(_word[looper].ToString()));
                            }else{
                                tempScore += 8;
                            }
                            
                        }else if(newTiles.tenPointer.Contains(_word[looper].ToString())) {
                            if(tLetMultiplier.Contains(_word[looper].ToString())){
                                tempScore += 30; // 10 * 3 == 30;
                                tLetMultiplier.RemoveAt(tLetMultiplier.IndexOf(_word[looper].ToString()));
                            }else if(dLetMultiplier.Contains(_word[looper].ToString())){
                                tempScore += 20; // 10 * 2 == 20;
                                dLetMultiplier.RemoveAt(dLetMultiplier.IndexOf(_word[looper].ToString()));
                            }else{
                                tempScore += 10;
                            }
                        }/*else if(Equals(" ")){//for blank tile
                            tempScore += 0;
                        }*/
                        
                    }
                     if( player == "human"){
                        for(int ctr = 0; ctr < _word.Length; ctr++){
                           Console.WriteLine("befor: "+tempScore);
                            int flag = 1;
                            foreach(int index in tWordMultiplier){
                                if(squares[index].boardMemory == _word[ctr].ToString() && flag == 1){
                                    tempScore *= 3;
                                flag = 0;
                                }
                            }
                            int flag2 = 1;
                        
                            foreach(int index in dWordMultiplier){
                                if(squares[index].boardMemory == _word[ctr].ToString() && flag2 == 1){
                                    tempScore *= 2;
                                    flag2 = 0;
                                }
                            }
                           Console.WriteLine("after" + tempScore);


                        }
                     } else if( player == "computer"){
                    
                        tempScore = computeWordMultiplier(tempScore);
                        
                    }

                score += tempScore;


            }
            Console.WriteLine("SCORE:" + score);
            return (firstTurn == true) ? score * 2: score; 
        }
        public int computeWordMultiplier(int subScore){
            
            foreach(int index in aiMoveIndices){
                if(squares[index].squarePoints == 2){
                        subScore *= 2;
                }else if(squares[index].squarePoints == 4){
                        subScore *= 3;
                }
          
    
            }
                
            return subScore;
        }
                     static int innerCtr = 1;

        public void displayScore(int playerScore, string _player, int _turnCounter){
            int player = ( _player.Equals("human")) ? 1 : 2;
            if(_turnCounter % 2 == 0 && _turnCounter !=0){
                innerCtr ++;
            }
            DataRow row1 = table.Rows[innerCtr-1];
            row1[0] = innerCtr;
            row1[player] = playerScore;
      
            dataGridView.DataSource = table;

            object sumObject;
            sumObject = table.Compute("Sum(You)", "");


            object sumObject2;
            sumObject2 = table.Compute("Sum(Computer)", "");
            if( innerCtr != 1){
                hTotalScore = Convert.ToInt32(sumObject);
                cTotalScore = Convert.ToInt32(sumObject2);
            }
            lblHScore.Text = sumObject.ToString();
            lblCScore.Text = sumObject2.ToString();


        }
        public void displayToBoard(List<int> _moves, List<string> _letters){
            int ctr1 = 0;
            int ctr2 = 0;
            ai = new AI();
            string temp="";
            List<string> wordTemp = new List<string>();
            aiMoveIndices.Clear();
            aiMoveIndices.AddRange(_moves);
            Console.WriteLine("ss" + string.Join(",", _moves));
            Console.WriteLine("sss" + string.Join(",", _letters));

            for(; ctr1 < _letters.Count; ctr1++, ctr2++){

                squares[_moves[ctr2]].boardMemory = _letters[ctr1];
                Console.WriteLine(squares[_moves[ctr2]].boardMemory + ": "+ _moves[ctr2]);

                squares[_moves[ctr2]].pictureBox.Image  = Image.FromFile("Tiles/Scrabble/"+ tileImage(_letters[ctr1])); 
                temp += _letters[ctr1];
                squares[_moves[ctr2]].pictureBox.AllowDrop = false;
                if( squares[_moves[ctr2]].squarePoints == 1){
                    
                    dLetMultiplier.Add(squares[_moves[ctr2]].boardMemory);
                
                }else if(squares[_moves[ctr2]].squarePoints == 3){
                  
                    tLetMultiplier.Add(squares[_moves[ctr2]].boardMemory);
               
                }
            }
            ai.updateTiles(temp);
            wordTemp.Add(temp);
            allWords.Add(temp);
            foreach(string wos in allWords){
                Console.WriteLine("allwords: " + wos);
            }
            
            int turnScore1 = scoreMaker(wordTemp, "computer");
            displayScore(turnScore1, "computer", turnCounter++);
            tilesLeft.Text = "Tiles Left: " + tileBag.Count;
           
            lblStatus.Text = "   Your Turn.";
            lblStatus.ForeColor = Color.FromArgb(0,0,255);

           
      
        }
        public void winnerChecker(  ){
            Console.WriteLine("aitiles" + string.Join(",", AI._playerTiles));
            Console.WriteLine("huimantiles" + string.Join(",", rackTiles));
            int deduction1 = scoreMaker(AI._playerTiles, "player");
            int deduction2 = scoreMaker(rackTiles, "player");

            Console.WriteLine("d1 " +deduction1);
            Console.WriteLine("d2 " + deduction2);

            lblStatus.Text = "Game Over";
            lblStatus.ForeColor = Color.FromArgb(0,255,0);


            cTotalScore = cTotalScore - deduction1;
            hTotalScore = hTotalScore - deduction2;

            lblCScore.Text = ""+ cTotalScore;
            lblHScore.Text = ""+ hTotalScore;

            if(hTotalScore > cTotalScore){

                lblComputer.ForeColor = Color.FromArgb(255,0,0);
                lblCScore.ForeColor = Color.FromArgb(255,0,0);
                lblHScore.ForeColor = Color.FromArgb(0,255,0);
                lblHuman.ForeColor = Color.FromArgb(0,255,0);
                MessageBox.Show("CONGRATULATIONS. YOU WIN" );


            }else if(cTotalScore > hTotalScore){

                lblHuman.ForeColor = Color.FromArgb(255,0,0);
                lblHScore.ForeColor = Color.FromArgb(255,0,0);
                lblComputer.ForeColor = Color.FromArgb(0,255,0);
                lblCScore.ForeColor = Color.FromArgb(0,255,0);
                MessageBox.Show("Computer Win. YOU LOSE");
                
            
            }else{
                MessageBox.Show("Draw");
            }

             DialogResult dialogResult = MessageBox.Show("Play Again?", "ISKRABOL", MessageBoxButtons.YesNo);
            if(dialogResult == DialogResult.Yes)
            {   
                Application.Restart();
            }
            else if (dialogResult == DialogResult.No)
            {
                  System.Windows.Forms.Application.Exit();
            }
            
        }

    }
}
    