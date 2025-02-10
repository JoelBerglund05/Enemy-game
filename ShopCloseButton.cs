using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasyMonoGame;
using Microsoft.Xna.Framework;

namespace EasyStart
{
    internal class ShopCloseButton : Button
    {
        float attackCooldown;
        public ShopCloseButton(float attackCooldown, float ScaleSprite = .2f) : base(ScaleSprite)
        {
            this.ScaleSprite = ScaleSprite;
            this.attackCooldown = attackCooldown;
        }
        public override void Act()
        {
            if (IsButtonClicked())
            {
                EasyGame.Instance.ActiveWorld = new WorldLevel2(attackCooldown);
            }
            base.Act();
        }
        public float AttackCooldown
        {
            set { attackCooldown = value; }
        }
    }
}

