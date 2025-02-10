using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using EasyMonoGame;
using Microsoft.Xna.Framework;

namespace EasyStart
{
    internal class MyWorld : World
    {
        private Button playButton;
        public MyWorld() : base(1000, 1000)
        {
            BackgroundTileName = "FieldsTile_38";

            playButton = new StartButton();
            Add(playButton, "play", this.Width / 2, this.Height / 2);
        }

    }
}
