using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
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



        private Vector2 _gravity;

        private float _speed;
        
        


        public int _height = 0;
        public int _width = 0;

        public int _spawnLocation = 0;

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
        private float CalcGravity()
        {
            bool Done = Timer();
            if (Done)
            {
                _speed = 4f;// sets the value of how quick it the block falls
            }
            

            

            return _speed;
        }
        private bool Timer()
        {
            
            System.Timers.Timer timer = new System.Timers.Timer();
            timer.Interval = 30;
            timer.Enabled = true;
                
            while(timer.Enabled == true)
            {
                return false;
            }

            return true;
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
            _gravity = new Vector2(_width+100, 0);
            
            
            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            Timer();       
            _gravity.Y += CalcGravity();
            

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _spawnLocation = _width+100;
            
            _spriteBatch.Begin();
            _spriteBatch.Draw(_grid, new Rectangle((_width-100),10, 500,1000), Color.White);
            _spriteBatch.Draw(_testBlock, _gravity, new Rectangle(_spawnLocation,10,102,100), Color.White);
            _spriteBatch.End();
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }

        
    }
}