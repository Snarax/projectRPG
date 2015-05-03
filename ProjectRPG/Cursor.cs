using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace ProjectRPG
{
    public class Cursor
    {
        private Texture2D _texture;
        private Vector2 _position;
        private Vector2 _positionGlobal;
        private MouseState _mouseState;
        private MouseState _mouseOldState;
        private Viewport _viewport;
        private Camera _camera;
        public int mouseWheel;

        public float X
        {
            get { return _position.X; }
            set { _position.X = value; }
        }

        public float Y
        {
            get { return _position.Y; }
            set { _position.Y = value; }
        }

        public Texture2D Texture
        {
            get { return _texture; }
            set { _texture = value; }
        }

        public Vector2 Position
        {
            get { return _position; }
            set { _position = value; }
        }

        public Vector2 PositionGlobal
        {
            get { return _positionGlobal; }
            set { _positionGlobal = Vector2.Transform(value, Matrix.Invert(_camera.Transform)); }
        }

        public Cursor(Viewport viewport, Camera camera)
        {
            _camera = camera;
            _viewport = viewport;
            _mouseOldState = Mouse.GetState();
        }

        public void Load(ContentManager Content)
        {
            Texture = Content.Load<Texture2D>(@"Images\cursor");
        }

        public void Update()
        {
            _mouseState = Mouse.GetState();

            // Опрос состояния мышки
            if (_mouseState.Position.X != _mouseOldState.X || _mouseState.Position.Y != _mouseOldState.Y)
            {
                X = _mouseState.Position.X;
                Y = _mouseState.Position.Y;
            }

            if (_mouseState.ScrollWheelValue - _mouseOldState.ScrollWheelValue == 0) mouseWheel = 0;
            else 
                if (_mouseState.ScrollWheelValue - _mouseOldState.ScrollWheelValue < 0) mouseWheel = -1;
                else mouseWheel = 1;

            _mouseOldState = _mouseState;
            PositionGlobal = _position;
        }

        public void Update(Vector2 playerPosition)
        {
            _mouseState = Mouse.GetState();

            // Опрос состояния мышки
            if (_mouseState.Position.X != _mouseOldState.X || _mouseState.Position.Y != _mouseOldState.Y)
            {
                X = _mouseState.Position.X;
                Y = _mouseState.Position.Y;
            }

            if (_mouseState.ScrollWheelValue - _mouseOldState.ScrollWheelValue == 0) mouseWheel = 0;
            else
                if (_mouseState.ScrollWheelValue - _mouseOldState.ScrollWheelValue < 0) mouseWheel = -1;
                else mouseWheel = 1;

            _mouseOldState = _mouseState;
            PositionGlobal = _position;

            if (_positionGlobal.X - X > _viewport.Width * 0.4f) _positionGlobal.X = _viewport.Width * 0.4f;
            else if (_positionGlobal.X - X < -_viewport.Width * 0.4f) _positionGlobal.X = -_viewport.Width * 0.4f;
            if (_positionGlobal.Y - Y > _viewport.Height * 0.4f) _positionGlobal.Y = _viewport.Height * 0.4f;
            else if (_positionGlobal.Y - Y < -_viewport.Height * 0.4f) _positionGlobal.Y = -_viewport.Height * 0.4f;
        }

        public void Draw(SpriteBatch spriteBatch)
        {

            // Рисуем курсор
            spriteBatch.Draw(Texture, Position, Color.White);
        }
    }
}
