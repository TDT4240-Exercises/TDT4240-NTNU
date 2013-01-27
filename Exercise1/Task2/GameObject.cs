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
        private const float MAX_SPEED = 8.0f;
        private Vector2 position;
        private Vector2 velocity;
        private Texture2D texture;
        private Vector2 scaleSize;
        private float rotation;

        public GameObject(Texture2D setTexture)
        {
            texture = setTexture;
            velocity = new Vector2(0, 0);
            position = new Vector2(0, 0);
            scaleSize = new Vector2(1.0f, 1.0f);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, null, Color.White, rotation, new Vector2(0, 0), scaleSize, velocity.X > 0 ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 0);
        }

        public void AddVelocity(float x, float y)
        {
            velocity.X += x;
            velocity.Y += y;
        }

        public void SetScale(float x, float y)
        {
            scaleSize.X = x;
            scaleSize.Y = y;
        }

        public void Update(GameTime gameTime, int screenWidth, int screenHeight)
        {

            position.X = Math.Min(Math.Max(position.X, 0), screenWidth - texture.Bounds.Width * scaleSize.X);
            position.Y = Math.Min(Math.Max(position.Y, 0), screenHeight - texture.Bounds.Height * scaleSize.Y);

            //Update position
            position += velocity;
            rotation = 0.25f / MAX_SPEED * velocity.X;

            //Lose velocity
            velocity *= 0.95f;
        }

        public Vector2 GetPosition()
        {
            return position;
        }

    }
}
