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

namespace DMIT1514_Lab06_Platformer
{
    public class Collider: GameObject
    {
        public enum ColliderType
        {
            Left, Right, Top, Bottom
        }
        public ColliderType type;

        public Collider(Game game, Transform transform, Texture2D texture, ColliderType providedType) : base(game, transform, texture)
        {
            this.transform = base.transform;
            this.texture = base.texture;
            this.rectangle = this.texture.Bounds;
            type = providedType;
            this.rectangle.Location = this.transform._position.ToPoint();
            this.rectangle = new Rectangle(rectangle.Location, new Point(rectangle.Width * (int)transform._scale, rectangle.Height * (int)transform._scale));      
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        internal bool ProcessCollisions(Actor actor)
        {
            bool didCollide = false;
            if (rectangle.Intersects(actor.rectangle))
            {
                didCollide = true;
                switch (type)
                {
                    case ColliderType.Left:
                        //if the player is moving rightwards                        
                        actor.sideColliding = true;
                        break;
                    case ColliderType.Right:
                        //if the player is moving leftwards
                        actor.sideColliding = true;
                        actor.transform.MovePosition(actor.Velocity);
                        break;
                    case ColliderType.Top:
                        //if the player is landing on top
                        actor.Land(rectangle);
                        actor.StandOn(rectangle);
                        break;
                    case ColliderType.Bottom:
                        //if the player hits the bottom
                        actor.Velocity.Y = 8;
                        actor.transform.MovePosition(actor.Velocity);
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
