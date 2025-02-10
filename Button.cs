using EasyMonoGame;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace EasyStart
{
    internal class Button : Actor
    {
        float buttonSize = 0.2f;
        public Button()
        {
            this.ScaleSprite = .2f;
            this.buttonSize= ScaleSprite * 0.5f;
        }

        public override void Act()
        {
            if (IsButtonClicked())
            {
                EasyGame.Instance.ActiveWorld = new WorldLevel1();
            }

            if (IsMouseOverButton() && this.ScaleSprite < 0.2f * 1.08f)
            {
                this.ScaleSprite *= 1.03f;
            }
            else if (!IsMouseOverButton() && this.ScaleSprite > 0.2f)
            {
                this.ScaleSprite *= 0.97f;
            }
        }

        private bool IsMouseOverButton()
        {
            Vector2 buttonSize = new Vector2(this.Width * this.buttonSize, this.Height * this.buttonSize);
            Vector2 buttonPosition = new Vector2(this.Position.X, this.Position.Y);

            return buttonPosition.X - buttonSize.X < Mouse.GetState().X &&
                   buttonPosition.X + buttonSize.X > Mouse.GetState().X &&
                   buttonPosition.Y - buttonSize.Y < Mouse.GetState().Y &&
                   buttonPosition.Y + buttonSize.Y > Mouse.GetState().Y;
        }

        private bool IsButtonClicked()
        {
            return IsMouseOverButton() && Mouse.GetState().LeftButton == ButtonState.Pressed;
        }
    }
}