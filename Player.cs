﻿using System;
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
        private PlayerState state;
        public Player() {
            speed = 100f;
            this.ScaleSprite = .1f;
            this.ScaleRadius = .1f;
            walkingAnimationTimer = 0;
            string[] imageNames = { "0_Golem_Walking_000", "0_Golem_Walking_023", "0_Golem_Walking_009", 
                "0_Golem_Idle_000", "0_Golem_Idle_008", "0_Golem_Idle_017", "0_Golem_Idle Blinking_000",
                "0_Golem_Idle Blinking_003", "0_Golem_Idle Blinking_006", "0_Golem_Idle Blinking_009",
                "0_Golem_Idle Blinking_012", "0_Golem_Idle Blinking_015", "0_Golem_Idle Blinking_017"};
            GameArt.Add(imageNames);
            this.state = PlayerState.Idle;
        }

        public override void Update(GameTime gameTime)
        {
            this.Movement(gameTime);

            this.TurnTowards(Mouse.GetState().X, Mouse.GetState().Y);

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

        private void AnimationManager(GameTime gameTime)
        {
            if (this.state == PlayerState.Walking)
            {
                this.Walk(gameTime);
            }
            else if (this.state == PlayerState.Idle)
            {
                this.Idle(gameTime);
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
