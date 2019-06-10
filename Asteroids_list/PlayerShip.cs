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

        public PlayerShip(int id, Point pos) : base(id, pos, new Point(0, 0), new Size(0, 0))
        {
            image = Image.FromFile($"Resources/ship1.png");
            OSize = new Size(image.Width, image.Height);
        }
        public override void Draw()
        {
            Game.Buffer.Graphics.DrawImage(image, Pos);
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
    }
}
