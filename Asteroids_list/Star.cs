using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Asteroids
{
    class Star : BaseObject
    {
        public static event Message ObjectCreated;

        public Star(int id, Point pos, Point dir, Size size, int energy) : base(id, pos, dir, size, energy)
        {
            ObjectCreated?.Invoke($"New star created {id}, ({Pos.X},{Pos.Y}), speed: ({dir.X},{dir.Y})");
        }

        public override void Draw()
        {
            Game.Buffer.Graphics.DrawLine(Pens.White, Pos.X + Size.Width / 4, Pos.Y + Size.Height / 4, Pos.X + Size.Width * 3 / 4, Pos.Y + Size.Height * 3 / 4);
            Game.Buffer.Graphics.DrawLine(Pens.White, Pos.X + Size.Width * 3 / 4, Pos.Y + Size.Height / 4, Pos.X + Size.Width / 4, Pos.Y + Size.Height * 3 / 4);
            Game.Buffer.Graphics.DrawLine(Pens.White, Pos.X + Size.Width / 2, Pos.Y, Pos.X + Size.Width / 2, Pos.Y + Size.Height);
            Game.Buffer.Graphics.DrawLine(Pens.White, Pos.X, Pos.Y + Size.Height / 2, Pos.X + Size.Width, Pos.Y + Size.Height / 2);
            // Ниже идет отрисовка значения количества хп звезд
            // Game.Buffer.Graphics.DrawRectangle(Pens.Yellow, Pos.X, Pos.Y + Size.Height + 1, (OEnergy * Size.Width) / (Game.defaultStarEnergy * Game.level * 5 / 2), 1);
            // ниже идет отладочная строка, которая выводит ID объекта сверху от него
            //Game.Buffer.Graphics.DrawString($"{GetID}", new Font(FontFamily.GenericSansSerif, 10), new SolidBrush(Color.White), Pos.X, Pos.Y - 12);
            //Game.Buffer.Graphics.DrawString($"{Pos.X},{Pos.Y}", new Font(FontFamily.GenericSansSerif, 10), new SolidBrush(Color.White), Pos.X, Pos.Y + Size.Height + 3);
        }
        public override void Update()
        {
            bool flag = false;
            Pos.X = Pos.X + Dir.X;
            Pos.Y = Pos.Y + Dir.Y;
            /* Тут объект при достижении края экрана отражается от него и летит в другом направлении
            if (Pos.X < 0) Dir.X = -Dir.X;
            if (Pos.X > Game.Width) Dir.X = -Dir.X;
            if (Pos.Y < 0) Dir.Y = -Dir.Y;
            if (Pos.Y > Game.Height) Dir.Y = -Dir.Y;
            */
            /* Тут объект при достижении края экрана продолжает движение из центра
            if (((Pos.X < 0) || (Pos.X > Game.Width)) || ((Pos.Y < 0) || (Pos.Y > Game.Height)))
            {
                Pos.X = Game.Width / 2;
                Pos.Y = Game.Height / 2;
            }
            */
            // Тут объект при достижении края экрана продолжает движение с другого края, при этом, если объект движется по горизонтали, у него изменяется координата по Y
            if (Pos.X < 0 - Size.Width)
            {
                Pos.X = Game.Width + Size.Width;
                flag = true;
            }
            if (Pos.X > Game.Width + Size.Width)
            {
                Pos.X = 0 - Size.Width;
                flag = true;
            }
            if (Dir.Y != 0)
            {
                if (Pos.Y < 0 - Size.Height) Pos.Y = Game.Height + Size.Height;
                if (Pos.Y > Game.Height + Size.Height) Pos.Y = 0 - Size.Height;
            }
            else if (flag == true)
            {
                Pos.Y = Game.r.Next(Game.Height);
            }
            //
        }
        public override void Die()
        {
        }
    }
}
