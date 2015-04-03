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
        private Texture2D bulletTexture;

        private Rectangle tankRect;

        //The centre of image
        private Vector2 tankOrigin;

        public Vector2 tankPosition;
        public float rotation;

        private const float ROTATION_VELOCITY = 0.07f;


        public Vector2 tankVelocity;
        private const float TANGENT_VELOCITY = 6f;
        private const float TANGENT_BACK_VELOCITY = 2f;
        private const float FRICTION = 0.3f;

        private List<Bullet> bullets = new List<Bullet>();

        private KeyboardState pastKey;

        public Tank(Texture2D tankTexture, Texture2D bulletTexture, int x, int y)
        {
            this.tankTexture = tankTexture;
            this.bulletTexture = bulletTexture;
            tankPosition = new Vector2(x, y);
        }

        public void Draw(SpriteBatch sb)
        {
            sb.Draw(tankTexture, tankPosition, null, Color.White, rotation, tankOrigin, 1f, SpriteEffects.None, 0);
        }

        public void DrawBullets(SpriteBatch sb)
        {
            foreach (Bullet bullet in bullets)
                bullet.Draw(sb);
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


            /*
            //Граница екрана
            if (tankPosition.X < 0 + tankTexture.Height / 2)
                tankPosition.X = 0 + tankTexture.Height / 2;
            if (tankPosition.X > Game1.SCREEN_WIDTH - tankTexture.Height / 2)
                tankPosition.X = Game1.SCREEN_WIDTH - tankTexture.Height / 2;
            if (tankPosition.Y < 0 + tankTexture.Height / 2)
                tankPosition.Y = 0 + tankTexture.Height / 2;
            if (tankPosition.Y > Game1.SCREEN_HEIGHT - tankTexture.Height / 2)
                tankPosition.Y = Game1.SCREEN_HEIGHT - tankTexture.Height / 2;
             * */

        }

        private void Shoot()
        {
            Bullet newBullet = new Bullet(bulletTexture);
            newBullet.velocity = new Vector2((float)Math.Sin(rotation), -(float)Math.Cos(rotation)) * 3/** 3f + tankVelocity*/;
            newBullet.position.X = tankPosition.X;
            newBullet.position.Y = tankPosition.Y;
            newBullet.isVisible = true;

           // if (bullets.Count() < 20)
                bullets.Add(newBullet);
        }

        private void UpdateBullets()
        {
            foreach (Bullet bullet in bullets)
            {
                bullet.position += bullet.velocity;
                /*if (Vector2.Distance(bullet.position, tankPosition) > 500)
                    bullet.isVisible = false;*/
            }

            /*for (int i = 0; i < bullets.Count(); i++)
            {
                if (!bullets[i].isVisible)
                    bullets.RemoveAt(i);
                i--;
            }*/
        }

        public void Shooting()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Space) && pastKey.IsKeyUp(Keys.Space))
                Shoot();

            pastKey = Keyboard.GetState();

            UpdateBullets();
        }
    }

}
