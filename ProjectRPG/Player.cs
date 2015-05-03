using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using System;

namespace ProjectRPG
{
    public class Player
    {
        private Vector2 _position;
        private float _rotation;
        private Texture2D _texture;
        private float _speed = 2.0f;

        public Vector2 Position
        {
            get { return _position; }
            
            // Получение позиции с проверкой выхода за пределы игрового мира
            set 
            { 
                _position = value;
                if (_position.X < 0) _position.X = 0;
                if (_position.Y < 0) _position.Y = 0;
                if (_position.X > World._width * TileEngine.TileWidth) _position.X = World._width * TileEngine.TileWidth;
                if (_position.Y > World._height * TileEngine.TileHeight) _position.Y = World._height * TileEngine.TileHeight;
            }
        }

        public Vector2 Move(int x, int y)
        {
            return new Vector2(x * _speed, y * _speed);
        }

        public float Rotation
        {
            get { return _rotation; }
            set { _rotation = value; }
        }

        public Texture2D Texture
        {
            get { return _texture; }
            set { _texture = value; }
        }

        public Player()
        {

            Position = new Vector2(0, 0);
        }

        public void Load(ContentManager Content)
        {
            Texture = Content.Load<Texture2D>(@"Images\player");
        }


        public void Update()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.W))
                Position += Move(0, -1);
            else if (Keyboard.GetState().IsKeyDown(Keys.S))
                Position += Move(0, 1);
            if (Keyboard.GetState().IsKeyDown(Keys.A))
                Position += Move(-1, 0);
            else if (Keyboard.GetState().IsKeyDown(Keys.D))
                Position += Move(1, 0);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, null, Color.White, 1.57f + Rotation, new Vector2(Texture.Width * 0.5f, Texture.Height * 0.5f), 1, SpriteEffects.None, 0);
            
        }
    }
}
