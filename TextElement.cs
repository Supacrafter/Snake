﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    /// <summary>
    /// Represents a Text UI element
    /// </summary>
    internal class TextElement
    {
        public string Text { get; set; }
        public Vector2 Position { get; set; }
        public Vector2 Scale { get; set; }
        public SpriteFont Font { get; set; }
        public Color TextColor { get; set; }
        public bool Enabled { get; set; }

        /// <summary>
        /// Creates a new text box
        /// </summary>
        /// <param name="font">Reference to a loaded font</param>
        /// <param name="scale">Scale at which the text is rendered</param>
        /// <param name="position">Position to render the text</param>
        /// <param name="text">Text to render</param>
        public TextElement(SpriteFont font, Vector2 scale, Vector2 position, string text, Color color) 
        {
            Enabled = true;
            Font = font;
            Scale = scale;
            Position = position;
            Text = text;
            TextColor = color;
        }

        public TextElement(SpriteFont font, Vector2 scale, Vector2 position, string text)
        {
            Enabled = true;
            Font = font;
            Scale = scale;
            Position = position;
            Text = text;
            TextColor = Color.White;
        }
    }
}
