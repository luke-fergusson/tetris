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

namespace tetris
{
    public abstract class component
    {
        public abstract void Draw(GameTime gameTime, SpriteBatch spriteBatch);
        public abstract void Update(GameTime gameTime);
    }
}
