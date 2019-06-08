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
        public ImgGalaxy(int id, Point pos, Point dir) : base(id, pos, dir, new Size(0,0))
        {
            image = Image.FromFile($"Resources/galaxy{Game.r.Next(4) + 1}.jpg");
            OSize = new Size(image.Width, image.Height);
        }
        public override void Draw()
        {
            Game.Buffer.Graphics.DrawImage(image, Pos);
        }
    }
}
