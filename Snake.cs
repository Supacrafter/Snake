using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    public enum SnakeDirection
    {
        Up,
        Down,
        Left,
        Right
    }

    /// <summary>
    /// Handles all the snake behavior, including input handling and the 'trailing' of the body
    /// </summary>
    internal class Snake
    {
        private Vector2 headPosition; // what the rest of the body will follow. not to be modified directly by external classes
        private List<Vector2> snakeBody; // the entire body of the snake, including the head
        private Vector2 direction; // a vector2 to represent which direction the snake head should move

        public Vector2 HeadPosition { get { return headPosition; } }
        public List<Vector2> SnakeBody { get { return snakeBody; } }
        public Vector2 Direction { get { return direction; } }

        /// <summary>
        /// Creates a snake with an inital positon, direction, and size
        /// </summary>
        /// <param name="initPosition">The initial position (on the grid) the snake head should be placed</param>
        /// <param name="initDirection">The initial direction of the snake</param>
        /// <param name="size">The number of segments the snake should start with</param>
        public Snake(Vector2 initPosition, Vector2 initDirection, int size)
        {
            headPosition = initPosition;
            SetDirection(initDirection);

            snakeBody = new List<Vector2>(0);

            // Set position of the snake body
            Vector2 offset = new Vector2(0, 0);
            for (int i = 0; i < size; i++)
            {
                snakeBody.Add(initPosition - (offset * direction));
                offset += Vector2.One;
            }
        }

        /// <summary>
        /// Sets the direction of the snake
        /// </summary>
        /// <param name="newDirection">The new direction the snake should head in</param>
        /// <exception cref="ArgumentOutOfRangeException">If the direction is not (+/- 1, 0) or (0, +/- 1)</exception>
        public void SetDirection(Vector2 newDirection)
        {
            // TODO: Fix direction setting. Can set diagonals
            if (newDirection.X != 1 ^ newDirection.Y != 1 || newDirection.X != -1 ^ newDirection.Y != -1)
            {
                throw new ArgumentOutOfRangeException(nameof(newDirection), "The specified direction is not valid: Supplied direction is not (+/- 1, 0) or (0, +/- 1)");
            }

            direction = newDirection;
        }

        /// <summary>
        /// Sets the direction of the snake
        /// </summary>
        /// <param name="x">The x component of the direction the snake should head</param>
        /// <param name="y">The y component of the direction the snake should head</param>
        public void SetDirection(int x, int y)
        {
            SetDirection(new Vector2(x, y));
        }

        /// <summary>
        /// Sets the direction of the snake
        /// </summary>
        /// <param name="direction">The direction the snake should head</param>
        /// <exception cref="NotImplementedException"></exception>
        public void SetDirection(SnakeDirection direction)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Adds a segment to the body of the snake
        /// </summary>
        public void AddSegment()
        {
            Vector2 segementPosition = snakeBody[snakeBody.Count-1];
            Vector2 offest = Vector2.One * direction;

            snakeBody.Add(segementPosition + -offest);
        }
    }
}
