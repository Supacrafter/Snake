using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    /// <summary>
    /// A class representing a single space on a grid
    /// </summary>
    internal class GridSpace
    {
        private Rectangle dimensions; // Dimensions of this space
        public Texture2D Texture { get; set; } // Texture for this given space on the grid
        public Vector2 Position { get { return dimensions.Location.ToVector2(); } }
        public Vector2 Size { get { return dimensions.Size.ToVector2(); } }
        public Rectangle Dimensions { get { return dimensions; } }

        /// <summary>
        /// Creates a new space with a type and dimensions
        /// </summary>
        /// <param name="type">The type of content held in this space</param>
        /// <param name="dimensions">A rectangle representing the dimensions of this space (Position + Size)</param>
        public GridSpace(Rectangle dimensions)
        {
            this.dimensions = dimensions;
        }
    }
}
