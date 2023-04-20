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
using System.Numerics;

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
            //set dimensions?
            switch (type)
            {
                case ColliderType.Top:
                    this.rectangle = new Rectangle(rectangle.Location, new Point(rectangle.Width * (int)transform._scale, 1 * (int)transform._scale));
                    break;
                case ColliderType.Bottom:
                    this.rectangle = new Rectangle(rectangle.Location + new Point(0, 50), new Point(rectangle.Width * (int)transform._scale, 1 * (int)transform._scale));//POSITION IS NOT UPDATING
                    break;
                case ColliderType.Left:
                    this.rectangle = new Rectangle(rectangle.Location, new Point(1 * (int)transform._scale, rectangle.Height * (int)transform._scale));
                    break;
                case ColliderType.Right:
                    //this.rectangle = new Rectangle(rectangle.Location + new Point(150, 0), new Point(1 * (int)transform._scale, rectangle.Height * (int)transform._scale));
                    break;
            }                   
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
                        //actor.Velocity.Y = 8;
                        actor.transform.MovePosition(actor.Velocity);
                        break;
                    case ColliderType.Right:
                        //if the player is moving leftwards
                        actor.sideColliding = true;
                        //actor.Velocity.Y = 8;
                        actor.transform.MovePosition(actor.Velocity);
                        break;
                    case ColliderType.Top:
                        //if the player is landing on top
                        actor.Land(rectangle);
                        actor.StandOn(rectangle);
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
