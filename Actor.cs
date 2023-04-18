using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PlatformerGame;
using static PlatformerGame.Player;

namespace DMIT1514_Lab06_Platformer
{
    public class Actor : GameObject
    {
        internal Vector2 Velocity;
        public enum JumpState
        {
            grounded,
            jumping,
            falling
        }

        public JumpState CurrentPlayerJumpState = JumpState.grounded;
        int jumpTime = 0;

        public Actor(Game game, Transform transform, Texture2D texture) : base(game, transform, texture)
        {
            Velocity = new Vector2(0, 4);
            this.transform = base.transform;
            this.texture = base.texture;

            this.rectangle = this.texture.Bounds;
            //Velocity.Y += 1;
        }

        public override void Update(GameTime gameTime)
        {
            //Velocity.Y += 1;

            //Velocity.Y = 3;
            if (Keyboard.GetState().IsKeyDown(Keys.Right))//Maybe add hardcoded screen side limits?
            {
                Velocity.X = 3;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                Velocity.X = -3;
            }
            else
            {
                Velocity.X = 0;
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
                    Velocity.Y = -7;
                    jumpTime -= 1;
                    if (jumpTime == 0)
                    {
                        CurrentPlayerJumpState = JumpState.falling;
                    }

                    break;
                case JumpState.falling:
                    Velocity.Y += 0.4f;

                    if (transform._position.Y > Game.Window.ClientBounds.Height - 45)//Hardcoded ground limit
                    {
                        transform._position.Y = Game.Window.ClientBounds.Height - 80;
                        CurrentPlayerJumpState = JumpState.grounded;
                    }
                    break;
            }

            
            //transform._position += Velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;
            rectangle.Offset(Velocity);
            transform.MovePosition(Velocity);
            base.Update(gameTime);

// rectangle.Offset(Velocity);
//            transform.MovePosition(Velocity);
        }
        internal void Land(Rectangle landingRect)
        {
            transform.SetPosition(new Vector2(transform._position.X, landingRect.Top - rectangle.Height + 1));
            Velocity.Y = 0;
        }
        internal void StandOn(Rectangle standRect)
        {
            Velocity.Y -= 1;
        }

        public void SetVelocity(int x, int y)
        {
            Velocity = new Vector2(x, y);
        }
    }
}
