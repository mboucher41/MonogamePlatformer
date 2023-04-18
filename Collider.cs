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
            type = ColliderType.Top;
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
                        //if the player is moving rightwards
                        if (player.Velocity.X > 0)
                        {
                            player.Velocity.X = 0;
                            player.transform.MovePosition(player.Velocity);
                            //player.MoveHorizontally(0);
                        }
                        break;
                    case ColliderType.Right:
                        //if the player is moving leftwards
                        if (player.Velocity.X < 0)
                        {
                            player.Velocity.X = 0;
                            player.transform.MovePosition(player.Velocity);
                            //player.MoveHorizontally(0);
                        }
                        break;
                    case ColliderType.Top:
                        //player.CurrentPlayerJumpState = Player.JumpState.grounded;
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
