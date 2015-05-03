using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace ProjectRPG
{
    public class Camera
    {
        private Matrix _transform;
        private float _zoom = 1.0f;
        private float _rotation;
        private Viewport _viewport;
        private Vector2 _center;

        public Matrix Transform
        {
            get { return _transform; }
        }

        public float Zoom
        {
            get { return _zoom; }
            set 
            {
                _zoom = value; 
                if (_zoom <= 0.1f) 
                    _zoom = 0.1f; 
            }
        }

        public float X
        {
            get { return _center.X; }
            set { _center.X = value; }
        }

        public float Y
        {
            get { return _center.Y; }
            set { _center.Y = value; }
        }
        

        public float Rotation
        {
            get { return _rotation; }
            set { _rotation = value; }
        }

        public Vector2 Position
        {
            get { return _center; }
            set { _center = value; }
        }

        public Camera(Viewport viewport)
        {
            _viewport = viewport;
        }

        public Camera(Viewport viewport, Player player)
        {
            _viewport = viewport;
            Update(player.Position);
        }

        public void Update(Vector2 position)
        {
            _center = new Vector2(position.X, position.Y);
            _transform = Matrix.CreateTranslation(new Vector3(-_center.X, -_center.Y, 0)) *
                Matrix.CreateRotationZ(_rotation) *
                Matrix.CreateScale(new Vector3(Zoom, Zoom, 1)) *
                Matrix.CreateTranslation(new Vector3(_viewport.Width / 2, _viewport.Height / 2, 0));
        }

        public void Update(Vector2 position, Vector2 playerPosition)
        {
            _center = new Vector2(position.X, position.Y);
            _transform = Matrix.CreateTranslation(new Vector3(-playerPosition.X, -playerPosition.Y, 0)) *
                Matrix.CreateTranslation(new Vector3(-_center.X, -_center.Y, 0)) *
                Matrix.CreateRotationZ(_rotation) *
                Matrix.CreateScale(new Vector3(Zoom, Zoom, 1)) *
                Matrix.CreateTranslation(new Vector3(_viewport.Width, _viewport.Height, 0));
        }
    }
}
