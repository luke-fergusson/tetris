using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace tetris
{
    public abstract class component// used to when generating buttons
    {
        public abstract void Draw(GameTime gameTime, SpriteBatch spriteBatch);
        public abstract void Update(GameTime gameTime);
    }
}
