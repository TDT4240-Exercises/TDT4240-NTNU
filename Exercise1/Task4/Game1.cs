using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Task4
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        private Viewport viewPort;
        private Texture2D whiteTexture;
        private Rectangle leftPaddle;
        private Rectangle rightPaddle;
        private Rectangle ball;
        private Vector2 ballVelocity;

        private const int PADDLE_WIDTH = 20;
        private const int BALL_SIZE = 10;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
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

            whiteTexture = new Texture2D(GraphicsDevice, 1, 1);
            whiteTexture.SetData(new[] { Color.White });

            viewPort = graphics.GraphicsDevice.Viewport;

            leftPaddle = new Rectangle(0, 200, PADDLE_WIDTH, 50);
            rightPaddle = new Rectangle(viewPort.Width - PADDLE_WIDTH, 200, PADDLE_WIDTH, 50);
            ball = new Rectangle(viewPort.Width / 2, viewPort.Height / 2, BALL_SIZE, BALL_SIZE);

            ballVelocity = new Vector2(3, 0);

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
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
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
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // TODO: Add your update logic here
            ball.X += (int)ballVelocity.X;
            ball.Y += (int)ballVelocity.Y;

            //Bounce left wall
            if (ball.X < 0)
            {
                ball.X = 0;
                ballVelocity.X = -ballVelocity.X;
            }

            //Bounce right wall
            else if (ball.X + BALL_SIZE > viewPort.Width)
            {
                ball.X = viewPort.Width - BALL_SIZE;
                ballVelocity.X = -ballVelocity.X;
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
            spriteBatch.Begin();
            spriteBatch.Draw(whiteTexture, leftPaddle, Color.White);
            spriteBatch.Draw(whiteTexture, rightPaddle, Color.White);
            spriteBatch.Draw(whiteTexture, ball, Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
