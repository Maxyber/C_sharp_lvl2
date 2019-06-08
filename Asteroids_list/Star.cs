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
        public Star(int id, Point pos, Point dir, Size size) : base(id, pos,dir,size)
        {
        }

        public override void Draw()
        {
            Game.Buffer.Graphics.DrawLine(Pens.White, Pos.X + Size.Width/4, Pos.Y + Size.Height/4, Pos.X + Size.Width*3/4, Pos.Y + Size.Height*3/4);
            Game.Buffer.Graphics.DrawLine(Pens.White, Pos.X + Size.Width*3/4, Pos.Y + Size.Height/4, Pos.X + Size.Width/4, Pos.Y + Size.Height*3/4);
            Game.Buffer.Graphics.DrawLine(Pens.White, Pos.X + Size.Width/2, Pos.Y, Pos.X + Size.Width/2, Pos.Y + Size.Height);
            Game.Buffer.Graphics.DrawLine(Pens.White, Pos.X, Pos.Y + Size.Height/2, Pos.X + Size.Width, Pos.Y + Size.Height/2);
        }
        /*
        public override void Update()
        {
            Pos.X = Pos.X - Dir.X;
            if (Pos.X < (0-Size.Width)) Pos.X = Game.Width + Size.Width;
            if (Pos.X > (Game.Width + Size.Width)) Pos.X = 0 - Size.Width;
        }
        */
    }
}
