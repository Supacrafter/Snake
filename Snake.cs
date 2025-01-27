using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    internal enum SnakeDirection
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
        private List<Vector2> snakeBody; // the entire body of the snake, including the head
        private Vector2 directionVector; // a vector2 to represent which direction the snake head should move
        private SnakeDirection direction; // enum for Direction;

        public Vector2 HeadPosition { get { return snakeBody[0]; } } // head of the snake, all other segments follow this
        public List<Vector2> SnakeBody { get { return snakeBody; } }
        public SnakeDirection Direction { get { return direction; } }
        public bool CanMove { get; set; }
        public static Snake Instance { get; private set; }


        /// <summary>
        /// Creates a snake with an inital positon, direction, and size
        /// </summary>
        /// <param name="initPosition">The initial position (on the grid) the snake head should be placed</param>
        /// <param name="initDirection">The initial direction of the snake</param>
        /// <param name="size">The number of segments the snake should start with</param>
        public Snake(Vector2 initPosition, SnakeDirection initDirection, int size)
        {
            // singleton
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                return;
            }

            directionVector = DirectionToVector(initDirection);

            snakeBody = new List<Vector2>(0);

            // Set position of the snake body
            Vector2 offset = new Vector2(0, 0);
            for (int i = 0; i < size; i++)
            {
                snakeBody.Add(initPosition - (offset * directionVector));
                offset += Vector2.One;
            }

            CanMove = true;
        }

        /// <summary>
        /// Sets the direction of the snake
        /// </summary>
        /// <param name="direction">The direction the snake should head</param>
        public void SetDirection(SnakeDirection direction)
        {
            // shouldnt change directions if we are going to instantly flip backwards
            if (this.directionVector == DirectionToVector(GetOppositeDirection(direction)))
            {
                return;
            }

            this.direction = direction;
            directionVector = DirectionToVector(direction);
        }

        /// <summary>
        /// Converts from a Direction to an appropriate Vector2 (acts like a cast)
        /// </summary>
        /// <param name="direction">Direction to convert</param>
        /// <returns>A Vector2 representing the converted direction</returns>
        /// <exception cref="ArgumentException">Something went horribly wrong within this conversion</exception>
        public static Vector2 DirectionToVector(SnakeDirection direction)
        {
            switch (direction)
            {
                case SnakeDirection.Up:
                    return -Vector2.UnitY;
                case SnakeDirection.Down:
                    return Vector2.UnitY;
                case SnakeDirection.Left:
                    return -Vector2.UnitX;
                case SnakeDirection.Right:
                    return Vector2.UnitX;
                default:
                    throw new ArgumentException("Something went horribly wrong in this conversion");
            }
        }

        /// <summary>
        /// Gets the opposite direction of a supplied one
        /// </summary>
        /// <param name="direction">Direction to find opposite of</param>
        /// <returns>The opposite direction</returns>
        public static SnakeDirection GetOppositeDirection(SnakeDirection direction)
        {
            if ((int)direction % 2 == 1)
            {
                return direction - 1;
            }
            else
            {
                return direction + 1;
            }
        }

        /// <summary>
        /// Adds a segment to the body of the snake
        /// </summary>
        public void AddSegment()
        {
            Vector2 segementPosition = snakeBody[snakeBody.Count-1];
            Vector2 offset = snakeBody[snakeBody.Count - 1] - segementPosition;

            snakeBody.Add(segementPosition + offset);
        }

        /// <summary>
        /// Moves the head 1 space in direction, and all other segments follow the one infront of it
        /// </summary>
        public void Move()
        {
            if (CanMove)
            {
                for (int i = snakeBody.Count - 1; i >= 1; i--)
                {
                    snakeBody[i] = snakeBody[i - 1];
                }
                snakeBody[0] += directionVector;
            }
        }

        /// <summary>
        /// Checks if the snake is dead via hitting itself
        /// </summary>
        /// <returns>If the snake is dead</returns>
        public bool IsDead()
        {
            for (int i = 1; i < snakeBody.Count; i++)
            {
                if (snakeBody[i].Equals(snakeBody[0]))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Checks if the head is about to be out of bounds upon next Move() call
        /// </summary>
        /// <returns>If the head will be out of bounds</returns>
        public bool HeadToBeOutOfBounds()
        {
            // where the head will be if it were to move
            int headX = (int)(HeadPosition.X + directionVector.X);
            int headY = (int)(HeadPosition.Y + directionVector.Y);

            return !(headX > -1 && headX < Grid.Instance.Columns && headY > -1 && headY < Grid.Instance.Rows); // inverse of if the snake is in bounds
        }
    }
}
