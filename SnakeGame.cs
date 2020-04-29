using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace Snake
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class SnakeGame : Game
    {

        enum GameState
        {
            Menu, // Intro menu
            Stopped, // Lost a life so stopped
            Running, // Game is running,
            Lost // Game over
        }

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        SpriteFont font_s12;
        SpriteFont font_s22;
        SpriteFont font_s40;

        private Snake snake;
        private Food food;
        GameState gameState;
        Random randomNum;

        private const int snakeSize = 10;
        private const int foodSize = 10;
        private const int scoreIncrease = 10;
        private const int initialSpeed = 60;
        private const int maxSpeed = 18;
        private const int speedIncrease = 1;
        private const int initialLives = 3;
        private int updateInterval = 60;

        private int milliSinceUpdate = 0;

        private int score = 0;
        private int lives = initialLives;

        public SnakeGame()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.PreferredBackBufferHeight = 300;
            graphics.PreferredBackBufferWidth = 500;
            graphics.ApplyChanges();

        }

        private void CheckCollision()
        {
            // Grab the position of the snake head first
            Vector2 snakeHeadPos = snake.GetPieces()[0].GetPosition();
            // Check collision
            // Walls
            if (snakeHeadPos.X < 0 || snakeHeadPos.X >= graphics.GraphicsDevice.Viewport.Width ||
                snakeHeadPos.Y < 0 || snakeHeadPos.Y >= graphics.GraphicsDevice.Viewport.Height)
            {
                // Hit one of the walls so lose a life, reset snake and food
                lives--;
                snake.ResetSnake();
                this.SpawnFood();
                updateInterval = initialSpeed;
                gameState = GameState.Stopped;
            }

            // Body
            bool hit = false;
            for (int x = 1; x < snake.GetPieces().Count - 1; x++)
            {
                if (snakeHeadPos == snake.GetPieces()[x].GetPosition())
                {
                    hit = true;
                }
            }
            if (hit)
            {
                // Hit body so lose a life, reset snake and food
                lives--;
                snake.ResetSnake();
                this.SpawnFood();
                updateInterval = initialSpeed;
                gameState = GameState.Stopped;
            }

            // Food
            if (snakeHeadPos == food.GetPosition())
            {
                // Hit a piece of food so grow, increase score, and redraw food
                // Grow snake
                snake.Grow();
                // Redraw food and pass snake so we can check valid spaces
                this.SpawnFood();
                // Increase the score
                score += scoreIncrease;
                // Increase the speed
                this.IncreaseSpeed();
            }
        }

        private void IncreaseSpeed()
        {
            if (updateInterval > maxSpeed)
            {
                this.updateInterval -= speedIncrease;
            }
        }

        private void SpawnFood()
        {
            // Create a new food instance but only on a valid space
            bool validSpace = false;
            Vector2 newPos = new Vector2();
            while (!validSpace)
            {
                validSpace = true;
                newPos.X = randomNum.Next((((graphics.GraphicsDevice.Viewport.Width - foodSize) / 10) + 1)) * foodSize;
                newPos.Y = randomNum.Next((((graphics.GraphicsDevice.Viewport.Height - foodSize) / 10) + 1)) * foodSize;

                // Check the food isn't on top of the snake
                foreach (Piece part in snake.GetPieces())
                {
                    if (newPos == part.GetPosition())
                    {
                        validSpace = false;
                    }
                }
            }
            food.SetPosition(newPos);
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            // I want to see the mouse cursor
            this.IsMouseVisible = true;
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // Add the fonts
            font_s12 = Content.Load<SpriteFont>("Fonts/Font_S12");
            font_s22 = Content.Load<SpriteFont>("Fonts/Font_S22");
            font_s40 = Content.Load<SpriteFont>("Fonts/Font_S40");

            // TODO: use this.Content to load your game content here
            snake = new Snake(graphics.GraphicsDevice, spriteBatch, snakeSize);
            food = new Food(graphics.GraphicsDevice, spriteBatch, foodSize);
            randomNum = new Random();
            gameState = GameState.Menu;
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();


            // Set game to run in direction pressed to start, bit gross but it works
            if (gameState == GameState.Menu || gameState == GameState.Stopped || gameState == GameState.Lost)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.Up))
                {
                    // Reset lives if we need to
                    if (gameState == GameState.Lost) this.lives = initialLives;
                    // Start the game off (again)
                    gameState = GameState.Running;
                    // Set a direction to get the snake moving
                    snake.SetDirection(Direction.Up);
                    // Spawn some food
                    this.SpawnFood();
                }
                else if (Keyboard.GetState().IsKeyDown(Keys.Down))
                {
                    // Reset lives if we need to
                    if (gameState == GameState.Lost) this.lives = initialLives;
                    // Start the game off (again)
                    gameState = GameState.Running;
                    // Set a direction to get the snake moving
                    snake.SetDirection(Direction.Down);
                    // Spawn some food
                    this.SpawnFood();
                }
                else if (Keyboard.GetState().IsKeyDown(Keys.Left))
                {
                    // Reset lives if we need to
                    if (gameState == GameState.Lost) this.lives = initialLives;
                    // Start the game off (again)
                    gameState = GameState.Running;
                    // Set a direction to get the snake moving
                    snake.SetDirection(Direction.Left);
                    // Spawn some food
                    this.SpawnFood();
                }
                else if (Keyboard.GetState().IsKeyDown(Keys.Right))
                {
                    // Reset lives if we need to
                    if (gameState == GameState.Lost) this.lives = initialLives;
                    // Start the game off (again)
                    gameState = GameState.Running;
                    // Set a direction to get the snake moving
                    snake.SetDirection(Direction.Right);
                    // Spawn some food
                    this.SpawnFood();
                }
            }

            // Check for keyboard and do stuff here but only if we need to!
            if (gameState == GameState.Running)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.Up))
                {
                    snake.SetDirection(Direction.Up);
                }
                else if (Keyboard.GetState().IsKeyDown(Keys.Down))
                {
                    snake.SetDirection(Direction.Down);
                }
                else if (Keyboard.GetState().IsKeyDown(Keys.Left))
                {
                    snake.SetDirection(Direction.Left);
                }
                else if (Keyboard.GetState().IsKeyDown(Keys.Right))
                {
                    snake.SetDirection(Direction.Right);
                }

                milliSinceUpdate += gameTime.ElapsedGameTime.Milliseconds;

                if (milliSinceUpdate >= updateInterval && gameState == GameState.Running)
                {
                    milliSinceUpdate = 0;
                    snake.Update();
                    this.CheckCollision();
                    if (lives == 0) gameState = GameState.Lost;
                }
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here
            // Draw the introduction screen and instructions
            if (gameState == GameState.Menu)
            {
                spriteBatch.Begin();
                spriteBatch.DrawString(font_s40, "ssssSNAKE ", new Vector2(0,0), Color.White);
                spriteBatch.DrawString(font_s12, "Press any arrow key to start", new Vector2(0, graphics.GraphicsDevice.Viewport.Height - 20), Color.White);
                spriteBatch.End();
            }
            
            // Draw the game running    
            snake.Draw();
            if (gameState == GameState.Running)
            {
                food.Draw();
            }

            // Draw the game over screen
            if (gameState == GameState.Lost)
            {
                //StringBuilder scoreText = "Score: " + score + " Press any arrow key to retry";
                String scoreText = string.Format("Score: {0}                      Press any arrow key to retry!", this.score);
                spriteBatch.Begin();
                spriteBatch.DrawString(font_s40, "GAME OVER ", new Vector2(0, 0), Color.White);
                spriteBatch.DrawString(font_s12, scoreText, new Vector2(0, graphics.GraphicsDevice.Viewport.Height - 20), Color.White);
                spriteBatch.End();
            }

            // Draw the game over screen and instructions
            base.Draw(gameTime);
        }
    }
}
