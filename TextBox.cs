using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    internal class TextBox
    {
        public string Text { get; set; }
        public Vector2 Position { get; set; }
        public Vector2 Scale { get; set; }
        public SpriteFont Font { get; set; }

        /// <summary>
        /// Creates a new text box
        /// </summary>
        /// <param name="font">Reference to a loaded font</param>
        /// <param name="scale">Scale at which the text is rendered</param>
        /// <param name="position">Position to render the text</param>
        /// <param name="text">Text to render</param>
        public TextBox(SpriteFont font, Vector2 scale, Vector2 position, string text) 
        {
            Font = font;
            Scale = scale;
            Position = position;
            Text = text;
        }
    }
}
