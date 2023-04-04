using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace PlatformerGame
{
    public class Player : DrawableGameComponent
    {
        Rectangle rectangle;
        Transform transform;
        Texture2D texture;
        // Each child should overrode/make a new spritebatch.
        // Objects of the same class can share the spritebatch.
        public static SpriteBatch spriteBatch;
        enum JumpState
        {
            grounded,
            jumping,
            falling
        }
        JumpState CurrentPlayerJumpState = JumpState.grounded;
        int jumpTime = 0;

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
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
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

                    if (transform._position.Y > 680)
                    {
                        CurrentPlayerJumpState = JumpState.grounded;
                    }
                    break;
            }


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
    }
}
