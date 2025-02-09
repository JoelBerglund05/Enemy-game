using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using EasyMonoGame;

namespace EasyStart
{
    internal class MyWorld : World
    {
        private Player player;
        private Ghost ghost;
        private Satyr satyr;

        public MyWorld() : base(1000, 1000)
        {
            // Tile background with the file "bluerock" in the Content folder.
            BackgroundTileName = "FieldsTile_38";

            player = new Player();
            Add(player, "0_Golem_Walking_000", this.Width / 2, this.Height / 2);

            ghost = new Ghost(player);
            Add(ghost, "Wraith_01_Moving Forward_000", this.Width / 3, this.Height / 3);

            satyr = new Satyr(player);
            Add(satyr, "Satyr_02_Walking_000", this.Width / 1, this.Height / 1.5f);
        }
        
    }
}
