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
        public Bullet(int id, Point pos, Point dir, Size size) : base(id, pos, dir, size)
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
        }
    }
}
