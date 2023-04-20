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
    public class GamePlatform : GameObject
    {
        //public Collider topCollider;
        //public Collider bottomCollider;
        public Collider leftCollider;
        //public Collider rightCollider;

        public GamePlatform(Game game, Transform transform, Texture2D texture) : base(game, transform, texture)
        {
            this.transform = transform;
            this.texture = texture;
            this.rectangle = this.texture.Bounds;

            //topCollider = new Collider(game, this.transform, this.texture, Collider.ColliderType.Top);
            //bottomCollider = new Collider(game, this.transform, this.texture, Collider.ColliderType.Bottom);
            leftCollider = new Collider(game, this.transform, this.texture, Collider.ColliderType.Left);
            //rightCollider = new Collider(game, this.transform, this.texture, Collider.ColliderType.Right);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
    }
}
