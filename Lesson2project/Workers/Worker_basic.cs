using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workers
{
    abstract class Worker_basic : IComparable
    {
        protected string Name;
        protected int Age;
        protected double Salary;

        public Worker_basic(string name, int age, double salary)
        {
            Name = name;
            Age = age;
            Salary = salary;
        }

        public string GetName { get { return Name; } }
        public int GetAge { get { return Age; } }
        public double GetSalHour { get { return Salary; } }
        // Расчет зп для почасовых
        abstract public double CalcSalary();
        // Формирование строки для вывода
        abstract public override string ToString();
        // реализация интерфейса IComparable
        public int CompareTo(object obj)
        {
            Worker_basic worker = obj as Worker_basic;
            int result = 0;
            if (Age < worker.GetAge) result = -1;
            else if (Age > worker.GetAge) result = 1;
            else
            {
                if (Salary < worker.CalcSalary()) result = -1;
                else if (Salary > worker.CalcSalary()) result = 1;
                else result = 0;
            }
            return result;
        }

    }
}
