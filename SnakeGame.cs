using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Snake
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class SnakeGame : Game
    {

        enum GameState
        {
            Stopped,
            Running,
            Lost
        }

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        private const int snakeSize = 10;
        private const int foodSize = 10;
        private const int updateInterval = 50;

        private int milliSinceUpdate = 0;

        private Snake snake;
        private Food food;
        GameState gameState;

        public SnakeGame()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.PreferredBackBufferHeight = 600;
            graphics.PreferredBackBufferWidth = 800;
            graphics.ApplyChanges();

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

            // TODO: use this.Content to load your game content here
            snake = new Snake(graphics.GraphicsDevice, spriteBatch, snakeSize);
            food = new Food(graphics.GraphicsDevice, spriteBatch, foodSize);
            gameState = GameState.Stopped;
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

            // Set game to run if enter is pressed
            if (Keyboard.GetState().IsKeyDown(Keys.Enter) && gameState == GameState.Stopped)
            {
                // Start the game off
                gameState = GameState.Running;
                // Set a direction to get the snake moving
                snake.SetDirection(Direction.Right);
            }

            // Check for keyboard here but only if we need to!
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
            }

            // TODO: Add your update logic here
            milliSinceUpdate += gameTime.ElapsedGameTime.Milliseconds;

            if (milliSinceUpdate >= updateInterval && gameState == GameState.Running)
            {
                milliSinceUpdate = 0;
                snake.Update();
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
            snake.Draw();
            food.Draw();
            base.Draw(gameTime);
        }
    }
}
