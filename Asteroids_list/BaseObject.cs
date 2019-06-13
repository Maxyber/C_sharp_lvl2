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
        protected int BonusType; // тип бонуса, 1 - увеличение скорости атаки, 2 - увеличение скорости корабля, 3 - восполнение energy
        protected int Energy; // количество жизни каждого из объектов на поле, для пули оно равно 10, для всех остальных - от 25 и выше
        protected int Damage; // ПОКА НЕ ИСПОЛЬЗУЕТСЯ количество урона, который наносит каждый из объектов на поле при столкновении, для корабля 0, для всех остальных больше 0

        public delegate void Message();
        public BaseObject(int id, Point pos, Point dir, Size size, int energy)
        {
            ID = id;
            Pos = pos;
            Dir = dir;
            Size = size;
            Energy = energy;
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
        public int OEnergy
        {
            get { return Energy; }
            set { Energy = value; }
        }
        public Size OSize
        {
            get { return Size; }
            set { Size = value; }
        }
        public void EnergyLow(int damage)
        {
            Energy -= damage;
            if (Energy <= 0)
            {
                Game.WriteLog e = Game.LogEvent;
                e($"Energy of {ToString()} lower than zero");
            }
        }
        public abstract void Die();
        public virtual void Draw()
        {
            Game.Buffer.Graphics.DrawEllipse(Pens.White, Pos.X, Pos.Y, Size.Width, Size.Height);
        }
        public abstract void Update();
    }
}
