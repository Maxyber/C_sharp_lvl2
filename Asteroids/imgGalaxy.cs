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
        private Image image;
        public ImgGalaxy(Point pos, Point dir, Image image) : base(pos, dir, new Size(0,0))
        {
            this.image = image;
        }
        public override void Draw()
        {
            Game.Buffer.Graphics.DrawImage(image, Pos);
        }
    }
}
