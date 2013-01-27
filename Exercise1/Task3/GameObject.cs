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
        private Vector2 velocity;
        private Texture2D texture;
        private float rotation;
        private Rectangle boundingBox;
        private Rectangle spriteFrame;
        private double lastFrameUpdate;

        public GameObject(Texture2D setTexture, float spawnX, float spawnY, float xVelocity, float yVelocity)
        {
            texture = setTexture;
            velocity = new Vector2(xVelocity, yVelocity);

            boundingBox = new Rectangle((int)spawnX, (int)spawnY, 130, 52);
            spriteFrame = new Rectangle(0, 0, 130, 52);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, boundingBox, spriteFrame, Color.White, rotation, new Vector2(), velocity.X > 0 ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 0);
        }

        public void AddVelocity(float x, float y)
        {
            velocity.X += x;
            velocity.Y += y;
        }

        public void Update(GameTime gameTime, int screenWidth, int screenHeight)
        {
            //Bounce X direction
            if (screenWidth < boundingBox.Right || boundingBox.X < 0)
            {
                velocity.X = -velocity.X;
            }

            //Bounce Y direction
            if (screenHeight < boundingBox.Bottom || boundingBox.Y < 0)
            {
                velocity.Y = -velocity.Y;
            }

            if (gameTime.TotalGameTime.TotalMilliseconds > lastFrameUpdate)
            {
                lastFrameUpdate = gameTime.TotalGameTime.TotalMilliseconds + 100; //100 ms until next frame
                spriteFrame.X += spriteFrame.Width;
                if (spriteFrame.X >= texture.Bounds.Width) spriteFrame.X = 0;
            }

            //Update position
            boundingBox.X += (int)velocity.X;
            boundingBox.Y += (int)velocity.Y;
            rotation = 0.25f / MAX_SPEED * velocity.X;
        }

        public void CheckCollision(GameObject other)
        {
            
            //Check box collision
            if (boundingBox.Intersects(other.boundingBox))
            {
                other.velocity.X = -other.velocity.X;
                other.velocity.Y = -other.velocity.Y;
                velocity.X = -velocity.X;
                velocity.Y = -velocity.Y;
            }
        }

    }
}
