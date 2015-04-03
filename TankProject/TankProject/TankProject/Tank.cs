using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TankProject
{
    class Tank
    {
        private Texture2D tankTexture;
        private Rectangle tankRect;

        //The centre of image
        private Vector2 tankOrigin;

        private Vector2 tankPosition;
        private float rotation;

        private const float ROTATION_VELOCITY = 0.07f;


        private Vector2 tankVelocity;
        private const float TANGENT_VELOCITY = 6f;
        private const float TANGENT_BACK_VELOCITY = 2f;
        private const float FRICTION = 0.3f;

        public Tank(Texture2D tankTexture, int x, int y)
        {
            this.tankTexture = tankTexture;
            tankPosition = new Vector2(x, y);
        }

        public void Draw(SpriteBatch sb)
        {
            sb.Draw(tankTexture, tankPosition, null, Color.White, rotation, tankOrigin, 1f, SpriteEffects.None, 0);
        }

        public void Move()
        {
            tankRect = new Rectangle((int)tankPosition.X, (int)tankPosition.Y, tankTexture.Width, tankTexture.Height);
            tankPosition += tankVelocity;
            tankOrigin = new Vector2(tankRect.Width / 2, tankRect.Height / 2 + 7);

            if (Keyboard.GetState().IsKeyDown(Keys.Right))
                rotation += ROTATION_VELOCITY;

            if (Keyboard.GetState().IsKeyDown(Keys.Left))
                rotation -= ROTATION_VELOCITY;

            if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                tankVelocity.X = (float)Math.Sin(rotation) * TANGENT_VELOCITY;
                tankVelocity.Y = -(float)Math.Cos(rotation) * TANGENT_VELOCITY;
            }
            else if (tankVelocity != Vector2.Zero)
            {
                Vector2 i = tankVelocity;
                tankVelocity = i -= FRICTION * i;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                tankVelocity.X = -(float)Math.Sin(rotation) * TANGENT_BACK_VELOCITY;
                tankVelocity.Y = (float)Math.Cos(rotation) * TANGENT_BACK_VELOCITY;
            }
            else if (tankVelocity != Vector2.Zero)
            {
                Vector2 i = tankVelocity;
                tankVelocity = i -= FRICTION * i;
            }

        }
    }
}
