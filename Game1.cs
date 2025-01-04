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

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            _grid = new Grid(15, 15, 25); // Create a new grid
            _snake = new Snake(new Vector2(9, 5), new Vector2(1, 2), 5); // create a new snake

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            emptySpaceTexture = Content.Load<Texture2D>("spaceTest");
            snakeBodyTexture = Content.Load<Texture2D>("snakeTest");

            /*
             * This is a part of the code that is prime for optimization
             */
            for (int i = _grid.Rows-1; i >= 0; i--)
            {
                for (int k = 0; k < _grid.Columns; k++)
                {
                    GridSpace space = _grid[i, k];
                    foreach (Vector2 segment in _snake.SnakeBody)
                    {
                        // If the space we are checking is not null or an empty space, skip it
                        if (space.Texture != null && space.Texture != emptySpaceTexture)
                        {
                            continue;
                        }

                        if (space.Position.Equals(segment * 25))
                        {
                            space.Texture = snakeBodyTexture;
                        }
                        else
                        {
                            space.Texture = emptySpaceTexture;
                        }
                    }
                }
            }
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // Set direction based on input
            // There is probably a better way of doing this
            if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                _snake.SetDirection(0, 1);
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                _snake.SetDirection(0, -1);
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                _snake.SetDirection(-1, 0);
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                _snake.SetDirection(1, 0);
            }

            // if snake head touches any part of its body, game over

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
    }
}
