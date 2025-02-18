﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Vector2 = Microsoft.Xna.Framework.Vector2;

namespace EasyMonoGame
{
    internal class Text
    {
        private SpriteFont font;
        private string text;
        private Vector2 position;
        private Color color;
        private float size;

        internal Text(SpriteFont font, string text, Vector2 position, Color color, float size = 1f)
        {
            this.font = font;
            this.text = text;
            this.position = position;
            this.color = color;
            this.size = size;
        }

        internal void Draw(SpriteBatch spriteBatch)
        {
            Vector2 center = font.MeasureString(text) / 2;
            spriteBatch.DrawString(
                font, 
                text, 
                position, 
                Color.White,
                0,
                center,
                1f * size, // scale
                SpriteEffects.None,
                0.5f);
        }
    }
}
