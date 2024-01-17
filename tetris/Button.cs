using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.Direct3D9;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tetris
{
    public class Button : component
    {
        private MouseState mouseState;
        private SpriteFont _font;
        private SpriteBatch spriteBatch;
        private bool _ishov;
        private MouseState previousState;
        private Texture2D _texture;

        public event EventHandler Click;
        public Vector2 Position;
        public string Text { get; set; }

        public Button(Texture2D texture, SpriteFont font)
        {
            _texture = texture;
            _font = font;

        }
        public Rectangle Rectangle
        {
            get
            {
                return new Rectangle((int)Position.X, (int)Position.Y, 100, 100);
            }
        }
        
        public override void Draw(GameTime gameTime , SpriteBatch spriteBatch)
        {
            var colour = Color.White;
            if (_ishov)
            {
                colour = Color.Gray;
                

            }
            spriteBatch.Draw(_texture, Rectangle, colour);
            if (!string.IsNullOrEmpty(Text))
            {
                var x = (Rectangle.X  + (Rectangle.Width / 2))-(_font.MeasureString(Text).X /2);
                var y = (Rectangle.Y + (Rectangle.Height / 2)) - (_font.MeasureString(Text).Y / 2);

                spriteBatch.DrawString(_font, Text, new Vector2(x,y), Color.Black);
            }
        }

        public override void Update(GameTime gameTime)
        {
            previousState = mouseState;
            mouseState = Mouse.GetState();

            var mouseRectangle = new Rectangle(mouseState.X, mouseState.Y,1,1);

            _ishov = false;
            if (mouseRectangle.Intersects(Rectangle))
            {
                _ishov = true;
                if (mouseState.LeftButton == ButtonState.Pressed && previousState.LeftButton == ButtonState.Released)
                {

                    Click?.Invoke(this, new EventArgs());//if click is not null
                }

            }
        }
    }
}
