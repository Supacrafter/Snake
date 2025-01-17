using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Diagnostics;

namespace Snake
{
    /// <summary>
    /// This is a snake game. The goal of this project is to learn the monogame workflow first, then optimize later
    /// </summary>
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private Grid _grid;
        private Snake _snake;

        private Texture2D emptySpaceTexture;
        private Texture2D snakeBodyTexture;
        private Texture2D foodTexture;

        private double timeSinceLastMove; // the time since the snake has moved last in seconds
        private const double secondsPerMove = .25; // the number of seconds that should pass before the snake moves again

        SnakeDirection dirToMove;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            _graphics.PreferredBackBufferHeight = 900;
            _graphics.PreferredBackBufferWidth = 900;

            _grid = new Grid(15, 15, 25); // Create a new grid
            _snake = new Snake(new Vector2(9, 5), SnakeDirection.Right, 5); // create a new snake

            _grid.AddFood();

            dirToMove = _snake.Direction;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            emptySpaceTexture = Content.Load<Texture2D>("spaceTest");
            snakeBodyTexture = Content.Load<Texture2D>("snakeTest");
            foodTexture = Content.Load<Texture2D>("foodTest");

            UpdateGrid();
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // Set direction based on input
            // There is probably a better way of doing this

            // If player sets direction perpendicular to current, and then to the opposite of the previous direction BEFORE the snake moves
            // the snake head will retract into the body, ending the game
            if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                dirToMove = SnakeDirection.Up;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                dirToMove = SnakeDirection.Down;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                dirToMove = SnakeDirection.Left;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                dirToMove = SnakeDirection.Right;
            }
            
            // check if the snake hits itself or if it will be out of bounds by the time it moves next
            if (_snake.IsDead() || _snake.HeadToBeOutOfBounds())
            {
                // replace this with new 'Game over' code later
                Exit();
            }
            
            if (timeSinceLastMove > gameTime.ElapsedGameTime.TotalSeconds + secondsPerMove)
            {
                timeSinceLastMove = gameTime.ElapsedGameTime.TotalSeconds;
                _snake.SetDirection(dirToMove);
                _snake.Move();
            }
            else
            {
                timeSinceLastMove += gameTime.ElapsedGameTime.TotalSeconds;
            }

            // if snake hits food item, add a segment to it
            // will need to add a check to make sure the snake does not extend beyond the grid, in which case the game would be 'won'
            if (_snake.HeadPosition.Equals(_grid.FruitSpace.Position / 25))
            {
                _snake.AddSegment();
                _grid.AddFood();
            }

            UpdateGrid();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin(samplerState: SamplerState.PointWrap); // TODO: Fix sprite wrapping

            // Draw each space in the grid, using its own texture
            foreach (GridSpace space in _grid)
            {
                _spriteBatch.Draw(space.Texture, space.Dimensions, Color.White);
            }

            _spriteBatch.End();

            base.Draw(gameTime);
        }

        private void UpdateGrid()
        {
            // Flush the grid
            foreach (GridSpace space in _grid)
            {
                space.Texture = emptySpaceTexture;
            }

            _grid.FruitSpace.Texture = foodTexture;

            foreach (Vector2 segment in _snake.SnakeBody)
            {
                _grid[(int)segment.X, (int)segment.Y].Texture = snakeBodyTexture;
            }   
        }
    }
}
