using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMIT1514_Lab06_Platformer
{
    public class ColliderRight : Collider
    {
        public ColliderRight(Vector2 position, Vector2 dimensions, ColliderType colliderType, Texture2D texture) : base(position, dimensions, colliderType, texture)
        {
        }

        internal bool ProcessCollisions(Actor actor)
        {
            bool didCollide = false;
            if (BoundingBox.Intersects(actor.rectangle))
            {
                didCollide = true;
                if (actor.Velocity.X < 0)
                {
                    //actor.sideColliding = true;
                    actor.Velocity = Vector2.Zero;
                }
            }
            return didCollide;
        }
    }
}
