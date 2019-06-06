using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Asteroids
{
    class ImgGalaxy : BaseObject
    {
        public ImgGalaxy(Point pos, Point dir) : base(pos, dir, new Size(0,0))
        {
        }
        public override void Draw()
        {
            Image image = Image.FromFile("galaxy1.jpg");
            Game.Buffer.Graphics.DrawImage(image, Pos);
        }
    }
}
