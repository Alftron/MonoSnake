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

        private static Texture2D foodTexture;
        private Vector2 position;

        public Food(GraphicsDevice graphics, SpriteBatch spriteBatch, int foodSize)
        {
            this.spriteBatch = spriteBatch;
            this.SetTexture(graphics, foodSize);
        }

        public void Draw()
        {
            spriteBatch.Begin();
            // TODO: Random positioning of food 
            spriteBatch.Draw(foodTexture, new Vector2(0, 0), Color.Red);
            spriteBatch.End();
        }

        public Vector2 GetPosition()
        {
            return this.position;
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
