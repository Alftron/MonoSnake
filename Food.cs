using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    class Food
    {
        private SpriteBatch spriteBatch;
        private GraphicsDevice graphics;

        private static Texture2D foodTexture;
        private Vector2 position;

        public Food(GraphicsDevice graphics, SpriteBatch spriteBatch, int foodSize)
        {
            this.graphics = graphics;
            this.spriteBatch = spriteBatch;
            this.SetTexture(graphics, foodSize);
        }

        public void Draw()
        {
            spriteBatch.Begin();
            spriteBatch.Draw(foodTexture, position, Color.Red);
            spriteBatch.End();
        }

        public Vector2 GetPosition()
        {
            return this.position;
        }

        public void SetPosition(Vector2 pos)
        {
            this.position = pos;
        }

        private void SetTexture(GraphicsDevice graphics, int foodSize)
        {
            // Create texture
            foodTexture = new Texture2D(graphics, foodSize, foodSize);
            Color[] colorData = new Color[foodSize * foodSize];
            for (int x = 0; x < colorData.Length; x++)
            {
                colorData[x] = Color.Red;
            }
            foodTexture.SetData(colorData);
        }


    }
}
