﻿using System;
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

        public static event Message MessageDie;
        public PlayerShip(int id, Point pos, int energy) : base(id, pos, new Point(0, 0), new Size(0, 0), energy)
        {
            image = Image.FromFile($"Resources/ship1.png");
            OSize = new Size(image.Width, image.Height);
        }
        public override void Draw()
        {
            Game.Buffer.Graphics.DrawImage(image, Pos);
            Game.Buffer.Graphics.DrawRectangle(Pens.Yellow, Pos.X, Pos.Y + OSize.Height + 1, (OEnergy * OSize.Width) / Game.defaultShipEnergy, 1);
            // ниже идет отладочная строка, которая выводит ID объекта сверху от него
            //Game.Buffer.Graphics.DrawString($"{OEnergy}", new Font(FontFamily.GenericSansSerif, 10), new SolidBrush(Color.White), Pos.X, Pos.Y + Size.Height + 3);
            //Game.Buffer.Graphics.DrawString($"{GetID}", new Font(FontFamily.GenericSansSerif, 10), new SolidBrush(Color.White), Pos.X, Pos.Y - 12);
            //Game.Buffer.Graphics.DrawString($"{Pos.X},{Pos.Y}", new Font(FontFamily.GenericSansSerif, 10), new SolidBrush(Color.White), Pos.X, Pos.Y + Size.Height + 3);
        }
        public override void Update()
        {
            if ((Game.flagUp == true) && (Game.flagDown != true))
                ODir = new Point(ODir.X, -1 * Game.shipSpeed);
            if ((Game.flagDown == true) && (Game.flagUp != true))
                ODir = new Point(ODir.X, 1 * Game.shipSpeed);
            if ((Game.flagLeft == true) && (Game.flagRight != true))
                ODir = new Point(-1 * Game.shipSpeed, ODir.Y);
            if ((Game.flagLeft != true) && (Game.flagRight == true))
                ODir = new Point(1 * Game.shipSpeed, ODir.Y);
            if ((Game.flagUp == false) && (Game.flagDown == false))
                ODir = new Point(ODir.X, 0);
            if ((Game.flagLeft == false) && (Game.flagRight == false))
                ODir = new Point(0, ODir.Y);
            Pos.X = Pos.X + Dir.X;
            Pos.Y = Pos.Y + Dir.Y;
            // Тут объект при достижении края экрана продолжает движение с другого края, при этом, если объект движется по горизонтали, у него изменяется координата по Y
            if (Pos.X < 0 - Size.Width) Pos.X = Game.Width;
            if (Pos.X > Game.Width + Size.Width) Pos.X = 0 - Size.Width;
            if (Pos.Y < 0 - Size.Height) Pos.Y = Game.Height;
            if (Pos.Y > Game.Height + Size.Height) Pos.Y = 0 - Size.Height;
            //
        }
        public void ChangeSpeed(Point dir)
        {
            ODir = dir;
        }
        public override void Die()
        {
            MessageDie?.Invoke("Player died");
        }
    }
}
