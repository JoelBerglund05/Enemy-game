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
        float walkAnimationTimer;

        IActor player;
        public Enemy(IActor player)
        {
            speed = 50f;
            this.player = player;
            walkAnimationTimer = 0;
            this.ScaleSprite = .2f;
            this.ScaleRadius = .05f;

            string[] animations = { "Wraith_01_Moving Forward_000", "Wraith_01_Moving Forward_003", "Wraith_01_Moving Forward_006", 
                "Wraith_01_Moving Forward_009", "Wraith_01_Moving Forward_011" };
            GameArt.Add(animations);
        }

        public override void Update(GameTime gameTime)
        {
            this.TurnTowards(this.player.Position.X, this.player.Position.Y);
            this.Move((float)gameTime.ElapsedGameTime.TotalSeconds * speed);

            if (IsTouching(typeof(Slash)))
            {
                World.RemoveActor(this);
            }

            this.WalkAnimation(gameTime);
        }

        private void WalkAnimation(GameTime gameTime)
        {
            if (walkAnimationTimer <= 0.0f)
            {
                this.ImageName = "Wraith_01_Moving Forward_000";
                this.Image = GameArt.Get(ImageName);
            }
            else if (walkAnimationTimer <= 0.5f)
            {
                this.ImageName = "Wraith_01_Moving Forward_003";
                this.Image = GameArt.Get(ImageName);
            }
            else if (walkAnimationTimer <= 1.0f)
            {
                this.ImageName = "Wraith_01_Moving Forward_006";
                this.Image = GameArt.Get(ImageName);
            }
            else if (walkAnimationTimer <= 1.5f)
            {
                this.ImageName = "Wraith_01_Moving Forward_009";
                this.Image = GameArt.Get(ImageName);
            }
            else if (walkAnimationTimer <= 2.0f)
            {
                this.ImageName = "Wraith_01_Moving Forward_011";
                this.Image = GameArt.Get(ImageName);
                walkAnimationTimer = 0.0f;
            }
                walkAnimationTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
        }
    }
}
