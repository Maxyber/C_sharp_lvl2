using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Asteroids
{
    class ShipBonus : BaseObject
    {
        private Image image;

        public ShipBonus(int id, Point pos, Point dir) : base(id, pos, dir, new Size(0, 0))
        {
            int type = Game.r.Next(100) + 1;
            if (type > 90) OBonType = 2;
            else OBonType = 1;
            image = Image.FromFile($"Resources/bonus{OBonType}.png");
            OSize = new Size(image.Width, image.Height);
        }
        public override void Draw()
        {
            Game.Buffer.Graphics.DrawImage(image, Pos);
        }
        public override void Update()
        {
            Pos.X = Pos.X + Dir.X;
            Pos.Y = Pos.Y + Dir.Y;
            if ((Pos.X < 0 - Size.Width) || (Pos.X > Game.Width + Size.Width))
                Dir = new Point(0, 0);
        }
    }
}
