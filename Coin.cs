using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using PlatformerGame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DMIT1514_Lab06_Platformer.Collider;

namespace DMIT1514_Lab06_Platformer
{
    public class Coin : GameObject
    {
        public static int coinCount = 0;
        public bool active = true;
        public static bool gameWon = false;
        public Coin(Game game, Transform transform, Texture2D texture2D) : base(game, transform, texture2D)
        {
            this.transform = transform;
            this.texture = texture2D;

            this.rectangle = this.texture.Bounds;
            this.rectangle = new Rectangle(rectangle.Location, new Point(rectangle.Width * (int)transform._scale, rectangle.Height * (int)transform._scale));
            active = true;
        }

        internal Rectangle BoundingBox
        {
            get
            {
                return new Rectangle((int)transform._position.X, (int)transform._position.Y, (int)texture.Width, (int)texture.Height);
            }
        }

        public void ProcessCollisions(Actor actor)
        {
            if (active == true) 
            {
                if (BoundingBox.Intersects(actor.rectangle))
                {
                    coinCount += 1;
                    active = false;
                    if (coinCount == 3)
                    {
                        gameWon = true;
                    }
                }
            }
        }

        public override void Draw(GameTime gameTime)
        {
            if (active == true)
            {
                spriteBatch.Begin(samplerState: SamplerState.PointClamp);
                spriteBatch.Draw(texture, transform._position, texture.Bounds, Color.Black, transform._rotation, texture.Bounds.Center.ToVector2(), transform._scale, SpriteEffects.None, 0);
                spriteBatch.End();
                base.Draw(gameTime);
            }
        }
    }
}
