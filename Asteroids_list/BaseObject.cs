﻿using System;
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
        protected int ID;

        public BaseObject(int id, Point pos, Point dir, Size size)
        {
            ID = id;
            Pos = pos;
            Dir = dir;
            Size = size;
        }
        public int GetID
        {
            get { return ID; }
        }
        public Point GetPos
        {
            get { return Pos; }
        }
        public Point GetDir
        {
            get { return Dir; }
        }
        public Size OSize
        {
            get { return Size; }
            set { Size = value; }
        }
        public virtual void Draw()
        {
            Game.Buffer.Graphics.DrawEllipse(Pens.White, Pos.X, Pos.Y, Size.Width, Size.Height);
        }
        public virtual void Update()
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
            if (this is Bullet)
            {
                if ((Pos.X < 0 - Size.Width) || (Pos.X > Game.Width + Size.Width))
                    Dir = new Point(0, 0);
            }
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
    }
}
