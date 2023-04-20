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
        bool landed;
        public enum JumpState
        {
            grounded,
            jumping,
            falling
        }

        public JumpState CurrentPlayerJumpState = JumpState.falling;
        int jumpTime = 0;

        public Actor(Game game, Transform transform, Texture2D texture) : base(game, transform, texture)
        {
            Velocity = new Vector2(0, 0);
            this.transform = base.transform;
            this.texture = base.texture;

            this.rectangle = this.texture.Bounds;
            this.rectangle = new Rectangle(rectangle.Location, new Point(rectangle.Width * (int)transform._scale, rectangle.Height * (int)transform._scale));
            Velocity.Y += 1;
        }

        public override void Update(GameTime gameTime)
        {
            Velocity.Y += 0.5f;

            if (Keyboard.GetState().IsKeyDown(Keys.Right))//Maybe add hardcoded screen side limits?
            {
                Velocity.X = 6;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                Velocity.X = -6;
            }
            else
            {
                Velocity.X = 0;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Space) && landed == true)
            {
                landed = false;
                CurrentPlayerJumpState = JumpState.jumping;
                jumpTime += 10;
            }

            switch (CurrentPlayerJumpState)
            {
                case JumpState.grounded:
                    Velocity.Y = 0;
                    jumpTime = 0;
                    landed = true;
                    break;
                case JumpState.jumping:
                    Velocity.Y = -13;
                    jumpTime -= 1;
                    if (jumpTime == 0)
                    {
                        CurrentPlayerJumpState = JumpState.falling;
                    }

                    break;
                case JumpState.falling:
                    if (transform._position.Y > Game.Window.ClientBounds.Height - 60)//Hardcoded ground limit
                    {
                        transform._position.Y = Game.Window.ClientBounds.Height - 60;
                        CurrentPlayerJumpState = JumpState.grounded;                       
                    }
                    break;
            }
          
            rectangle.Offset(Velocity);
            transform.SyncRect(rectangle);
            base.Update(gameTime);
        }
        internal void Land(Rectangle landingRect)
        {
            transform.SetPosition(new Vector2(transform._position.X, landingRect.Top - rectangle.Height + 1));
            Velocity.Y = 0;
            transform.SyncRect(rectangle);
            landed = true;
        }

        internal void StandOn(Rectangle standRect)
        {
            Velocity.Y -= 1;
        }

        public void SetVelocity(int x, int y)
        {
            Velocity = new Vector2(x, y);
        }

        public void AddVelocity(int x, int y)
        {
            Velocity.X += x;
            Velocity.Y += y;
        }
    }
}
