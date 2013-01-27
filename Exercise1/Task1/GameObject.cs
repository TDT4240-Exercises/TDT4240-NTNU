using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Exercise1
{
    class GameObject
    {
        private Vector2 position;
        private Vector2 velocity;
        private Texture2D texture;
        private Vector2 scaleSize;

        public GameObject(Texture2D setTexture)
        {
            texture = setTexture;
            velocity = new Vector2(8, 4);
            position = new Vector2(0, 0);
            scaleSize = new Vector2(1.0f, 1.0f);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, null, Color.White, 0, new Vector2(0, 0), scaleSize, velocity.X > 0 ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 0);
        }

        public void Update(GameTime gameTime, int screenWidth, int screenHeight)
        {
            //Bounce X direction
            if (screenWidth < position.X + texture.Bounds.Width || position.X < 0)
            {
                velocity.X = -velocity.X;
            }

            //Bounce Y direction
            if (screenHeight < position.Y + texture.Bounds.Height || position.Y < 0)
            {
                velocity.Y = -velocity.Y;
            }

            //Update position
            position += velocity;
        }

    }
}
