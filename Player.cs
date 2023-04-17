﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace PlatformerGame
{
    public class Player : DrawableGameComponent
    {
        public Transform transform;
        Texture2D texture;
        // Each child should overrode/make a new spritebatch.
        // Objects of the same class can share the spritebatch.
        public static SpriteBatch spriteBatch;
        public Vector2 Velocity;

        protected Vector2 dimensions;
        protected Vector2 position;

        public enum JumpState
        {
            grounded,
            jumping,
            falling
        }

        public JumpState CurrentPlayerJumpState = JumpState.grounded;
        int jumpTime = 0;

        internal Rectangle BoundingBox
        {
            get
            {
                return new Rectangle((int)position.X, (int)position.Y, (int)dimensions.X, (int)dimensions.Y);
            }
        }


        protected void Initialze()
        {
        }

        public Player(Game game, Transform transform, Texture2D texture) : base(game)
        {
            if (spriteBatch is null)
            {
                spriteBatch = spriteBatch = new SpriteBatch(GraphicsDevice);
            }
            // Add more to the constructor.
            game.Components.Add(this); // This allows the game to call Update and Draw automatically.
            this.transform = transform;
            this.texture = texture;
        }

        public void Start(Vector2 startPosition)
        {
            // Use this to "reset" your game object at a position. Add more if needed.
            transform._position = startPosition;
            Enabled = true;
            Visible = true;
        }

        // This will be run by the game automatically if "Enabled" is true.
        public override void Update(GameTime gameTime)
        {
            // After intializing your transform, use transform.MovePosition() to move.

            if (Keyboard.GetState().IsKeyDown(Keys.Right))//Maybe add hardcoded screen side limits?
            {
                Velocity.X = 3;
                transform.MovePosition(Velocity);
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                Velocity.X = -3;
                transform.MovePosition(Velocity);
            }
            else
            {
                Velocity.X = 0;
                transform.MovePosition(Velocity);
            }

            switch (CurrentPlayerJumpState)
            {
                case JumpState.grounded:
                    Velocity.Y = 0;
                    jumpTime = 0;
                    if (Keyboard.GetState().IsKeyDown(Keys.Space))
                    {
                        CurrentPlayerJumpState = JumpState.jumping;
                        jumpTime += 30;
                    }
                    break;
                case JumpState.jumping:
                    Velocity.Y = -4;
                    transform.MovePosition(Velocity);
                    jumpTime -= 1;
                    if (jumpTime == 0)
                    {
                        CurrentPlayerJumpState = JumpState.falling;
                    }

                    break;
                case JumpState.falling:
                    Velocity.Y = 4;
                    transform.MovePosition(Velocity);

                    if (transform._position.Y > Game.Window.ClientBounds.Height - 45)//Hardcoded ground limit
                    {
                        CurrentPlayerJumpState = JumpState.grounded;
                    }
                    break;
            }
            position += Velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;
            base.Update(gameTime);
        }

        // This will be run by the game automatically if "Visible" is true;
        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin(samplerState: SamplerState.PointClamp);
            spriteBatch.Draw(texture, transform._position, texture.Bounds, Color.White, transform._rotation, texture.Bounds.Center.ToVector2(), transform._scale, SpriteEffects.None, 0);
            spriteBatch.End();
            base.Draw(gameTime);
        }

        internal void Land(Rectangle whatILandedOn)
        {
            if (CurrentPlayerJumpState == JumpState.jumping)
            {
                position.Y = whatILandedOn.Top - dimensions.Y + 1;
                Velocity.Y = 0;
                CurrentPlayerJumpState = JumpState.grounded;
            }
        }
        internal void StandOn(Rectangle whatImStandingOn)
        {
            //velocity.Y -= PlatformerGame.Gravity;
        }
    }
}
