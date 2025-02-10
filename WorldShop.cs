using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasyMonoGame;
using Microsoft.Xna.Framework;

namespace EasyStart
{
    class WorldShop : World
    {
        private Button playButton;
        public WorldShop() : base(1000, 1000)
        {
            BackgroundTileName = "FieldsTile_38";

           playButton = new Button();
           Add(playButton, "play", this.Width / 2, this.Height / 2);
        }
    }
}
