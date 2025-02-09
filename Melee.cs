using EasyMonoGame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace EasyStart
{
    internal class Melee : Weapons
    {
        private float duration;
        IActor player;

        public Melee(IActor player)
        {
            this.ScaleSprite = 0f;
            this.ScaleRadius = .15f;
            this.duration = 0.6f;
            this.player = player;
        }

        public override void Update(GameTime gameTime)
        {
            this.X = player.Position.X;
            this.Y = player.Position.Y;


            this.duration -= (float)gameTime.TotalGameTime.TotalSeconds;
            if (this.duration < 0)
            {
                this.World.RemoveActor(this);
                this.Image = null;
            }


        }
    }
}
