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
    public class Collider: GameObject
    {
        public enum ColliderType
        {
            Left, Right, Top, Bottom
        }
        protected ColliderType type;

        public Collider(Game game, Transform transform, Texture2D texture) : base(game, transform, texture)
        {
            this.transform = base.transform;
            this.texture = base.texture;
            this.rectangle = this.texture.Bounds;
            this.rectangle.Location = this.transform._position.ToPoint();
            this.rectangle = new Rectangle(rectangle.Location, new Point(rectangle.Width * (int)transform._scale, rectangle.Height * (int)transform._scale));
            type = ColliderType.Top;
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
                        if (actor.Velocity.X > 0)
                        {
                            actor.Velocity.X = 0;
                            actor.transform.MovePosition(actor.Velocity);
                            //player.MoveHorizontally(0);
                        }
                        break;
                    case ColliderType.Right:
                        //if the player is moving leftwards
                        if (actor.Velocity.X < 0)
                        {
                            actor.Velocity.X = 0;
                            actor.transform.MovePosition(actor.Velocity);
                            //player.MoveHorizontally(0);
                        }
                        break;
                    case ColliderType.Top:
                        //player.CurrentPlayerJumpState = Player.JumpState.grounded;
                        actor.Land(rectangle);
                        actor.StandOn(rectangle);
                        break;
                    case ColliderType.Bottom:
                        break;
                }
            }
            return didCollide;
        }
    }
}
