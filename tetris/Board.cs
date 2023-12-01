using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Diagnostics;
using System.Threading;
using System.Timers;

namespace tetris
{
    public class Board : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private Texture2D _grid;
        private Texture2D _testBlock;
        private Texture2D _newBlock;
        private Texture2D _block;

        

        private Vector2 _velocity;

        public char[,] currentGameBoards;
        public char[,] previousGameBoards;
        
        private const float Time = 0;
        private float TimeElapsed;
        private bool Displayed = false;

        public int _height = 0;
        public int _width = 0;

        public int _spawnLocation = 0;

        private float count = 0;

        private KeyboardState D1, D2; // D1 old keyboard state D2 is current keyboard state so one input is accepted at a time
        private KeyboardState A1, A2;

        public float Count { get => count; set => count = value; }
        public int SpawnLocation { get => _spawnLocation; set => _spawnLocation = value; }

        public Board()
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
        /*private bool SpeedRampUp(GameTime gameTime)// the game progress speeds up the drop speed of the blocks
        {
            count = (float)gameTime.ElapsedGameTime.TotalSeconds;
            TimeElapsed = TimeElapsed + count;
            
            

            if (TimeElapsed > 2)
            {
                
                TimeElapsed = Time;
                return true;
            }
            return false;
        }*/
        
        
    
        public void initalGameBoard()
        {
            currentGameBoards = new char[10, 20];
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            currentGameBoards = new char[10, 20];
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


            /*bool Timer = SpeedRampUp(gameTime);
            if (Timer)
            {
                _velocity.Y += 50f;
            }*/


            D2 = Keyboard.GetState();

            if(D1.IsKeyUp(Keys.D) && D2.IsKeyDown(Keys.D))
            {
                _velocity.X += 50f;
                //DrawBoard();
            }
            D1 = D2;

            A2 = Keyboard.GetState();

            if (A1.IsKeyUp(Keys.A) && A2.IsKeyDown(Keys.A))
            {
                _velocity.X -= 50f;
            }
            A1 = A2;


            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            if (!Displayed)
            {
                DrawBoard();
            }
            Blocks currentBlock = new Blocks();
            Debug.WriteLine(currentBlock.work);
            currentBlock.setBlock();

            



            base.Draw(gameTime);
        }

        private void DrawBoard()
        {
            
            
            Blocks currentBlock = new Blocks();
            Debug.WriteLine(currentBlock.work);
            currentBlock.setBlock();

            while (!Displayed)
            {
                for (int i = 0; i < 10; i++)
                {
                    for (int j = 0; j < 20; j++)
                    {

                        if (currentGameBoards[i, j] == 0)
                        {
                            _spriteBatch.Begin();
                            _spriteBatch.Draw(_newBlock, new Rectangle((50 * i) + _width - 100, 50 * j + 10, 50, 50), Color.White * 0.25f);
                            // draws board with size of 50 px per square at 50px intverals at the centre of the screen and 10px down
                            _spriteBatch.End();
                        }
                        if (currentGameBoards[i, j] == 'o')
                        {
                            _spriteBatch.Begin();
                            _spriteBatch.Draw(_newBlock, new Rectangle((50 * i) + _width - 100, 50 * j + 10, 50, 50), Color.Yellow);
                            Debug.WriteLine("yellow");
                            _spriteBatch.End();
                        }
                    }
                }
                
                Displayed = true;
            }


        }

        
    }

 
}