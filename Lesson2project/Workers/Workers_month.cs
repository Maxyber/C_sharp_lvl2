using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workers
{
    class Workers_month : Worker_basic
    {

        public Workers_month(string name, int age, double salary) : base(name, age, salary)
        {
        }

        public override double CalcSalary()
        {
            return Salary;
        }
        public override string ToString()
        {
            return $"Работник: {GetName} с фиксированной оплатой, возраст: {GetAge}, зп: {CalcSalary():f2}";
        }
    }
}
