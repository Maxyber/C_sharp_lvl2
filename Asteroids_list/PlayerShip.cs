using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Asteroids
{
    class PlayerShip : BaseObject
    {
        private Image image;
        public PlayerShip(int id, Point pos) : base(id, pos, new Point(0,0), new Size(0, 0))
        {
            image = Image.FromFile($"Resources/ship1.png");
            OSize = new Size(image.Width, image.Height);
        }
        public override void Draw()
        {
            Game.Buffer.Graphics.DrawImage(image, Pos);
        }
    }
}
