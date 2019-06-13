using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Asteroids
{
    class BackgroundStar
    {
        Point Pos;
        Size Size;
        Color StarColor;
        public BackgroundStar(Point pos, Size size, Color color)
        {
            Pos = pos;
            Size = size;
            StarColor = color;
        }
        public Point Position
        {
            get { return Pos; }
            set { Pos = value; }
        }
        public Size GetSize
        {
            get { return Size; }
        }
        public Color GetColor
        {
            get { return StarColor; }
        }
    }
}
