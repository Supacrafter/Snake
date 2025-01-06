using Microsoft.Xna.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    /// <summary>
    /// A Grid holds spaces, to be used as a 'game world' for the snake game
    /// </summary>
    internal class Grid : IEnumerable<GridSpace>
    {
        private GridSpace[,] spaces; // All of the spaces within this grid

        public int Rows { get { return spaces.GetLength(0); } }
        public int Columns { get { return spaces.GetLength(1); } }
        public int Size { get { return spaces.Length; } }

        public GridSpace this[int i, int k] { get { return spaces[i, k]; } set { spaces[i, k] = value; } } // indexer property

        /// <summary>
        /// Creates a singleton instance of a grid
        /// </summary>
        /// <param name="rows">The number of rows this grid should have</param>
        /// <param name="columns">The number of columns this grid should have</param>
        /// <param name="spaceSize">The size of each space, in pixels</param>
        public Grid(int rows, int columns, int spaceSize)
        {
            spaces = new GridSpace[rows, columns];
            int xCoord = 0; // current xCoord of the column (in pixels)

            for (int i = 0; i < spaces.GetLength(0); i++)
            {
                int yCoord = 0; // current yCoord of the row (in pixels)
                for (int k = 0; k < spaces.GetLength(1); k++)
                {
                    // make a grid space, as a specified position with a certain type of content
                    spaces[i, k] = new GridSpace(ContentType.Empty, new Rectangle(xCoord, yCoord, spaceSize, spaceSize)); 
                    yCoord += spaceSize; // increment the yCoord by appropriate amt
                }
                xCoord += spaceSize; // increment the xCoord by the appropriate amt
            }
        }

        public void UpdateGrid()
        {

        }

        public IEnumerator<GridSpace> GetEnumerator()
        {
            foreach (GridSpace space in spaces)
            {
                yield return space;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
