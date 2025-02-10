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
        float buttonSize;
        float buttonBaseSize;
        public Button(float ScaleSprite = 0.2f)
        {
            this.ScaleSprite = .2f;
            this.buttonBaseSize = ScaleSprite;
            this.buttonSize = ScaleSprite * 0.5f;
        }

        public override void Act()
        {
            if (IsMouseOverButton() && this.ScaleSprite < buttonBaseSize * 1.08f)
            {
                this.ScaleSprite *= 1.03f;
            }
            else if (!IsMouseOverButton() && this.ScaleSprite > buttonBaseSize)
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

        protected bool IsButtonClicked()
        {
            return IsMouseOverButton() && Mouse.GetState().LeftButton == ButtonState.Pressed;
        }
    }
}