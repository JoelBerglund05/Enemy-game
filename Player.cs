using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using EasyMonoGame;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace EasyStart
{
    internal class Player : Actor
    {
        float speed;
        public Player() {
            speed = 100f;
            this.ScaleSprite = .1f;
            this.ScaleRadius = .1f;
        }

        public override void Update(GameTime gameTime)
        {
            Movement(gameTime);

            this.TurnTowards(Mouse.GetState().X, Mouse.GetState().Y);
        }

        private void Movement(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                this.Y -= (float)Math.Sqrt(speed * gameTime.ElapsedGameTime.TotalSeconds);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                this.Y += (float)Math.Sqrt(speed * gameTime.ElapsedGameTime.TotalSeconds);

            }
            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                this.X += (float)Math.Sqrt(speed * gameTime.ElapsedGameTime.TotalSeconds);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                this.X -= (float)Math.Sqrt(speed * gameTime.ElapsedGameTime.TotalSeconds);
            }
        }


    }
}
