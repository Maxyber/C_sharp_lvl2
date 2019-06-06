using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Asteroids
{
    class BaseObject
    {
        protected Point Pos;
        protected Point Dir;
        protected Size Size;

        public BaseObject(Point pos, Point dir, Size size)
        {
            Pos = pos;
            Dir = dir;
            Size = size;
        }
        public virtual void Draw()
        {
            Game.Buffer.Graphics.DrawEllipse(Pens.White, Pos.X, Pos.Y, Size.Width, Size.Height);
        }
        public virtual void Update()
        {
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
            // Тут объект при достижении края экрана продолжает движение с другого края
            if (Pos.X < 0 - Size.Width) Pos.X = Game.Width + Size.Width;
            if (Pos.X > Game.Width + Size.Width) Pos.X = 0 - Size.Width;
            if (Pos.Y < 0 - Size.Height) Pos.Y = Game.Height + Size.Height;
            if (Pos.Y > Game.Height + Size.Height) Pos.Y = 0 - Size.Height;
            //
        }
    }
}
