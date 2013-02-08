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
        SpriteFont font;

        private Viewport viewPort;
        private Texture2D whiteTexture;
        private Rectangle leftPaddle;
        private Rectangle rightPaddle;
        private Rectangle ball;
        private Vector2 ballVelocity;

        private const int PADDLE_WIDTH = 20;
        private const int BALL_SIZE = 10;

        private int lPaddleMovementSpeed = 3;
        private int rPaddleMovementSpeed = 3;

        private int lPaddlePoints = 0;
        private int rPaddlePoints = 0;

        private enum GameState
        {
            START,
            PLAYING,
            GAMEOVER
        };

        GameState state = GameState.START;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "PongContent";
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

            font = Content.Load<SpriteFont>("SpriteFont1");


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

        private void HandleInput(GameTime gameTime)
        {
            KeyboardState keyboard = Keyboard.GetState();

            // Allows the game to exit
            if (keyboard.IsKeyDown(Keys.Escape))
            {
                this.Exit();
            }

            // Paddle movement
            bool lPlayerUp = keyboard.IsKeyDown(Keys.W);
            bool lPlayerDn = keyboard.IsKeyDown(Keys.S);

            bool rPlayerUp = keyboard.IsKeyDown(Keys.O);
            bool rPlayerDn = keyboard.IsKeyDown(Keys.L);

            if (lPlayerUp ^ lPlayerDn)
                if (lPlayerUp)  leftPaddle.Y -= lPaddleMovementSpeed;
                else            leftPaddle.Y += lPaddleMovementSpeed;
            if (rPlayerUp ^ rPlayerDn)
                if (rPlayerUp)  rightPaddle.Y -= rPaddleMovementSpeed;
                else            rightPaddle.Y += rPaddleMovementSpeed;

            if (state != GameState.PLAYING && keyboard.IsKeyDown(Keys.Space))
            {
                state = GameState.PLAYING;
                ball.X = viewPort.Width / 2;
                ball.Y = viewPort.Height / 2;
                lPaddlePoints = 0;
                rPaddlePoints = 0;
            }
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            HandleInput(gameTime);

            switch (state)
            {
                case GameState.START:
                    break;

                case GameState.PLAYING:
                    // TODO: Add your update logic here
                    ball.X += (int)ballVelocity.X;
                    ball.Y += (int)ballVelocity.Y;

                    //Bounce left wall
                    if (ball.X < PADDLE_WIDTH)
                    {
                        if (ball.Y + ball.Height > leftPaddle.Y && ball.Y < leftPaddle.Y + leftPaddle.Height)
                        {
                            ball.X = PADDLE_WIDTH;
                            ballVelocity.X = -ballVelocity.X;
                            ballVelocity.Y = ((ball.Y + (ball.Height / 2)) - (leftPaddle.Y + (leftPaddle.Height / 2))) / 8;
                        }
                        else
                        {
                            rPaddlePoints++;
                            ball.X = viewPort.Width / 2;
                            ball.Y = viewPort.Height / 2;
                        }
                    }

                    //Bounce right wall
                    else if (ball.X + BALL_SIZE > viewPort.Width - PADDLE_WIDTH)
                    {
                        if (ball.Y + ball.Height > rightPaddle.Y && ball.Y < rightPaddle.Y + rightPaddle.Height)
                        {
                            ball.X = viewPort.Width - BALL_SIZE - PADDLE_WIDTH;
                            ballVelocity.X = -ballVelocity.X;
                            ballVelocity.Y = ((ball.Y + (ball.Height / 2)) - (rightPaddle.Y + (rightPaddle.Height / 2))) / 8;
                        }
                        else
                        {
                            lPaddlePoints++;
                            ball.X = viewPort.Width / 2;
                            ball.Y = viewPort.Height / 2;
                        }
                    }

                    if (ball.Y < 0)
                    {
                        ball.Y = 0;
                        ballVelocity.Y = -ballVelocity.Y;
                    }
                    else if (ball.Y + ball.Height > viewPort.Height)
                    {
                        ball.Y = viewPort.Height - ball.Height;
                        ballVelocity.Y = -ballVelocity.Y;
                    }
                    break;

                case GameState.GAMEOVER:
                    break;
            }

            if (lPaddlePoints == 21 || rPaddlePoints == 21) state = GameState.GAMEOVER;

            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            

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
            spriteBatch.DrawString(font, lPaddlePoints.ToString(), new Vector2(viewPort.Width / 4, 20), Color.AliceBlue);
            spriteBatch.DrawString(font, rPaddlePoints.ToString(), new Vector2((viewPort.Width * 3) / 4, 20), Color.AliceBlue);

            switch (state)
            {
                case GameState.START:
                    // Write "Press space to play"
                    spriteBatch.DrawString(font, "Press SPACE to play", new Vector2((viewPort.Width / 2) - 100, (viewPort.Height / 2) - 40), Color.AliceBlue);
                    break;

                case GameState.GAMEOVER:
                    // Write winner and "Press space to play again"
                    int winnerPosX = (viewPort.Width / 4) * (lPaddlePoints > rPaddlePoints ? 1 : 3);
                    spriteBatch.DrawString(font, "Press SPACE to play again", new Vector2((viewPort.Width / 2) - 140, (viewPort.Height / 2) - 135), Color.AliceBlue);
                    spriteBatch.DrawString(font, "Winner", new Vector2(winnerPosX - 30, (viewPort.Height / 2) - 40), Color.AliceBlue);
                    break;

                default:
                    break;
            }

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
