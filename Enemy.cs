using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using EasyMonoGame;

namespace EasyStart
{
    internal class Enemy : Actor
    {
        float speed;
        IActor player;
        private Random random = new Random();
        public Enemy(IActor player)
        {
            speed = 50f;
            this.player = player;
        }

        public override void Update(GameTime gameTime)
        {
            this.TurnTowards(this.player.Position.X, this.player.Position.Y);
            this.Move((float)gameTime.ElapsedGameTime.TotalSeconds * speed);

            if (IsTouching(typeof(Slash)))
            {
                World.RemoveActor(this);
            }
        }
    }
}
