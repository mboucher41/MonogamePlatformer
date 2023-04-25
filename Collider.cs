using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using PlatformerGame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using System.Diagnostics;
using static DMIT1514_Lab06_Platformer.Collider;

namespace DMIT1514_Lab06_Platformer
{
    public class Collider
    {
        public enum ColliderType
        {
            Left, Right, Top, Bottom
        }
        public ColliderType type;

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

        public Collider(Vector2 position, Vector2 dimensions, ColliderType colliderType, Texture2D texture)
        {
            this.position = position;
            this.dimensions = dimensions;
            this.type = colliderType;
            this.texture = texture;
        }

        internal void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, BoundingBox, new Rectangle(0, 0, 1, 1), Color.White);
        }

        //public override void Update(GameTime gameTime)
        //{
        //    base.Update(gameTime);
        //}

        internal bool ProcessCollisions(Actor actor)
        {
            bool didCollide = false;
            if (BoundingBox.Intersects(actor.rectangle))
            {
                didCollide = true;
                switch (type)
                {
                    case ColliderType.Left:
                        //if the player is moving rightwards
                        if (actor.Velocity.X > 0)
                        {
                            actor.sideColliding = true;
                            actor.Velocity.X = 0;
                        }
                        actor.transform.MovePosition(actor.Velocity);
                        break;
                    case ColliderType.Right:
                        //if the player is moving leftwards
                        if (actor.Velocity.X < 0)
                        {
                            actor.sideColliding = true;
                            actor.Velocity.X = 0;
                        }
                        actor.transform.MovePosition(actor.Velocity);
                        break;
                    case ColliderType.Top:
                        //if the player is landing on top
                        actor.Land(BoundingBox);
                        actor.StandOn(BoundingBox);
                        break;
                    case ColliderType.Bottom:
                        //if the player hits the bottom
                        //if (actor.Velocity.Y < 0)
                        //{
                            actor.Velocity.Y = 8;
                            actor.transform.MovePosition(actor.Velocity);
                        //}                        
                        //actor.CurrentPlayerJumpState = Actor.JumpState.falling;
                        //actor.transform.MovePosition(actor.Velocity);                    
                        break;
                }
            }
            else
            {
                actor.sideColliding = false;
            }
            return didCollide;
        }
    }
}
