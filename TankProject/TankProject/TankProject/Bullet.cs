using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TankProject
{
    class Bullet
    {
        public Texture2D texture;
        public Vector2 position;
        public Vector2 velocity;
        public Vector2 origin;

        public bool isVisible;

        public Bullet(Texture2D newTexture)
        {
            texture = newTexture;
            isVisible = false;
        }

        public void Draw(SpriteBatch sb)
        {
            sb.Draw(texture, position, null, Color.White, 0f, origin, 1f, SpriteEffects.None, 0);
        }
  
    }
}
