using EasyMonoGame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyStart
{
    internal class StartButton : Button
    {
        public StartButton(float ScaleSprite = .2f) : base(ScaleSprite)
        {
            this.ScaleSprite = ScaleSprite;
        }
        public override void Act()
        {
            if (this.IsButtonClicked())
            {
                EasyGame.Instance.ActiveWorld = new WorldLevel1();
            }
            base.Act();
        }
    }
}
