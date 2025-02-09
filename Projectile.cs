using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using EasyMonoGame;

namespace EasyStart
{
    internal class Projectile : Weapons
    {
        private float speed;

        public Projectile() : base()
        {
            this.speed = 170f;
            this.ScaleSprite = .2f;
            this.ScaleRadius = .05f;
        }
        public override void Update(GameTime gameTime)
        {
            this.Move(speed * (float)gameTime.ElapsedGameTime.TotalSeconds);
            if (IsAtEdge())
            {
                this.World.RemoveActor(this);
            }
        }
    }
}
