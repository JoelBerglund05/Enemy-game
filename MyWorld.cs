using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasyMonoGame;

namespace EasyStart
{
    internal class MyWorld : World
    {
        private Player player;

        public MyWorld() : base(1000, 1000)
        {
            // Tile background with the file "bluerock" in the Content folder.
            BackgroundTileName = "bluerock";

            player = new Player();
            Add(player, "man", this.Width / 2, this.Height / 2);

        }  
    }
}
