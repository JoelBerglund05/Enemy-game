using EasyMonoGame;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyStart
{
    internal class Ghost : Enemy
    {
        float speed;
        float walkAnimationTimer;
        float attackAnimationTimer;

        float attackColdown;

        public Ghost(IActor player) : base(player)
        {
            this.speed = 100f;
            
            this.walkAnimationTimer = 0;
            this.attackAnimationTimer = 0;

            this.ScaleSprite = .2f;
            this.ScaleRadius = .05f;
            
            this.attackColdown = 2.0f;

            string[] animations = { "Wraith_01_Moving Forward_000", "Wraith_01_Moving Forward_003", "Wraith_01_Moving Forward_006",
                "Wraith_01_Moving Forward_009", "Wraith_01_Moving Forward_011", "Wraith_01_Casting Spells_000", "Wraith_01_Casting Spells_003",
                "Wraith_01_Casting Spells_007", "Wraith_01_Casting Spells_014", "Wraith_01_Casting Spells_017"};
            GameArt.Add(animations);
        }

        public override void Update(GameTime gameTime)
        {
            WalkTowardsPlayer(gameTime);
            WalkAnimation(gameTime);
            Attack(gameTime);
            IsKilled();
        }

        private void Attack(GameTime gameTime)
        {
            if (attackColdown <= 0.0f)
            {
                attackAnimationTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (attackAnimationTimer <= 0.0f)
                {
                    this.ImageName = "Wraith_01_Casting Spells_000";
                    this.Image = GameArt.Get(ImageName);
                }
                else if (attackAnimationTimer <= 0.3f)
                {
                    this.ImageName = "Wraith_01_Casting Spells_003";
                    this.Image = GameArt.Get(ImageName);
                }
                else if (attackAnimationTimer <= 0.6f)
                {
                    this.ImageName = "Wraith_01_Casting Spells_007";
                    this.Image = GameArt.Get(ImageName);
                }
                else if (attackAnimationTimer <= 0.9f)
                {
                    this.ImageName = "Wraith_01_Casting Spells_014";
                    this.Image = GameArt.Get(ImageName);
                }
                else if (attackAnimationTimer <= 1.2f)
                {
                    this.ImageName = "Wraith_01_Casting Spells_017";
                    this.Image = GameArt.Get(ImageName);
                    attackAnimationTimer = 0.0f;
                    Projectile projectile = new Projectile();
                    projectile.Rotation = this.Rotation;
                    World.Add(projectile, "Spells Effect", this.Position.X, this.Position.Y);
                    projectile.Move(30f);
                    attackColdown = 2.0f;
                }
            }
            else
            {
                attackColdown -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
        }

        protected override void WalkAnimation(GameTime gameTime)
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
