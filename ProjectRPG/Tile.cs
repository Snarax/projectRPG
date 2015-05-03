using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectRPG
{
    public class Tile
    {
        private byte _tileID;
        //private int _x;
        //private int _y;

        public Tile(byte tileID)
        {

            _tileID = tileID;
            //_x = X;
            //_y = Y;
        }

        public byte tileID
        {
            get { return _tileID; }
            set { _tileID = value; }
        }
    }
}
