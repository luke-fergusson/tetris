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

namespace tetris
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private Texture2D _grid;
        private Texture2D _testBlock;
        private Texture2D _newBlock;
        private Texture2D _block;

        public char Row;
        public char col;

        public Board board = new Board();
        public Blocks blocks = new Blocks();
        public O_Block O_Block = new O_Block();
        public I_Block I_Block = new I_Block();


        private Vector2 _velocity;

        //public char[,] currentGameBoards;
        public char[,] previousGameBoards;
        
        private const float Time = 0;
        private float TimeElapsed;
        private bool Displayed = false;

        private bool OutOfBounds;

        public int _height = 0;
        public int _width = 0;

        public int _spawnLocation = 0;

        private float count = 0;

        private float delay = 0;
        private float totalDelay;

        public bool Delay = true;

        public float Count { get => count; set => count = value; }
        public int SpawnLocation { get => _spawnLocation; set => _spawnLocation = value; }

        public bool bottom;
        public bool LWall;
        public bool RWall;

        public Game1()
        {
            

            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            _graphics.HardwareModeSwitch = false; //sets screen to boarderless 


            _width = Window.ClientBounds.Width;
            _height = Window.ClientBounds.Height;
            _graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            _graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;// sets the screen to size of the monitor 


            _graphics.IsFullScreen = true;
            _graphics.ApplyChanges();

            
        }
        private bool SpeedRampUp(GameTime gameTime)// the game progress speeds up the drop speed of the blocks
        {
            count = (float)gameTime.ElapsedGameTime.TotalSeconds;
            TimeElapsed = TimeElapsed + count;


            if (TimeElapsed > 0.5)
            {
                
                TimeElapsed = Time;
                return true;
            }
            return false;
        }
        private bool DelayInput(GameTime gameTime)// the game progress speeds up the drop speed of the blocks
        {
            delay = (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            
            totalDelay= totalDelay + delay;


            Debug.WriteLine(totalDelay);
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
            board.blankBoard();
            //I_Block.StarPosition();

            O_Block.StarPosition();
            
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _newBlock = Content.Load<Texture2D>("sa");
            _velocity = new Vector2(_width+100, 10);
            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            bottom = O_Block.GroundCollision();
            RWall = O_Block.RWallCollision();
            LWall = O_Block.LWallCollision();
            bool Timer = SpeedRampUp(gameTime);
            Delay = DelayInput(gameTime);
            if (Timer && bottom)
            {
                O_Block.Down();
                DrawBoard();
            }

            if(Keyboard.GetState().IsKeyDown(Keys.D) )
            {

                if (Delay&& RWall)
                {

                    O_Block.Right();
                    DrawBoard();
                }
            }
            
            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                if (Delay && LWall)
                {
                    O_Block.Left();
                    DrawBoard();
                    
                }
                
            }
            
            

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            
            DrawBoard();
            


            base.Draw(gameTime);
        }

        private void DrawBoard()
        {
            previousGameBoards = O_Block.board.GetBoard();
            
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 20; j++)
                {
                    if (previousGameBoards[i,j] == '0' || previousGameBoards[i, j] == 0)
                    {
                        _spriteBatch.Begin();
                        _spriteBatch.Draw(_newBlock, new Rectangle((50 * i) + _width - 100, 50 * j + 10, 50, 50), Color.White);
                        _spriteBatch.End();
                    }
                    if(previousGameBoards[i, j] == 'o')
                    {
                        _spriteBatch.Begin();
                        _spriteBatch.Draw(_newBlock, new Rectangle((50 * i) + _width - 100, 50 * j + 10, 50, 50), Color.Yellow);
                        _spriteBatch.End();
                    }
                    if (previousGameBoards[i, j] == 'i')
                    {
                        _spriteBatch.Begin();
                        _spriteBatch.Draw(_newBlock, new Rectangle((50 * i) + _width - 100, 50 * j + 10, 50, 50), Color.Blue);
                        _spriteBatch.End();
                    }
                    
                }
            }
                

        }

        
    }

 
}