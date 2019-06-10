using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Asteroids
{
    abstract class BaseObject
    {
        protected Point Pos;
        protected Point Dir;
        protected Size Size;
        protected int ID;
        protected int BonusType; // тип бонуса, 1 - увеличение скорости атаки, 2 - увеличение скорости корабля
        protected int HP; // ПОКА НЕ ИСПОЛЬЗУЕТСЯ количество жизни каждого из объектов на поле, для пули оно равно 0, для всех остальных отличное от нуля
        protected int Damage; // ПОКА НЕ ИСПОЛЬЗУЕТСЯ количество урона, который наносит каждый из объектов на поле при столкновении, для корабля 0, для всех остальных больше 0

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
        public int OBonType {
            get { return BonusType; }
            set { BonusType = value; }
        }
        public Point GetPos
        {
            get { return Pos; }
        }
        public Point ODir
        {
            get { return Dir; }
            set { Dir = value; }
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
        public abstract void Update();
    }
}
