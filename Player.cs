using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using EasyMonoGame;
using Microsoft.Xna.Framework.Input;

namespace EasyStart
{
    internal class Player : Actor
    {
        float speed;
        public Player() {
            speed = 3f;
            this.ScaleRadius = 4f;
        }

        public override void Act()
        {
            Movement();

            this.TurnTowards(Mouse.GetState().X, Mouse.GetState().Y);
        }

        private void Movement()
        {
            Vector2 position = new Vector2();
            position.X = this.X;
            position.Y = this.Y;
            position = Vector2.Normalize(position);
            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                this.Y -= position.Y * speed;

            }
            if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                this.Y += position.Y * speed;

            }
            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                this.X += position.X * speed;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                this.X -= position.X * speed;
            }
           
        }
    }
}
