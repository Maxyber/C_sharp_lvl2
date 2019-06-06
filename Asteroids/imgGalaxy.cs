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
        public string path;
        public ImgGalaxy(Point pos, Point dir, string path) : base(pos, dir, new Size(0,0))
        {
            this.path = path;
        }
        public override void Draw()
        {
            Image image = Image.FromFile(path);
            Game.Buffer.Graphics.DrawImage(image, Pos);
        }
    }
}
