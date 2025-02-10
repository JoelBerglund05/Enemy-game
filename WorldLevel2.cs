using EasyMonoGame;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyStart
{
    internal class WorldLevel2 : World
    {
        private Player player;
        private Ghost ghost;
        private Satyr satyr;

        float[] nextEnemyTimer = { 4f, 8f };
        float levelTimer = 0;

        int maxTurns = 20;
        int turn = 0;

        public WorldLevel2(float attackCooldown) : base(1000, 1000)
        {
            // Tile background with the file "bluerock" in the Content folder.
            BackgroundTileName = "FieldsTile_38";

            player = new Player(attackCooldown);
            Add(player, "0_Golem_Walking_000", this.Width / 2, this.Height / 2);

            ghost = new Ghost(player);
            Add(ghost, "Wraith_01_Moving Forward_000", this.Width / 3, this.Height / 3);

            satyr = new Satyr(player);
            Add(satyr, "Satyr_02_Walking_000", this.Width / 1, this.Height / 1.5f);
        }

        public override void Update(GameTime gameTime)
        {
            int randomX = EasyGame.Instance.Random.Next(0, 1000);
            int randomY = EasyGame.Instance.Random.Next(0, 1000);
            bool isPlayerNear = true;
            while (isPlayerNear)
            {
                if (randomX == player.Position.X && randomY == player.Position.Y)
                {
                    randomX = EasyGame.Instance.Random.Next(0, 1000);
                    randomY = EasyGame.Instance.Random.Next(0, 1000);
                }
                else
                {
                    isPlayerNear = false;
                }
            }


            if (levelTimer >= nextEnemyTimer[0] && levelTimer <= nextEnemyTimer[1] && maxTurns >= turn)
            {
                Satyr satyr = new Satyr(player);
                Add(satyr, "Satyr_02_Walking_000", randomX, randomY);
                turn++;
                nextEnemyTimer[0] += 6.0f;
            }
            else if (levelTimer >= nextEnemyTimer[1] && levelTimer <= nextEnemyTimer[0] && maxTurns >= turn)
            {
                turn++;
                nextEnemyTimer[1] += 6.0f;
                Ghost ghost = new Ghost(player);
                Add(ghost, "Wraith_01_Moving Forward_000", randomX, randomY);
            }
            else if (maxTurns < turn && this.NumberOfActors(typeof(Enemy)) == 0 && this.NumberOfActors(typeof(Player)) == 1 || this.NumberOfActors(typeof(Player)) == 0 && levelTimer > 0.0f)
            {
                this.RemoveActor(player);
                WorldShop nextLevel = new WorldShop(player.AttackDooldown, player.Money);
                EasyGame.Instance.ActiveWorld = nextLevel;
            }

            levelTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            ShowText("Dash cooldown: " + player.DashCooldownTimer.ToString("0"), this.Width / 4, (int)(this.Height / 1.1f), .7f);


            base.Update(gameTime);
        }
    }
}
