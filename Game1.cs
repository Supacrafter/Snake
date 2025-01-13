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

        private bool spacebarDownPrevFrame;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            _grid = new Grid(15, 15, 25); // Create a new grid
            _snake = new Snake(new Vector2(9, 5), SnakeDirection.Right, 5); // create a new snake

            spacebarDownPrevFrame = false;
            
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            emptySpaceTexture = Content.Load<Texture2D>("spaceTest");
            snakeBodyTexture = Content.Load<Texture2D>("snakeTest");

            UpdateGrid();
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // Set direction based on input
            // There is probably a better way of doing this
            if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                _snake.SetDirection(SnakeDirection.Up);
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                _snake.SetDirection(SnakeDirection.Down);
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                _snake.SetDirection(SnakeDirection.Left);
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                _snake.SetDirection(SnakeDirection.Right);
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Space) && !spacebarDownPrevFrame)
            {
                spacebarDownPrevFrame = true;
            }
            
            if (!Keyboard.GetState().IsKeyDown(Keys.Space) && spacebarDownPrevFrame)
            {
                _snake.Move();
                spacebarDownPrevFrame = false;
            }

            UpdateGrid();

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

        private void UpdateGrid()
        {
            // Flush the grid
            foreach (GridSpace space in _grid)
            {
                space.Texture = emptySpaceTexture;
            }

            foreach (Vector2 segment in _snake.SnakeBody)
            {
                _grid[(int)segment.X, (int)segment.Y].Texture = snakeBodyTexture;
            }
        }
    }
}
