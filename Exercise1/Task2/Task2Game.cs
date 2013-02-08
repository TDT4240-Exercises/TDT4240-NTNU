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

namespace Exercise1
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Task2Game : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        private GameObject helicopter;
        private TimeSpan resizeTimeout;
        private SpriteFont font;

        public Task2Game()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content2";
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
            Texture2D texture = Content.Load<Texture2D>("HelicopterTexture");
            helicopter = new GameObject(texture);

            font = Content.Load<SpriteFont>("DefaultFont");
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
            GamePadState gamePad = GamePad.GetState(PlayerIndex.One);
            KeyboardState keyboard = Keyboard.GetState();

            // Allows the game to exit
            if (gamePad.IsButtonDown(Buttons.Back) || keyboard.IsKeyDown(Keys.Escape))
            {
                this.Exit();
            }

            //Movement
            bool moveLeft = gamePad.ThumbSticks.Left.X < 0 || keyboard.IsKeyDown(Keys.Left);
            bool moveRight = gamePad.ThumbSticks.Left.X > 0 || keyboard.IsKeyDown(Keys.Right);
            bool moveUp = gamePad.ThumbSticks.Left.Y < 0 || keyboard.IsKeyDown(Keys.Up);
            bool moveDown = gamePad.ThumbSticks.Left.Y > 0 || keyboard.IsKeyDown(Keys.Down);

            if (moveLeft) helicopter.AddVelocity(-0.5f, 0.0f);
            else if (moveRight) helicopter.AddVelocity(0.5f, 0.0f);
            if (moveUp) helicopter.AddVelocity(0.0f, -0.25f);
            else if (moveDown) helicopter.AddVelocity(0.0f, 0.5f);

            //Resize helicopter
            if (resizeTimeout < gameTime.TotalGameTime && (gamePad.IsButtonDown(Buttons.A) || keyboard.IsKeyDown(Keys.S)))
            {
                resizeTimeout = gameTime.TotalGameTime.Add(new TimeSpan(5000*1000));       //wait before triggering again
                Random rand = new Random();
                float scale = 1.1f - (float)rand.NextDouble();                             //random scale from 0.1f to 1.1f
                helicopter.SetScale(scale, scale);
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

            // TODO: Add your update logic here
            helicopter.Update(gameTime, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);

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
            helicopter.Draw(spriteBatch);
            spriteBatch.DrawString(font, helicopter.GetPosition().ToString(), new Vector2(), Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
