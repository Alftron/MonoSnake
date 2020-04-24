using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    class Piece
    {
        private Vector2 position;

        public Piece(Vector2 position)
        {
            this.position = position;
        }

        public Vector2 GetPosition()
        {
            // Returns the position vector
            return this.position;
        }

        public void SetPosition(Vector2 position)
        {
            // Set the position vector
            this.position = position;
        }
    }
}
