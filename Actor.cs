using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PlatformerGame;

namespace DMIT1514_Lab06_Platformer
{
    public class Actor : GameObject
    {
        Rectangle rectangle;
        Transform transform;
        Texture2D texture;
        public Vector2 Velocity;
        enum JumpState
        {
            grounded,
            jumping,
            falling
        }
        JumpState CurrentPlayerJumpState = JumpState.grounded;
        int jumpTime = 0;

        public Actor(Game game, Transform transform, Texture2D texture) : base(game, transform, texture)
        {
            Velocity = new Vector2(0, 1);
            this.texture = texture;
            this.transform = transform;
            this.rectangle = this.texture.Bounds;
        }

        public override void Update(GameTime gameTime)
        {

            if (Keyboard.GetState().IsKeyDown(Keys.Right))//Maybe add hardcoded screen side limits?
            {
                transform.MovePosition(new Vector2(3, 0));
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                transform.MovePosition(new Vector2(-3, 0));
            }

            switch (CurrentPlayerJumpState)
            {
                case JumpState.grounded:
                    jumpTime = 0;
                    if (Keyboard.GetState().IsKeyDown(Keys.Space))
                    {
                        CurrentPlayerJumpState = JumpState.jumping;
                        jumpTime += 30;
                    }
                    break;
                case JumpState.jumping:

                    transform.MovePosition(new Vector2(0, -8));
                    jumpTime -= 1;
                    if (jumpTime == 0)
                    {
                        CurrentPlayerJumpState = JumpState.falling;
                    }

                    break;
                case JumpState.falling:

                    transform.MovePosition(new Vector2(0, 8));

                    if (transform._position.Y > Game.Window.ClientBounds.Height - 45)//Hardcoded ground limit
                    {
                        CurrentPlayerJumpState = JumpState.grounded;
                    }
                    break;
            }
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            //base.Draw(gameTime);

            spriteBatch.Begin(samplerState: SamplerState.PointClamp);
            spriteBatch.Draw(texture, transform._position, texture.Bounds, Color.White, transform._rotation, texture.Bounds.Center.ToVector2(), transform._scale, SpriteEffects.None, 0);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
