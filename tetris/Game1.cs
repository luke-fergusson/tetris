using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.DirectWrite;
using SharpDX.XInput;
using System;
using System.Diagnostics;
using System.Reflection;
using System.Threading;
using System.Timers;
using System.Collections.Generic;
using SharpDX.MediaFoundation;
using System.Reflection.Metadata.Ecma335;

namespace tetris
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private Texture2D _newBlock;
        public enum GameStates//the differnt game states that are available 
        {
            Menu,
            Game,
            End,
            Ai
        }
        private GameStates _state;
        private SpriteFont _font;
        private SpriteFont _ScreenTitle;
        private readonly string Title = "TETRIS";
        
        public char Row;
        public char col;

        public Blocks blocks = new Blocks();
        public O_Block O_Block = new O_Block();
        public I_Block I_Block = new I_Block();
        public J_Block J_Block = new J_Block();
        public L_Block L_Block = new L_Block(); 
        public T_Block T_Block = new T_Block(); 
        public S_Block S_Block = new S_Block();// all block inisalised 
        public Z_Block Z_Block = new Z_Block();

        public GameAi AI = new GameAi();

        public char[,] currentBoards;
        public char[,] previousBoards;
        
        private const float Time = 0;
        private float TimeElapsed;

        public int _height = 0;
        public int _width = 0;

        private float count = 0;

        private float delay = 0;
        private float totalDelay;
        private int Totalnum = 0;
        public bool Delay = true;

        public float Count { get => count; set => count = value; }

        public bool bottom;
        public bool LWall;
        public bool RWall;
        public bool BlockHit;

        public List<Blocks> BlockList = new List<Blocks>();// list of blocks

        public Random random = new Random();
        public Queue<int> upComingBlocks = new Queue<int>(4);// stores the next fall blocks to be called

        public Type BlockType;
        public int HorizontalMove;

        private List<component> _gameComponents;

        private int numberOfMoves = 0;
        private int numOfRot = 0;

        private int score = 0;
        private double speed = 200; // in miliseonds how fast pieces fall

        public Game1()
        {
            

            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            _graphics.HardwareModeSwitch = false; //sets screen to boarderless 


            _width = Window.ClientBounds.Width;// get the current persons screen size
            _height = Window.ClientBounds.Height;
            _graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            _graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;// sets the screen to size of the monitor 


            _graphics.IsFullScreen = true;
            _graphics.ApplyChanges();

            
        }
        private bool SpeedRampUp(GameTime gameTime)// the game progress speeds up the drop speed of the blocks
        {
            double newSpeed;
            count = (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            TimeElapsed = TimeElapsed + count;
            int speedMultiplier = score % 1000;// checks the score for every thousand 
            if(speedMultiplier == 0 && score != 0) 
            {
                newSpeed = speed /(score / 1000);// for every thousand score increase by 10000th of that
            }
            else
            {
                newSpeed = speed;
            }
            if (TimeElapsed > newSpeed)
            {
                
                TimeElapsed = Time;
                return true;
            }
            return false;
        }
        private bool DelayInput(GameTime gameTime)// the game progress speeds up the drop speed of the blocks
        {
            delay = (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            
            totalDelay += delay;


            
            if(totalDelay > 100)
            {
                totalDelay = Time;
                return true ;
            }
            return false;
        }


        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            
            previousBoards = blocks.board.GetBoard();
            //blocks = L_Block;
            RanBlock();// generates a radom block
            blocks.StarPosition();
            IsMouseVisible = true;
            _state = GameStates.Menu;
            currentBoards = new char[10,20];
            //BlockList.Add(blocks);
            BlockType = blocks.GetType();

            AI.SimulateMove(BlockType, blocks);
            HorizontalMove = AI.Best();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _newBlock = Content.Load<Texture2D>("sa");// this texture is used as the indivual blocks on the grid 
           
            _font = Content.Load<SpriteFont>("buttonFont");
            _ScreenTitle = Content.Load<SpriteFont>("Title");


            var PlayButton = new Button(Content.Load<Texture2D>("sa"), Content.Load<SpriteFont>("buttonFont"))// loads each button
            {
                Position = new Vector2(_width / 2 + 375, _height / 2 + 150),// position of each button
                Text = "play",
                
            };
            var AIButton = new Button(Content.Load<Texture2D>("sa"), Content.Load<SpriteFont>("buttonFont"))
            {
                Position = new Vector2(_width / 2 + 375, _height / 2 + 350),
                Text = "AI",

            }; var QuitButton = new Button(Content.Load<Texture2D>("sa"), Content.Load<SpriteFont>("ButtonFont"))
            {
                Position = new Vector2(_width / 2 + 375, _height / 2 + 550),
                Text = "Quit",

            };
            AIButton.Click += AIButton_Click;
            QuitButton.Click += QuitButton_Click;

            PlayButton.Click += PlayButton_Click;
            _gameComponents = new List<component>()// list of component so easily can be drawn
            {
                PlayButton,
                AIButton,
                QuitButton,
            };

            // TODO: use this.Content to load your game content here
        }

        private void QuitButton_Click(object sender, EventArgs e)
        {
            Exit();// exits application
        }

        private void AIButton_Click(object sender, EventArgs e)
        {
            _state = GameStates.Ai;// loads ai
            GenerateNewBlock();
        }

        private void PlayButton_Click(object sender, EventArgs e)
        {
            _state = GameStates.Game; // loads game
        }
        /// <summary>
        /// 
        /// Each simulation of a move returns a horizonatal movement, left or right and rotation state
        /// based on the quantites specified it plays each move by going though a while loop until the condition is met
        /// these values are reset after each move is played 
        /// 
        /// </summary>
        /// <param name="gameTime"></param>
        protected override void Update(GameTime gameTime)
        {
            
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            if(_state == GameStates.Menu)
            {
                foreach(var component in _gameComponents)// checks the buttons for interactions
                {
                    component.Update(gameTime);
                }
            }
            if(_state == GameStates.Ai)
            {
                DrawBoard();

                
                bottom = blocks.GroundCollision();
                RWall = blocks.RWallCollision();
                LWall = blocks.LWallCollision();
                bool Timer = SpeedRampUp(gameTime);
                Delay = DelayInput(gameTime);

                if (!bottom && Timer)
                {
                    
                    blocks.Down();
                    LineCheck(blocks.GroundCollision(), blocks.BlockCollision());
                    DrawBoard();  
                }
                if (AI.HPoint.Direction == "Right")
                {
                    while (numberOfMoves < HorizontalMove)
                    {

                        if (!RWall && Totalnum != 6)
                        {
                            blocks.Right();
                            DrawBoard();

                            Totalnum++;
                        }
                        numberOfMoves++;
                    }
                }
                if(AI.HPoint.Direction == "Left")
                {
                    while(numberOfMoves < HorizontalMove)
                    {
                        if(!LWall && Totalnum != 4)
                        {
                            blocks.Left();
                            DrawBoard();

                            Totalnum++;
                        }
                        numberOfMoves++;
                    }
                }
               
                if(AI.HPoint.RotationState > 0)
                {
                    
                    while(numOfRot < AI.HPoint.RotationState)
                    {
                        blocks.Down();
                        blocks.RotateClockwise();
                        DrawBoard();
                        numOfRot++;
                    }
                }
                
                
                if (bottom)
                {
                    LineCheck(blocks.GroundCollision(), blocks.BlockCollision());
                    ClearTop();
                    GenerateNewBlock();

                    BlockType = blocks.GetType();

                    AI.SimulateMove(BlockType, blocks);
                    HorizontalMove = AI.Best();
                    Totalnum = 0;
                    numberOfMoves = 0;
                    numOfRot = 0;
                    DrawBoard();
                }
                BlockHit = blocks.BlockCollision();
                if (BlockHit && BlockList.Count > 1)
                {
                    LineCheck(blocks.GroundCollision(), blocks.BlockCollision());
                   
                    ClearTop();
                    GenerateNewBlock();

                    BlockType = blocks.GetType();
                    AI.SimulateMove(BlockType, blocks);
                    HorizontalMove = AI.Best();
                    Totalnum = 0;
                    numberOfMoves = 0;
                    numOfRot = 0; 
                    DrawBoard();
                }
                LineCheck(blocks.GroundCollision(), blocks.BlockCollision());
            }
            /// Game takes user input a and d as left and right with W being rotation state
            /// a Delay method stops the user from spamming inputs and breaking the game
            /// timer method is the speed at which the block falls
            /// 
            if (_state == GameStates.Game)
            {

                LineCheck(blocks.GroundCollision(), blocks.BlockCollision());
                bottom = blocks.GroundCollision();
                RWall = blocks.RWallCollision();
                LWall = blocks.LWallCollision();
                bool Timer = SpeedRampUp(gameTime);
                Delay = DelayInput(gameTime);
                
                

                if (Timer && !bottom)
                {
                    blocks.Down();
                    LineCheck(blocks.GroundCollision(), blocks.BlockCollision());
                    DrawBoard();
                }

                if (bottom)
                {
                    LineCheck(blocks.GroundCollision(), blocks.BlockCollision());
                    ClearTop();

                    GenerateNewBlock();

                }

                if (Keyboard.GetState().IsKeyDown(Keys.D))
                {
                    
                    if (Delay && !RWall)
                    {
                        
                        blocks.Right();
                        DrawBoard();
                    }
                }

                if (Keyboard.GetState().IsKeyDown(Keys.A))
                {
                    if (Delay && !LWall)
                    {
                        blocks.Left();
                        DrawBoard();

                    }

                }
                if (Keyboard.GetState().IsKeyDown(Keys.W))
                {
                    if (Delay)
                    {
                        blocks.RotateClockwise();
                        DrawBoard();
                    }
                }
                BlockHit = blocks.BlockCollision();
                if (BlockHit && BlockList.Count > 1)
                {
                    Debug.WriteLine(blocks);
                    LineCheck(blocks.GroundCollision(), blocks.BlockCollision());
                    ClearTop();
                
                    GenerateNewBlock();
                }
            }
            
            

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            if(_state == GameStates.Menu)
            {
                
                DrawMenu(gameTime);
                this.IsMouseVisible = true;
            }
            if(_state == GameStates.Game)
            {
                DrawBoard();
                this.IsMouseVisible = false;
            }
            if(_state == GameStates.Ai) 
            {

                DrawBoard();
                this.IsMouseVisible = false;    
            }
            if(_state == GameStates.End) 
            {
                DrawEndScreen(gameTime); this.IsMouseVisible = true;
            }
            
            


            base.Draw(gameTime);
        }
        /// <summary>
        /// Draws goes through every position on the board comparing its value with 0 and the leters defining the blocks
        /// and colour coding them based on there letter
        /// </summary>
        private void DrawBoard()
        {
            previousBoards = blocks.board.GetBoard();
            
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 20; j++)
                {
                    if (previousBoards[i,j] == '0' || previousBoards[i, j] == 0)
                    {
                        _spriteBatch.Begin();
                        _spriteBatch.Draw(_newBlock, new Rectangle((50 * i) + _width - 100, 50 * j + 10, 50, 50), Color.White);
                        _spriteBatch.End();
                    }
                    if(previousBoards[i, j] == 'o')
                    {
                        _spriteBatch.Begin();
                        _spriteBatch.Draw(_newBlock, new Rectangle((50 * i) + _width - 100, 50 * j + 10, 50, 50), Color.Yellow);
                        _spriteBatch.End();
                    }
                    if (previousBoards[i, j] == 'i')
                    {
                        _spriteBatch.Begin();
                        _spriteBatch.Draw(_newBlock, new Rectangle((50 * i) + _width - 100, 50 * j + 10, 50, 50), Color.LightBlue);
                        _spriteBatch.End();
                    }
                    if (previousBoards[i, j] == 'j')
                    {
                        _spriteBatch.Begin();
                        _spriteBatch.Draw(_newBlock, new Rectangle((50 * i) + _width - 100, 50 * j + 10, 50, 50), Color.Blue);
                        _spriteBatch.End();
                    }
                    if (previousBoards[i,j] == 'l')
                    {
                        _spriteBatch.Begin();
                        _spriteBatch.Draw(_newBlock, new Rectangle((50 * i) + _width - 100, 50 * j + 10, 50, 50), Color.Orange);
                        _spriteBatch.End();
                    }
                    if(previousBoards[i,j] == 't')
                    {
                        _spriteBatch.Begin();
                        _spriteBatch.Draw(_newBlock, new Rectangle((50 * i) + _width - 100, 50 * j + 10, 50, 50), Color.Purple);
                        _spriteBatch.End();
                    }
                    if (previousBoards[i, j] == 's')
                    {
                        _spriteBatch.Begin();
                        _spriteBatch.Draw(_newBlock, new Rectangle((50 * i) + _width - 100, 50 * j + 10, 50, 50), Color.LimeGreen);
                        _spriteBatch.End();
                    }
                    if (previousBoards[i, j] == 'z')
                    {
                        _spriteBatch.Begin();
                        _spriteBatch.Draw(_newBlock, new Rectangle((50 * i) + _width - 100, 50 * j + 10, 50, 50), Color.Red);
                        _spriteBatch.End();
                    }

                }
            }
            _spriteBatch.Begin();
            _spriteBatch.DrawString(_font, "Score: " + score, new Vector2(150, 10), Color.White);
            _spriteBatch.End();

        }
        public void DrawMenu(GameTime gameTime)
        {

            _spriteBatch.Begin();
            _spriteBatch.DrawString(_ScreenTitle, Title, new Vector2(_width / 2 + 375, _height -375), Color.Black);
            foreach (var component in _gameComponents)
            {
                
                component.Draw(gameTime, _spriteBatch);
            }
            _spriteBatch.End();
        }
        public void GenerateNewBlock()
        {
            
            currentBoards = previousBoards;

            //blocks = new L_Block();
            //BlockList.Add(blocks);
            RanBlock();
            blocks.board.currentGameBoards = currentBoards;
            
            
            blocks.StarPosition();
            DrawBoard();
        }
        public void RanBlock()
        {
            int num = random.Next(0, 7);
            switch (num)
            {
                case 0:
                    blocks = new O_Block();
                    break;
                case 1:
                    blocks = new I_Block();
                    break;
                case 2:
                    blocks = new L_Block();
                    break;
                case 3:
                    blocks = new J_Block();
                    break;
                case 4:
                    blocks = new S_Block();
                    break;
                case 5:
                    blocks = new Z_Block();
                    break;
                case 6:
                    blocks = new T_Block();
                    break;
                default:break;
            }
            BlockList.Add(blocks);
        }
        /// <summary>
        /// Goes through the entire board checking if a line has been filled thats defined as 10 non zeros in a single line
        /// and calls the linemovedown method
        /// the number of lines cleared is counted and passed to score to calculate the score
        /// </summary>
        /// <param name="Bottom"></param>
        /// <param name="BlockHit"></param>
        public void LineCheck(bool Bottom, bool BlockHit)
        {
            int RowCount = 0;
            int LineCount = 0;
            currentBoards = blocks.board.GetBoard();
            if (!Bottom && !BlockHit)
            {


                for (int j = 0; j < 20; j++)
                {
                    for (int i = 0; i < 10; i++)
                    {
                        if (currentBoards[i, j] != '0')
                        {
                            LineCount++;
                        }
                    }
                    if (LineCount == 10)
                    {
                        RowCount++;
                        blocks.LineMoveDown(j);
                    }
                    LineCount = 0;
                }
            }
            Score(RowCount);
        }

        public void Score(int RowCount)
        {
            switch (RowCount)
            {
                case 0:
                    break;
                case 1:
                    score += 100;
                    break;
                case 2:
                    score += 200;
                    break;
                case 3:
                    score += 500;
                    break;
                case 4:
                    score += 800;
                    break;
                default:
                    break;
            }
            
        }
        public void ClearTop()// used to stop duplicate blocks when moving everything down
        {
            /*for (int j = 0; j < 10; j++)
            {
                for(int i = 0;i < 5; i++)
                {
                    blocks.board.ChangeBoard(j, i, '0');
                }
                    
            }*/
        }
       
        public void DrawEndScreen(GameTime gameTime)
        {
            _spriteBatch.Begin();
            _spriteBatch.DrawString(_ScreenTitle, "GameOver", new Vector2(_width / 2 + 375, _height - 375), Color.Black);
            foreach (var component in _gameComponents)
            {

                component.Draw(gameTime, _spriteBatch);
            }
            _spriteBatch.End();
        }
    }
    
 
}