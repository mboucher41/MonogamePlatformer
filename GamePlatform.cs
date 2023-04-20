using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PlatformerGame;

namespace DMIT1514_Lab06_Platformer
{
    public class GamePlatform : Collider
    {
        public enum ColliderType
        {
            Left, Right, Top, Bottom
        }
        ColliderType type;

        public GamePlatform(Game game, Transform transform, Texture2D texture) : base(game, transform, texture)
        {
            this.transform = transform;
            this.texture = texture;
            this.rectangle = this.texture.Bounds;
            type = ColliderType.Top;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        internal bool ProcessCollisions(Actor player)
        {
            bool didCollide = false;

            if (rectangle.Intersects(player.rectangle))
            {
                didCollide = true;
                switch (type)
                {
                    case ColliderType.Left:
                        break;
                    case ColliderType.Right:
                        break;
                    case ColliderType.Top:
                        player.Land(rectangle);
                        player.StandOn(rectangle);
                        break;
                    case ColliderType.Bottom:
                        break;
                }
            }
            return didCollide;
        }
    }
}
