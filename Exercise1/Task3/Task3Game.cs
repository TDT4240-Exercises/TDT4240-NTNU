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
    public class Task3Game : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        private SpriteFont font;
        private List<GameObject> gameObjects;

        public Task3Game()
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

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            Random rand = new Random();

            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            Texture2D texture = Content.Load<Texture2D>("HelicopterSheet");
            gameObjects = new List<GameObject>();

            //Spawn some random helicopters
            for (int i = 0; i < 4; ++i)
            {
                gameObjects.Add(new GameObject(texture, i * 200.0f, i * 100.0f, rand.Next(-4, 4), rand.Next(-4, 4)));
            }

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
            foreach (GameObject entity in gameObjects) entity.Update(gameTime, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);

            //Collision detection (O(N^2) complexity)
            for (int i = 0; i < gameObjects.Count; ++i)
            {
                for (int j = i+1; j < gameObjects.Count; ++j)
                {
                    gameObjects[i].CheckCollision(gameObjects[j]);
                }
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
            foreach (GameObject entity in gameObjects) entity.Draw(spriteBatch);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
