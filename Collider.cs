using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using PlatformerGame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace DMIT1514_Lab06_Platformer
{
    public class Collider
    {
        public enum ColliderType
        {
            Left, Right, Top, Bottom
        }
        protected ColliderType colliderType;
        protected Texture2D texture;
        protected Vector2 position;
        protected Vector2 dimensions;
        internal Rectangle BoundingBox
        {
            get
            {
                return new Rectangle((int)position.X, (int)position.Y, (int)dimensions.X, (int)dimensions.Y);
            }
        }
        public Collider(Vector2 position, Vector2 dimensions, ColliderType colliderType)
        {
            this.position = position;
            this.dimensions = dimensions;
            this.colliderType = colliderType;
        }
        internal void LoadContent(ContentManager Content)
        {
            string textureString = "Platform" + colliderType.ToString();
            texture = Content.Load<Texture2D>(textureString);
        }
        internal void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, BoundingBox, new Rectangle(0, 0, 1, 1), Color.White);
        }
        internal bool ProcessCollisions(Player player)
        {
            bool didCollide = false;
            if (BoundingBox.Intersects(player.BoundingBox))
            {
                didCollide = true;
                switch (colliderType)
                {
                    case ColliderType.Left:
                        //if the player is moving rightwards
                        if (player.Velocity.X > 0)
                        {
                            player.transform.MovePosition(new Vector2(-1, 0));
                            //player.MoveHorizontally(0);
                        }
                        break;
                    case ColliderType.Right:
                        //if the player is moving leftwards
                        if (player.Velocity.X < 0)
                        {
                            player.transform.MovePosition(new Vector2(1, 0));
                            //player.MoveHorizontally(0);
                        }
                        break;
                    case ColliderType.Top:
                        //player.CurrentPlayerJumpState = Player.JumpState.grounded;
                        player.Land(BoundingBox);
                        //player.StandOn(BoundingBox);
                        break;
                    case ColliderType.Bottom:
                        break;
                }
            }
            return didCollide;
        }
    }
}
