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
        private GridSpace foodSpace; // the space the fruit is on
        private Random rng; // random number generator for placing fruit
        public int Rows { get { return spaces.GetLength(0); } }
        public int Columns { get { return spaces.GetLength(1); } }
        public int Size { get { return spaces.Length; } }
        public GridSpace FoodSpace { get { return foodSpace; } } // the space the fruit is on
        public static Grid Instance { get; private set; }
        public GridSpace this[int i, int k] { get { return spaces[i, k]; } set { spaces[i, k] = value; } } // indexer property

        /// <summary>
        /// Creates a singleton instance of a grid
        /// </summary>
        /// <param name="rows">The number of rows this grid should have</param>
        /// <param name="columns">The number of columns this grid should have</param>
        /// <param name="spaceSize">The size of each space, in pixels</param>
        public Grid(int rows, int columns, int spaceSize)
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                return;
            }

            rng = new Random();

            spaces = new GridSpace[rows, columns];
            int xCoord = 0; // current xCoord of the column (in pixels)

            for (int i = 0; i < spaces.GetLength(0); i++)
            {
                int yCoord = 0; // current yCoord of the row (in pixels)
                for (int k = 0; k < spaces.GetLength(1); k++)
                {
                    // make a grid space, as a specified position with a certain type of content
                    spaces[i, k] = new GridSpace(new Rectangle(xCoord, yCoord, spaceSize, spaceSize)); 
                    yCoord += spaceSize; // increment the yCoord by appropriate amt
                }
                xCoord += spaceSize; // increment the xCoord by the appropriate amt
            }
        }

        /// <summary>
        /// Adds a food item onto the grid at a random location not occupied by the snake
        /// </summary>
        public void AddFood()
        {
            List<Vector2> invalidPositions = Snake.Instance.SnakeBody;

            bool done = false;

            // loop until a valid space is selected
            while (!done)
            {
                Vector2 selectedPosition = new Vector2(rng.Next(Columns), rng.Next(Rows));

                if (!invalidPositions.Contains(selectedPosition))
                {
                    foodSpace = spaces[(int)selectedPosition.X, (int)selectedPosition.Y];
                    done = true;
                }
            }
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
