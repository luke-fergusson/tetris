using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Diagnostics;
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



        private Vector2 _velocity;

        
        
        private const float Time = 0;
        private float TimeElapsed;


        public int _height = 0;
        public int _width = 0;

        public int _spawnLocation = 0;
        private float count = 0;

        private KeyboardState D1, D2;

        public float Count { get => count; set => count = value; }

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
        private bool SpeedRampUp(GameTime gameTime)
        {
            count = (float)gameTime.ElapsedGameTime.TotalSeconds;
            TimeElapsed = TimeElapsed + count;
            Debug.WriteLine(TimeElapsed);

            if (TimeElapsed > 2)
            {
                
                TimeElapsed = Time;
                return true;
            }
            return false;
        }
        private bool CheckForMovement()
        {
            if(Keyboard.GetState().IsKeyDown(Keys.D))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        
    
        

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            
            

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _grid = Content.Load<Texture2D>("Tetris_Background (1)");
            _testBlock = Content.Load<Texture2D>("testBlock");
            _velocity = new Vector2(_width+100, 10);
            
            
            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();


            bool Timer = SpeedRampUp(gameTime);
            if (Timer)
            {
                _velocity.Y += 50f;
            }

            bool keyDown = CheckForMovement();
            if (keyDown && Keyboard.GetState().IsKeyDown(Keys.D))
            {
                _velocity.X += 50f;
            }
           
            
            

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _spawnLocation = _width+100;
            
            _spriteBatch.Begin();
            _spriteBatch.Draw(_grid, new Rectangle((_width-100),10, 500,1000), Color.White);
            _spriteBatch.Draw(_testBlock, _velocity, new Rectangle(_spawnLocation,10,102,100), Color.White);
            _spriteBatch.End();
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }

        
    }
}