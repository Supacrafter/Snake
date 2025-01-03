using Microsoft.Xna.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    internal class Grid : IEnumerable<GridSpace>
    {
        private GridSpace[,] spaces;
        
        public int Rows { get { return spaces.GetLength(0); } }
        public int Columns { get { return spaces.GetLength(1); } }

        public GridSpace this[int i, int k]
        {
            get
            {
                return spaces[i, k];
            }
            set
            {
                spaces[i, k] = value;
            }
        }

        public Grid(int rows, int columns, int spaceSize)
        {
            spaces = new GridSpace[rows, columns];
            int xCoord = 0;

            for (int i = 0; i < spaces.GetLength(0); i++)
            {
                int yCoord = 0;
                for (int k = 0; k < spaces.GetLength(1); k++)
                {
                    spaces[i, k] = new GridSpace(ContentType.Empty, new Rectangle(xCoord, yCoord, spaceSize, spaceSize));
                    yCoord += spaceSize;
                }
                xCoord += spaceSize;
            }

            /* 
             * Make spaces the right size
             * Move them to appropriate spot (organize by rows and columns)
             */
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
