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
        Texture2D winTexture;
        Texture2D houseTexture;
        Actor player;       
        GamePlatform platform1, platform2, platform3, platform4, platform5, platform6;
        GamePlatform ground;

        Coin coin1, coin2, coin3, coin4, coin5, coin6;

        List<Coin> coinList = new List<Coin>();
        List<GamePlatform> platformList = new List<GamePlatform>();

        Transform playerTransform;
        Transform platformTransform1, platformTransform2, platformTransform3, platformTransform4, platformTransform5, platformTransform6;
        Transform groundTransform;
        Transform coinTransform1, coinTransform2, coinTransform3, coinTransform4, coinTransform5, coinTransform6;

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
            playerTransform = new Transform(new Vector2(425, 450), 0, 1);     

            groundTransform = new Transform(new Vector2(0, Window.ClientBounds.Height), 0, 1);
            ground = new GamePlatform(new Vector2(groundTransform._position.X, groundTransform._position.Y - 100), new Vector2(3000, 100), groundTexture);

            platformTransform1 = new Transform(new Vector2(150, 450), 0, 1);
            platformTransform2 = new Transform(new Vector2(0, 275), 0, 1);
            platformTransform3 = new Transform(new Vector2(700, 450), 0, 1);
            platformTransform4 = new Transform(new Vector2(Window.ClientBounds.Width - 150, 275), 0, 1);
            platformTransform5 = new Transform(new Vector2(250, 150), 0, 1);
            platformTransform6 = new Transform(new Vector2(550, 150), 0, 1);

            player = new Actor(this, playerTransform, playerTexture);
            platform1 = new GamePlatform(new Vector2(platformTransform1._position.X, platformTransform1._position.Y), new Vector2(100, 50), platformTexture);
            platform2 = new GamePlatform(new Vector2(platformTransform2._position.X, platformTransform2._position.Y), new Vector2(100, 50), platformTexture);
            platform3 = new GamePlatform(new Vector2(platformTransform3._position.X, platformTransform3._position.Y), new Vector2(100, 50), platformTexture);
            platform4 = new GamePlatform(new Vector2(platformTransform4._position.X, platformTransform4._position.Y), new Vector2(100, 50), platformTexture);
            platform5 = new GamePlatform(new Vector2(platformTransform5._position.X, platformTransform5._position.Y), new Vector2(100, 50), platformTexture);
            platform6 = new GamePlatform(new Vector2(platformTransform6._position.X, platformTransform6._position.Y), new Vector2(100, 50), platformTexture);

    
            coinTransform1 = new Transform(new Vector2(225, 400), 0, 1);
            coinTransform2 = new Transform(new Vector2(75, 225), 0, 1);
            coinTransform3 = new Transform(new Vector2(775, 400), 0, 1);
            coinTransform4 = new Transform(new Vector2(Window.ClientBounds.Width - 75, 225), 0, 1);
            coinTransform5 = new Transform(new Vector2(325, 100), 0, 1);
            coinTransform6 = new Transform(new Vector2(625, 100), 0, 1);

            coin1 = new Coin(this, coinTransform1, coinTexture);
            coin2 = new Coin(this, coinTransform2, coinTexture);
            coin3 = new Coin(this, coinTransform3, coinTexture);
            coin4 = new Coin(this, coinTransform4, coinTexture);
            coin5 = new Coin(this, coinTransform5, coinTexture);
            coin6 = new Coin(this, coinTransform6, coinTexture);

            coinList.Add(coin1);
            coinList.Add(coin2);
            coinList.Add(coin3);
            coinList.Add(coin4);
            coinList.Add(coin5);
            coinList.Add(coin6);

            platformList.Add(platform1);
            platformList.Add(platform2);
            platformList.Add(platform3);
            platformList.Add(platform4);
            platformList.Add(platform5);
            platformList.Add(platform6);
            platformList.Add(ground);
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            playerTexture = Content.Load<Texture2D>("PlayerShip");
            platformTexture = Content.Load<Texture2D>("Platform");
            groundTexture = Content.Load<Texture2D>("Ground");
            coinTexture = Content.Load<Texture2D>("Coin");
            winTexture = Content.Load<Texture2D>("Dub");
            houseTexture = Content.Load<Texture2D>("House");
            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (Coin.gameWon == false)
            {
                if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                    Exit();
                // TODO: Add your update logic here       
                player.leftCollide = false;
                player.rightCollide = false;//these still don't reset properly for some reason

                player.Update(gameTime);


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



                base.Update(gameTime);
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            // TODO: Add your drawing code here
            base.Draw(gameTime);

            _spriteBatch.Begin();
            _spriteBatch.Draw(houseTexture, new Vector2(425, 500), Color.White);
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
                _spriteBatch.Draw(winTexture, new Vector2(200, 100), Color.White);
            }
            _spriteBatch.End();       
        }
    }
}