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

        public ShipBonus(int id, Point pos, Point dir) : base(id, pos, dir, new Size(0, 0), 0)
        {
            int type = Game.r.Next(100) + 1;
            if (type > 95) OBonType = 3;
            else if ((type > 87) && (type <= 95)) OBonType = 2;
            else OBonType = 1;
            image = Image.FromFile($"Resources/bonus{OBonType}.png");
            OSize = new Size(image.Width, image.Height);
        }
        public override void Draw()
        {
            Game.Buffer.Graphics.DrawImage(image, Pos);
            // ниже идет отладочная строка, которая выводит ID объекта сверху от него и координаты объекта снизу
            //Game.Buffer.Graphics.DrawString($"{GetID}", new Font(FontFamily.GenericSansSerif, 10), new SolidBrush(Color.White), Pos.X, Pos.Y - 12);
            //Game.Buffer.Graphics.DrawString($"{Pos.X},{Pos.Y}", new Font(FontFamily.GenericSansSerif, 10), new SolidBrush(Color.White), Pos.X, Pos.Y + Size.Height + 3);
        }
        public override void Update()
        {
            Pos.X = Pos.X + Dir.X;
            Pos.Y = Pos.Y + Dir.Y;
            if ((Pos.X < 0 - Size.Width) || (Pos.X > Game.Width + Size.Width))
                Dir = new Point(-100, -100);
        }
        public override void Die()
        {
        }
    }
}
