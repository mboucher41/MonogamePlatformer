using DMIT1514_Lab06_Platformer;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace PlatformerGame
{
    public class PlatformerGame : Game
    {      
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        Texture2D playerTexture;
        Actor player;
        Transform playerTransform;

        Collider platform;

        List<Collider> colliderList = new List<Collider>();

        Texture2D platformTexture;
        Transform platformTransform;

        public PlatformerGame()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            base.Initialize();
            Window.Title = "Platformer Game";
            _graphics.PreferredBackBufferWidth = 1080;
            _graphics.PreferredBackBufferHeight = 720;
            _graphics.ApplyChanges();
            playerTransform = new Transform(new Vector2(400, 500), 0, 1f);
            player = new Actor(this, playerTransform, playerTexture);
            platformTransform = new Transform(new Vector2(400, 600), 0, 1f);
            platform = new Collider(this, platformTransform, platformTexture);
            colliderList.Add(platform);
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            playerTexture = Content.Load<Texture2D>("PlayerShip");
            platformTexture = Content.Load<Texture2D>("Platform");
            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            foreach (Collider c in colliderList)
            {
                c.ProcessCollisions(player);
            }

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}