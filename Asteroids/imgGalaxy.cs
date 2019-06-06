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
        public Image image;
        public ImgGalaxy(Point pos, Point dir, string path) : base(pos, dir, new Size(0,0))
        {
            image = Image.FromFile(path);
        }
        public override void Draw()
        {
            Game.Buffer.Graphics.DrawImage(image, Pos);
        }
    }
}
