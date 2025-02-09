using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasyMonoGame;
using Microsoft.Xna.Framework;

namespace EasyStart
{
    internal class Satyr : Enemy
    {
        float speed;
        float walkAnimationTimer;
        float attackAnimationTimer;
        float attackCooldown;

        public Satyr(IActor player) : base(player)
        {
            speed = 70f;
            walkAnimationTimer = 0;
            attackAnimationTimer = 0;
            attackCooldown = 1.0f;
            this.ScaleSprite = 0.2f;
            this.ScaleRadius = 0.1f;
            string[] animations = { "Satyr_02_Walking_000", "Satyr_02_Walking_008", "Satyr_02_Walking_017",
                "Satyr_02_Attacking_000", "Satyr_02_Attacking_002", "Satyr_02_Attacking_008", "Satyr_02_Attacking_009",
                "Satyr_02_Attacking_011"};
            GameArt.Add(animations);
        }
        public override void Update(GameTime gameTime)
        {
            this.WalkTowardsPlayer(gameTime);
            this.WalkAnimation(gameTime);
            this.Attack(gameTime);
            IsKilled();
        }

        private void Attack(GameTime gameTime)
        {
            if (attackCooldown <= 0f)
            {
                attackAnimationTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;

                if (attackAnimationTimer <= 0.0f)
                {
                    this.ImageName = "Satyr_02_Attacking_000";
                    Image = GameArt.Get(ImageName);
                }
                else if (attackAnimationTimer <= 0.2f)
                {
                    this.ImageName = "Satyr_02_Attacking_002";
                    Image = GameArt.Get(ImageName);
                }
                else if (attackAnimationTimer <= 0.4f)
                {
                    this.ImageName = "Satyr_02_Attacking_008";
                    Image = GameArt.Get(ImageName);
                    Melee slash = new Melee(this);
                    World.Add(slash, "SlashFX", X, Y);
                    slash.Rotation = this.Rotation;
                }
                else if (attackAnimationTimer <= 0.6f)
                {
                    this.ImageName = "Satyr_02_Attacking_009";
                    Image = GameArt.Get(ImageName);
                }
                else if (attackAnimationTimer <= 0.8f)
                {
                    this.ImageName = "Satyr_02_Attacking_011";
                    Image = GameArt.Get(ImageName);
                }
                else if (attackAnimationTimer <= 1.0f)
                {
                    this.ImageName = "Satyr_02_Walking_000";
                    Image = GameArt.Get(ImageName);
                    attackAnimationTimer = 0;
                    attackCooldown = 1f;
                }
            }
            else
            {
                attackCooldown -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            }

           
        }

        protected override void WalkAnimation(GameTime gameTime)
        {
            walkAnimationTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (walkAnimationTimer <= 0.0f)
            {
                this.ImageName = "Satyr_02_Walking_000";
                this.Image = GameArt.Get(this.ImageName);
            }
            else if (walkAnimationTimer <= 0.5f)
            {
                this.ImageName = "Satyr_02_Walking_008";
                this.Image = GameArt.Get(this.ImageName);
            }
            else if (walkAnimationTimer <= 1.0f)
            {
                this.ImageName = "Satyr_02_Walking_017";
                this.Image = GameArt.Get(this.ImageName);
            }
            else if (walkAnimationTimer <= 1.5f)
            {
                walkAnimationTimer = 0;

            }
        }
    }
}
