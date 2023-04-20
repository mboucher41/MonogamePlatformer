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
        private const int GameScale = 4;

        Texture2D playerTexture;
        Texture2D platformTexture;
        Actor player;       
        Collider platform;

        List<Collider> colliderList = new List<Collider>();

        Transform playerTransform;
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
            _graphics.PreferredBackBufferWidth = 360 * GameScale;
            _graphics.PreferredBackBufferHeight = 240 * GameScale;
            _graphics.ApplyChanges();
            playerTransform = new Transform(new Vector2(20 * GameScale, 20 * GameScale), 0, 1f);            
            platformTransform = new Transform(new Vector2(20 * GameScale, 200 * GameScale), 0, 1f);
            player = new Actor(this, playerTransform, playerTexture);
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
            // TODO: Add your update logic here
            foreach (Collider c in colliderList)
            {
                c.ProcessCollisions(player);
            }        

            //put inputs here?

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