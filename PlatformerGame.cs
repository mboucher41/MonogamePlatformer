using DMIT1514_Lab06_Platformer;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
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
        Texture2D groundTexture;
        Actor player;       
        GamePlatform platform;
        Collider ground;

        List<Collider> colliderList = new List<Collider>();
        List<GamePlatform> platformList = new List<GamePlatform>();

        Transform playerTransform;
        Transform platformTransform;
        Transform groundTransform;

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
            playerTransform = new Transform(new Vector2(360 * GameScale, 150 * GameScale), 0, 1f);//DOES NOT SEEM TO CHANGE     
            platformTransform = new Transform(new Vector2(150 * GameScale, 180 * GameScale), 0, 1f);
            groundTransform = new Transform(new Vector2(0, 240 * GameScale), 0, 1f);//COLLISION DOES NOT WORK PROPERLY
            player = new Actor(this, playerTransform, playerTexture);
            platform = new GamePlatform(new Vector2(platformTransform._position.X, platformTransform._position.Y), new Vector2(100,50), "Platform");
            ground = new Collider(new Vector2(groundTransform._position.X, groundTransform._position.Y), new Vector2(3000, 100), Collider.ColliderType.Top);

            platformList.Add(platform);
            colliderList.Add(ground);
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            playerTexture = Content.Load<Texture2D>("PlayerShip");
            platformTexture = Content.Load<Texture2D>("Platform");
            groundTexture = Content.Load<Texture2D>("Ground");
            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            // TODO: Add your update logic here
            //foreach (Collider c in colliderList)
            //{
            //    c.ProcessCollisions(player);
            //}        

            foreach (GamePlatform p in platformList)
            {
                p.topCollider.ProcessCollisions(player);
                p.bottomCollider.ProcessCollisions(player);
                p.leftCollider.ProcessCollisions(player);
                p.rightCollider.ProcessCollisions(player);
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            //foreach (GamePlatform platform in platformList)
            //{
            //    platform.Draw(_spriteBatch);
            //}
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}