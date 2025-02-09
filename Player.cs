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
    internal enum PlayerState
    {
        Idle,
        Walking,
        Attacking,
        Dead
    }
    internal class Player : Actor
    {
        float speed;
        float walkingAnimationTimer;
        float idleAnimationTimer;
        float attackAnimationTimer;
        float attackCooldown;

        private bool isAttacking;

        private PlayerState state;


        public Player() {
            speed = 100f;
            this.ScaleSprite = .1f;
            this.ScaleRadius = .05f;
            walkingAnimationTimer = 0f;
            idleAnimationTimer = 0f;
            attackAnimationTimer = 0f;
            attackCooldown = 1f;
            isAttacking = false;
            string[] imageNames = { "0_Golem_Walking_000", "0_Golem_Walking_023", "0_Golem_Walking_009", 
                "0_Golem_Idle_000", "0_Golem_Idle_008", "0_Golem_Idle_017", "0_Golem_Idle Blinking_000",
                "0_Golem_Idle Blinking_003", "0_Golem_Idle Blinking_006", "0_Golem_Idle Blinking_009",
                "0_Golem_Idle Blinking_012", "0_Golem_Idle Blinking_015", "0_Golem_Idle Blinking_017",
                "0_Golem_Run Slashing_000", "0_Golem_Run Slashing_003", "0_Golem_Run Slashing_006",
                "0_Golem_Run Slashing_007", "0_Golem_Run Slashing_008"};
            GameArt.Add(imageNames);
            this.state = PlayerState.Idle;

        }

        public override void Update(GameTime gameTime)
        {
            this.Movement(gameTime);
            this.Atack(gameTime);

            this.TurnTowards(Mouse.GetState().X, Mouse.GetState().Y);

            if (IsTouching(typeof(Enemy)))
            {
                World.RemoveActor(this);
            }

            this.AnimationManager(gameTime);


        }



        private void Movement(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                this.Y -= (float)Math.Sqrt(speed * gameTime.ElapsedGameTime.TotalSeconds);
                this.state = PlayerState.Walking;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                this.Y += (float)Math.Sqrt(speed * gameTime.ElapsedGameTime.TotalSeconds);
                this.state = PlayerState.Walking;

            }
            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                this.X += (float)Math.Sqrt(speed * gameTime.ElapsedGameTime.TotalSeconds);
                this.state = PlayerState.Walking;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                this.X -= (float)Math.Sqrt(speed * gameTime.ElapsedGameTime.TotalSeconds);
                this.state = PlayerState.Walking;
            }

            if (Keyboard.GetState().IsKeyUp(Keys.W) && Keyboard.GetState().IsKeyUp(Keys.S) && Keyboard.GetState().IsKeyUp(Keys.A) && Keyboard.GetState().IsKeyUp(Keys.D))
            {
                this.state = PlayerState.Idle;
            }
        }

        private void Atack(GameTime gameTime)
        {
            if (Mouse.GetState().LeftButton == ButtonState.Pressed && attackCooldown <= 0)
            {
                this.state = PlayerState.Attacking;
                isAttacking = true;
            }
            attackCooldown -= (float)gameTime.ElapsedGameTime.TotalSeconds;
        }

        private void AnimationManager(GameTime gameTime)
        {
            if (this.state == PlayerState.Attacking)
            {
                this.AttackAnimation(gameTime);
                attackAnimationTimer = 0;
                attackAnimationTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            else if (this.state == PlayerState.Walking && !isAttacking)
            {
                this.Walk(gameTime);
            }
            else if (this.state == PlayerState.Idle && !isAttacking)
            {
                this.Idle(gameTime);
            } 
            else
            {
                this.AttackAnimation(gameTime);
                attackAnimationTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            }


        }
        private void AttackAnimation(GameTime gameTime)
        {
            Slash slash = new Slash(this);

            if (attackAnimationTimer <= 0.0f)
            {
                this.ImageName = "0_Golem_Run Slashing_000";
                Image = GameArt.Get(ImageName);
            }
            else if (attackAnimationTimer <= 0.2f)
            {
                this.ImageName = "0_Golem_Run Slashing_003";
                Image = GameArt.Get(ImageName);
            }
            else if (attackAnimationTimer <= 0.4f)
            {
                this.ImageName = "0_Golem_Run Slashing_006";
                Image = GameArt.Get(ImageName);
                World.Add(slash, "SlashFX", X, Y);
                slash.Rotation = this.Rotation;
                slash.Move(15);
            }
            else if (attackAnimationTimer <= 0.6f)
            {
                this.ImageName = "0_Golem_Run Slashing_007";
                Image = GameArt.Get(ImageName);
            }
            else if (attackAnimationTimer <= 0.8f)
            {
                this.ImageName = "0_Golem_Run Slashing_008";
                Image = GameArt.Get(ImageName);
            }
            else if (attackAnimationTimer <= 1.0f)
            {
                this.ImageName = "0_Golem_Idle_000";
                Image = GameArt.Get(ImageName);
                attackAnimationTimer = 0;
                attackCooldown = 1f;
                isAttacking = false;
            }
        }

        private void Walk(GameTime gameTime)
        {
            if (this.state == PlayerState.Idle)
            {
                this.Idle(gameTime);
                walkingAnimationTimer = 0;

                return;
            }
            walkingAnimationTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (walkingAnimationTimer <= 0.0f)
            {
                this.ImageName = "0_Golem_Walking_000";
                Image = GameArt.Get(ImageName);
                Console.WriteLine(Image);

            }
            else if (walkingAnimationTimer <= 0.5f)
            {
                this.ImageName = "0_Golem_Walking_009";
                Image = GameArt.Get(ImageName);
                Console.WriteLine(Image);

            }
            else if (walkingAnimationTimer <= 1.0f)
            {
                this.ImageName = "0_Golem_Walking_023";
                Image = GameArt.Get(ImageName);
                Console.WriteLine(Image);
            } else if (walkingAnimationTimer <= 1.5f)
            {
                walkingAnimationTimer = 0;
            }
        }

        private void Idle(GameTime gameTime)
        {
            idleAnimationTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (idleAnimationTimer <= 0.0f)
            {
                this.ImageName = "0_Golem_Idle_000";
                Image = GameArt.Get(ImageName);
            }
            else if (idleAnimationTimer <= 0.5f)
            {
                this.ImageName = "0_Golem_Idle_008";
                Image = GameArt.Get(ImageName);
            }
            else if (idleAnimationTimer <= 1.0f)
            {
                this.ImageName = "0_Golem_Idle_017";
                Image = GameArt.Get(ImageName);
            }
            else if (idleAnimationTimer <= 1.5f)
            {
                this.ImageName = "0_Golem_Idle Blinking_000";
                Image = GameArt.Get(ImageName);
            }
            else if (idleAnimationTimer <= 1.6f)
            {
                this.ImageName = "0_Golem_Idle Blinking_003";
                Image = GameArt.Get(ImageName);
            }
            else if (idleAnimationTimer <= 1.7f)
            {
                this.ImageName = "0_Golem_Idle Blinking_006";
                Image = GameArt.Get(ImageName);
            }
            else if (idleAnimationTimer <= 1.8f)
            {
                this.ImageName = "0_Golem_Idle Blinking_009";
                Image = GameArt.Get(ImageName);
            }
            else if (idleAnimationTimer <= 1.9f)
            {
                this.ImageName = "0_Golem_Idle Blinking_012";
                Image = GameArt.Get(ImageName);
            }
            else if (idleAnimationTimer <= 2f) {
                this.ImageName = "0_Golem_Idle Blinking_015";
                Image = GameArt.Get(ImageName);
            }
            else if (idleAnimationTimer <= 2.1f)
            {
                this.ImageName = "0_Golem_Idle Blinking_017";
                Image = GameArt.Get(ImageName);
            }
            else if (idleAnimationTimer <= 3f)
            {
                idleAnimationTimer = 0;
            }
        }
    }
}
