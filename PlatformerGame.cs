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

        Texture2D playerTexture;
        Texture2D platformTexture;
        Texture2D groundTexture;
        Texture2D coinTexture;
        Actor player;       
        GamePlatform platform1, platform2;
        GamePlatform ground;

        Coin coin1, coin2, coin3;

        List<Coin> coinList = new List<Coin>();
        List<Collider> colliderList = new List<Collider>();
        List<GamePlatform> platformList = new List<GamePlatform>();

        Transform playerTransform;
        Transform platformTransform;
        Transform groundTransform;
        Transform coinTransform1, coinTransform2, coinTransform3;

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
            _graphics.PreferredBackBufferWidth = 1000;
            _graphics.PreferredBackBufferHeight = 700;
            _graphics.ApplyChanges();
            playerTransform = new Transform(new Vector2(360, 150), 0, 1);//DOES NOT SEEM TO CHANGE     
            platformTransform = new Transform(new Vector2(150, 500), 0, 1);
            groundTransform = new Transform(new Vector2(0, Window.ClientBounds.Height), 0, 1);//COLLISION DOES NOT WORK PROPERLY

            coinTransform1 = new Transform(new Vector2(100, 100), 0, 1);

            player = new Actor(this, playerTransform, playerTexture);
            platform1 = new GamePlatform(new Vector2(platformTransform._position.X, platformTransform._position.Y), new Vector2(100,50), platformTexture);
            platform2 = new GamePlatform(new Vector2(Window.ClientBounds.Width / 2 , Window.ClientBounds.Height - 50), new Vector2(100, 50), platformTexture);
            ground = new GamePlatform(new Vector2(groundTransform._position.X, groundTransform._position.Y), new Vector2(3000, 100), platformTexture);
            coin1 = new Coin(this, coinTransform1, coinTexture);

            coinList.Add(coin1);
            platformList.Add(platform1);
            platformList.Add(platform2);
            platformList.Add(ground);
            //colliderList.Add(ground);
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            playerTexture = Content.Load<Texture2D>("PlayerShip");
            platformTexture = Content.Load<Texture2D>("Platform");
            groundTexture = Content.Load<Texture2D>("Ground");
            coinTexture = Content.Load<Texture2D>("Coin");
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
                p.leftCollider.ProcessCollisions(player);
                p.rightCollider.ProcessCollisions(player);
                p.topCollider.ProcessCollisions(player);
                p.bottomCollider.ProcessCollisions(player);
            }

            foreach (GamePlatform p in platformList)
            {
                p.leftCollider.ProcessCollisions(player);
                p.rightCollider.ProcessCollisions(player);
                p.topCollider.ProcessCollisions(player);
                p.bottomCollider.ProcessCollisions(player);
            }

            foreach (Coin c in coinList)
            {
                c.ProcessCollisions(player);
            }

            if (Coin.gameWon == true)
            {
                //WIN
            }
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            foreach (GamePlatform platform in platformList)
            {
                platform.Draw(_spriteBatch);
            }

            foreach (Coin c in coinList)
            {
                c.Draw(gameTime);
            }

            if (Coin.gameWon == true)
            {
                //WIN
            }
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}