using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasyMonoGame;
using Microsoft.Xna.Framework;

namespace EasyStart
{
    class WorldShop : World
    {
        private ShopBackground background;
        ShopBackground header;
        ShopBackground energy;
        ShopButton button;
        ShopCloseButton shopCloseButton;

        private float attackCooldown;
        private int money;
        public WorldShop(float attackCooldown, int money) : base(1000, 1000)
        {
            BackgroundTileName = "FieldsTile_38";

            header = new ShopBackground();
            energy = new ShopBackground();
            button = new ShopButton(0.5f, attackCooldown, money);
            shopCloseButton = new ShopCloseButton(attackCooldown, 0.5f);

            this.money = money;

            this.attackCooldown = attackCooldown;

            background = new ShopBackground();
            Add(background, "bg", this.Width / 2, this.Height / 2);
            Add(header, "header", this.Width / 2, this.Height - background.Position.Y * 1.5f);
            Add(energy, "2", this.Width / 2, this.Height - background.Position.Y);
            Add(button, "btn", this.Width / 2, this.Height - background.Position.Y / 1.5f);
            Add(shopCloseButton, "close_2", (int)(background.Position.X * 1.5), (int)(background.Position.Y / 1.5));

            ShowText("Buy Attack speed", (int)button.Position.X, (int)button.Position.Y, 0.3f);
        }

        public override void Act()
        {
            money = button.Money;
            ShowText("Coins: " + money, (int)(background.Position.X / 1.7f), (int)(background.Position.Y / 1.5f), 0.7f);

            attackCooldown = button.AttackCooldown;
            ShowText("Attack speed: " + attackCooldown.ToString("0.00") + "S", (int)(background.Position.X / 1.7f), (int)background.Position.Y, 0.5f);
            shopCloseButton.AttackCooldown = attackCooldown;

            base.Act();
        }
    }
}
