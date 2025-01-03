using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    internal enum ContentType 
    {
        SnakeHead,
        SnakeBody,
        Empty,
        Food
    }

    internal class GridSpace
    {
        private ContentType type;
        private Rectangle dimensions;

        public ContentType Type 
        {
            get { return type; } 
            set { type = value; }
        }

        public Texture2D Texture { get; set; } // Texture for this given space on the grid
        public Vector2 Position { get { return dimensions.Location.ToVector2(); } }
        public Vector2 Size { get { return dimensions.Size.ToVector2(); } }
        public Rectangle Dimensions { get { return dimensions; } }


        public GridSpace(ContentType type, Rectangle dimensions)
        {
            this.type = type;
            this.dimensions = dimensions;
        }
    }
}
