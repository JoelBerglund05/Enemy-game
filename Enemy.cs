using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using EasyMonoGame;

namespace EasyStart
{
    abstract class Enemy : Actor
    {
        float speed;
        float walkAnimationTimer;

        IActor player;
        public Enemy(IActor player)
        {
            speed = 50f;
            this.player = player;
            walkAnimationTimer = 0;
            
        }

        protected void WalkTowardsPlayer(GameTime gameTime)
        {
            this.TurnTowards(this.player.Position.X, this.player.Position.Y);
            this.Move((float)gameTime.ElapsedGameTime.TotalSeconds * speed);
        }

        protected void IsKilled()
        {
            if (IsTouching(typeof(PlayerMelee)))
            {
                World.RemoveActor(this);
            }
        }

        protected abstract void WalkAnimation(GameTime gameTime);
    }
}
