using Microsoft.Xna.Framework;

namespace ProjectRPG
{
    class TileEngine
    {
        static int _tileWidth = 64;
        static int _tileHeight = 64;
        
        public static Rectangle GetRectangleByTileID(byte tileID)
        {
            return new Rectangle(tileID * _tileWidth, 0, _tileWidth, _tileHeight);
        }

        public static int TileWidth
        {
            get { return _tileWidth; }
            set { _tileWidth = value; }
        }

        public static int TileHeight
        {
            get { return _tileHeight; }
            set { _tileHeight = value; }
        }

    }
}
