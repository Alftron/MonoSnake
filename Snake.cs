using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    enum Direction
    {
        None,
        Up,
        Down,
        Left,
        Right
    }

    class Snake
    {
        private SpriteBatch spriteBatch;
        private GraphicsDevice graphics;

        private static Texture2D snakeTexture;
        private List<Piece> snakePieces; // Container for all the snake pieces including head
        private const int initialPieces = 4; // Initial pieces of the snake we want to create including head
        private int snakeSize = 10;

        private Direction direction; // The direction of the snake

        public Snake(GraphicsDevice graphics, SpriteBatch spriteBatch, int snakeSize)
        {
            this.graphics = graphics;
            this.spriteBatch = spriteBatch;

            // Set the texture for the snake!
            SetTexture(graphics, snakeSize);
            // Set the snake size member here
            this.snakeSize = snakeSize;
            // Create the snake container
            snakePieces = new List<Piece>();

            this.ResetSnake();
        }

        public void Draw()
        {
            spriteBatch.Begin();
            foreach (Piece part in snakePieces)
            {
                spriteBatch.Draw(snakeTexture, part.GetPosition(), Color.White);
            }
            spriteBatch.End();
        }

        public void Update()
        {
            // Move the snake
            if (this.direction != Direction.None)
            {
                this.Move();
            }
        }

        public List<Piece> GetPieces()
        {
            // Returns the snake pieces
            return snakePieces;
        }

        public void Grow()
        {
            // Eaten some food so increase the length but be wary of the snaking pattern/direction
        }

        public void ResetSnake()
        {
            // Give an initial position that's in the center
            Vector2 initialPos = new Vector2()
            {
                X = graphics.Viewport.Width / 2,
                Y = graphics.Viewport.Height / 2
            };

            // Empty out the snake if we have to!
            if (snakePieces.Count > 0)
            {
                snakePieces.Clear();
            }
            // Add all the pieces to the snake container
            for (int x = 0; x < initialPieces; x++)
            {
                // Create a new piece
                snakePieces.Add(new Piece(initialPos));
                // Change the vector for the other initial pieces
                initialPos.X -= snakeSize;
            }
            // Ensure the snake isn't moving to start with
            this.SetDirection(Direction.None);
        }

        public void SetDirection(Direction dir)
        {
            // Could probably do the opposite direction checking here?
            if (dir == Direction.Up && this.direction != Direction.Down)
            {
                this.direction = Direction.Up;
            }
            else if (dir == Direction.Down && this.direction != Direction.Up)
            {
                this.direction = Direction.Down;
            }
            else if (dir == Direction.Left && this.direction != Direction.Right)
            {
                this.direction = Direction.Left;
            }
            else if (dir == Direction.Right && this.direction != Direction.Left)
            {
                this.direction = Direction.Right;
            }
        }

        private void Move()
        {
            // Head should move to new position, and other pieces follow previous one, so work in reverse?
            Vector2 newPos;
            for (int i = snakePieces.Count - 1; i > 0; i--)
            {
                newPos = snakePieces[i - 1].GetPosition();
                snakePieces[i].SetPosition(newPos);
            }
            // Moved the tail now the head needs to move
            Vector2 currPos = snakePieces[0].GetPosition();
            switch (this.direction)
            {
                case Direction.None:
                    // Do nothing
                    break;
                case Direction.Up:
                    // Moving upwards
                    snakePieces[0].SetPosition(new Vector2 { X = currPos.X, Y = currPos.Y -= snakeSize });
                    break;
                case Direction.Down:
                    // Moving downwards
                    snakePieces[0].SetPosition(new Vector2 { X = currPos.X, Y = currPos.Y += snakeSize });
                    break;
                case Direction.Left:
                    // Moving left
                    snakePieces[0].SetPosition(new Vector2 { X = currPos.X -= this.snakeSize, Y = currPos.Y });
                    break;
                case Direction.Right:
                    // Moving right
                    snakePieces[0].SetPosition(new Vector2 { X = currPos.X += this.snakeSize, Y = currPos.Y});
                    break;
                default:
                    break;
            }
        }
        
        private void SetTexture(GraphicsDevice graphics, int snakeSize)
        {
            // Create texture
            snakeTexture = new Texture2D(graphics, snakeSize, snakeSize);
            Color[] colorData = new Color[snakeSize * snakeSize];
            for (int x = 0; x < colorData.Length; x++)
            {
                colorData[x] = Color.White;
            }
            snakeTexture.SetData(colorData);
        }
    }
}
