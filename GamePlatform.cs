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
        public Collider topCollider;
        public Collider bottomCollider;
        public Collider leftCollider;
        public Collider rightCollider;

        protected Texture2D texture;
        protected string textureName;
        protected Vector2 position;
        protected Vector2 dimensions;

        public GamePlatform(Vector2 position, Vector2 dimensions, string textureName)
        {
            topCollider = new Collider(                                        
                                        new Vector2(position.X + 3, position.Y),
                                        new Vector2(dimensions.X - 6, 1),
                                        Collider.ColliderType.Top);
            rightCollider = new Collider(
                                        new Vector2(position.X + dimensions.X - 1, position.Y + 1),
                                        new Vector2(1, dimensions.Y - 2),
                                        Collider.ColliderType.Right);
            bottomCollider = new Collider(
                                        new Vector2(position.X + 3, position.Y + dimensions.Y),
                                        new Vector2(dimensions.X - 6, 1),
                                        Collider.ColliderType.Bottom);
            leftCollider = new Collider(
                                        new Vector2(position.X, position.Y + 1),
                                        new Vector2(1, dimensions.Y - 2),
                                        Collider.ColliderType.Left);
            this.textureName = textureName;
        }

        internal void LoadContent(ContentManager Content)
        {
            texture = Content.Load<Texture2D>(textureName);
        }

        internal void ProcessCollisions(Actor actor)
        {
            topCollider.ProcessCollisions(actor);
            rightCollider.ProcessCollisions(actor);
            bottomCollider.ProcessCollisions(actor);
            leftCollider.ProcessCollisions(actor);
        }

        //internal void Draw(SpriteBatch spriteBatch)
        //{
        //    spriteBatch.Draw(texture, position, Color.White);
        //}
    }
}
