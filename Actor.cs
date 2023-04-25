using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PlatformerGame;

namespace DMIT1514_Lab06_Platformer
{
    public class Actor : GameObject
    {
        internal Vector2 Velocity;
        bool landed;
        public bool leftCollide;
        public bool rightCollide;
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

        public new void Update(GameTime gameTime)
        {
            //leftCollide = false;
            //rightCollide = false;
            rectangle.Location = transform._position.ToPoint();
            Velocity.Y += 0.4f;

            if (Keyboard.GetState().IsKeyDown(Keys.Right))//Maybe add hardcoded screen side limits?
            {
                if (leftCollide == false)
                {
                    Velocity.X = 6;
                }
                else
                {
                    Velocity.X = 0;
                }

                if (leftCollide == true)
                {
                    Velocity.X = 0;
                }
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                if (rightCollide == false)
                {
                    Velocity.X = -6;
                }
                else
                {
                    Velocity.X = 0;
                }

                if (rightCollide == true)
                {
                    Velocity.X = 0;
                }
            }
            else
            {
                Velocity.X = 0;
            }



            if (Keyboard.GetState().IsKeyDown(Keys.Space) && landed == true)
            {
                landed = false;
                CurrentPlayerJumpState = JumpState.jumping;
                jumpTime += 14;
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
                    Velocity.Y += 0.4f;
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
            transform._position.Y = landingRect.Top - rectangle.Y + 1;
            Velocity.Y = 0;
            transform.SyncRect(rectangle);
            landed = true;
            //CurrentPlayerJumpState = JumpState.grounded;
        }

        internal void StandOn(Rectangle standRect)
        {
            Velocity.Y -= 0.4f;
        }
        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin(samplerState: SamplerState.PointClamp);
            spriteBatch.Draw(texture, transform._position, texture.Bounds, Color.White, transform._rotation, texture.Bounds.Location.ToVector2(), transform._scale, SpriteEffects.None, 0);

            spriteBatch.End();
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
