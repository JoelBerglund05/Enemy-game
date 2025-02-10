using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasyMonoGame;
using Microsoft.Xna.Framework;

namespace EasyStart
{
    internal class ShopButton : Button
    {
        float attackCooldown;
        float clickCooldown;
        int money;
        public ShopButton(float ScaleSprite, float attackCooldown, int money) : base(ScaleSprite)
        {
            this.ScaleSprite = ScaleSprite;
            this.attackCooldown = attackCooldown;
            this.clickCooldown = 0;
            this.money = money;
        }

        public override void Update(GameTime gameTime)
        {
            if (this.IsButtonClicked() && clickCooldown > 0.2f && money > 0 && attackCooldown > 0.3f)
            {
                money--;
                attackCooldown -= 0.01f;
                clickCooldown = 0;
            }

            clickCooldown += (float)gameTime.ElapsedGameTime.TotalSeconds;

            base.Act();
        }

        public float AttackCooldown
        {
            get { return attackCooldown; }
        }

        public int Money
        {
            get { return money; }
        }
    }
}
