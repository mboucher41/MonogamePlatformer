using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace PlatformerGame
{
    public class GameObject: DrawableGameComponent
    {
        Game game;
        public Rectangle rectangle;
        public Transform transform;
        public Texture2D texture;

        // Each child should overrode/make a new spritebatch.
        // Objects of the same class can share the spritebatch.
        public static SpriteBatch spriteBatch;

        public GameObject(Game game, Transform transform, Texture2D texture2D) : base(game)
        {
            if (spriteBatch is null)
            {
                spriteBatch = spriteBatch = new SpriteBatch(GraphicsDevice);
            }
            // Add more to the constructor.
             // This allows the game to call Update and Draw automatically.
            this.transform = transform;
            this.texture = texture2D;

            rectangle = texture.Bounds;
            //this.rectangle = new Rectangle(rectangle.Location, new Point(rectangle.Width * (int)transform._scale, rectangle.Height * (int)transform._scale));
            game.Components.Add(this);
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
