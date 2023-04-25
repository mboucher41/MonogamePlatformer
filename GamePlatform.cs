using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using PlatformerGame;
using static DMIT1514_Lab06_Platformer.Collider;

namespace DMIT1514_Lab06_Platformer
{
    public class GamePlatform
    {
        public ColliderTop topCollider;
        public ColliderBottom bottomCollider;
        public ColliderLeft leftCollider;
        public ColliderRight rightCollider;

        public Texture2D texture;

        protected Vector2 position;
        protected Vector2 dimensions;

        public GamePlatform(Vector2 position, Vector2 dimensions, Texture2D texture)
        {
            this.texture = texture;
            this.position = position;
            this.dimensions = dimensions;
            topCollider = new ColliderTop(                                        
                                        new Vector2(position.X + 3, position.Y),
                                        new Vector2(dimensions.X - 6, 1),
                                        Collider.ColliderType.Top,
                                        texture);
            rightCollider = new ColliderRight(
                                        new Vector2(position.X + dimensions.X - 1, position.Y + 1),
                                        new Vector2(1, dimensions.Y - 2),
                                        Collider.ColliderType.Right,
                                        texture);
            bottomCollider = new ColliderBottom(
                                        new Vector2(position.X + 3, position.Y + dimensions.Y),
                                        new Vector2(dimensions.X - 6, 1),
                                        Collider.ColliderType.Bottom,
                                        texture);
            leftCollider = new ColliderLeft(
                                        new Vector2(position.X, position.Y + 1),
                                        new Vector2(1, dimensions.Y - 2),
                                        Collider.ColliderType.Left,
                                        texture);
        }

        internal void ProcessCollisions(Actor actor)
        {
            topCollider.ProcessCollisions(actor);
            rightCollider.ProcessCollisions(actor);
            bottomCollider.ProcessCollisions(actor);
            leftCollider.ProcessCollisions(actor);
        }

        internal void Draw(SpriteBatch spritebatch)
        {
            spritebatch.Draw(texture, position, Color.White);
            topCollider.Draw(spritebatch);
            rightCollider.Draw(spritebatch);
            bottomCollider.Draw(spritebatch);
            leftCollider.Draw(spritebatch);         
        }
    }
}
