﻿using Microsoft.Xna.Framework;
using MonoGameExtensions;

namespace PlatformerGame
{
    public struct Transform
    {
        public Vector2 _position;
        public float _rotation;
        public int _scale;

        public Vector2 Position => _position;
        public float Rotation => _rotation;
        public int Scale => _scale;
        public Point Location(Point referenceSize) => _position.Location(referenceSize);

        public Transform(Vector2 position, float rotation, int scale)
        {
            _position = position;
            _rotation = rotation;
            _scale = scale;
        }

        public void SetPosition(Vector2 newPosition)
        {
            _position = newPosition;
        }
        public void SetPosition(float x, float y)
        {
            _position = new Vector2(x, y);
        }
        public void SetPosition(Rectangle rect)
        {
            _position = rect.Center.ToVector2();
        }

        public Vector2 MovePosition(Vector2 posOffset)
        {
            SetPosition(_position + posOffset);
            return _position + posOffset;
        }
        public void SyncRect(Rectangle source)
        {
            _position.X = source.X;
            _position.Y = source.Y;
        }
    }
}
