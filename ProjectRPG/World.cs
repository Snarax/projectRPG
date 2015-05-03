using System;
using System.Drawing;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace ProjectRPG
{
    public class World
    {
        public static int _width;
        public static int _height;
        public Tile[,] Map;
        Random random = new Random();
        private Texture2D _tileTexture;
        string _worldImagePath = @"c:\GameDev\Projects\ProjectRPG\ProjectRPG\Content\Images\World.bmp";

        public int Width
        {
            get { return _width; }
        }

        public int Height
        {
            get { return _height; }
        }

        public World()
        {
            Bitmap worldImage = new Bitmap(_worldImagePath, true);
            _width = worldImage.Width;
            _height = worldImage.Height;
            Map = new Tile[_width, _height];
            for (int x = 0; x < _width; x++)
            {
                for (int y = 0; y < _height; y++)
                {
                    System.Drawing.Color pixelColor = worldImage.GetPixel(x, y);
                    if (pixelColor == System.Drawing.Color.FromArgb(0, 0, 0)) Map[x, y] = new Tile(0);
                    if (pixelColor == System.Drawing.Color.FromArgb(0, 0, 255)) Map[x, y] = new Tile(1);
                    if (pixelColor == System.Drawing.Color.FromArgb(0, 255, 0)) Map[x, y] = new Tile(2);
                    if (pixelColor == System.Drawing.Color.FromArgb(255, 0, 0)) Map[x, y] = new Tile(3);
                }
            }
        }

        public World(int Width, int Height)
        {
            _width = Width;
            _height = Height;
            Map = new Tile[_width, _height];
            for (int x = 0; x < _width; x++)
            {
                for (int y = 0; y < _height; y++)
                {
                    Map[x, y] = new Tile((byte)random.Next(4));
                }
            }
        }

        public void Load(ContentManager Content)
        {
            _tileTexture = Content.Load<Texture2D>(@"Images\tileTexture");
        }

        public void Update()
        {
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    spriteBatch.Draw(_tileTexture, new Vector2(
                        x * TileEngine.TileWidth, y * TileEngine.TileHeight),
                        TileEngine.GetRectangleByTileID(Map[x, y].tileID),
                        Microsoft.Xna.Framework.Color.White,
                        0,
                        Vector2.Zero,
                        1,
                        SpriteEffects.None,
                        1);
                }
            }
        }
    }
}
