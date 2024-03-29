﻿using System;
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
        public static event Message ObjectCreated;

        public ImgGalaxy(int id, Point pos, Point dir, int energy) : base(id, pos, dir, new Size(0, 0), energy)
        {
            image = Image.FromFile($"Resources/galaxy{Game.r.Next(4) + 1}.jpg");
            OSize = new Size(image.Width, image.Height);
            ObjectCreated?.Invoke($"New galaxy created {id}, ({Pos.X},{Pos.Y}), speed: ({dir.X},{dir.Y})");
    }
        public override void Draw()
        {
            Game.Buffer.Graphics.DrawImage(image, Pos);
            Game.Buffer.Graphics.DrawRectangle(Pens.Yellow, Pos.X, Pos.Y + OSize.Height + 1, (OEnergy * OSize.Width) / (Game.defaultGalaxyEnergy * Game.level * 5 / 2), 1);
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
