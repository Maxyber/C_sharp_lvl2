using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Asteroids
{
    class Bullet : BaseObject
    {
        // private Image image; Будет необходимо, когда выстрел будет графическим изображением, а не просто отрезком
        public Bullet(int id, Point pos, Point dir, Size size) : base(id, pos, dir, size, 10)
        {
            /* Будет необходимо, когда выстрел будет графическим изображением, а не просто отрезком
            image = Image.FromFile($"Resources/galaxy{Game.r.Next(4) + 1}.jpg");
            OSize = new Size(image.Width, image.Height);
            */
        }

        public override void Draw()
        {
            Game.Buffer.Graphics.DrawRectangle(Pens.OrangeRed, Pos.X, Pos.Y, Size.Width, Size.Height);
            // Game.Buffer.Graphics.DrawImage(image, Pos); Будет необходимо, когда выстрел будет графическим изображением, а не просто отрезком
            // ниже идет отладочная строка, которая выводит ID объекта сверху от него
            //Game.Buffer.Graphics.DrawString($"{GetID}", new Font(FontFamily.GenericSansSerif, 10), new SolidBrush(Color.White), Pos.X, Pos.Y - 12);
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
