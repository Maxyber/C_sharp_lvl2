using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workers
{
    class Workers_hour : Worker_basic
    {

        public Workers_hour(string name, int age, double salary) : base(name, age, salary)
        {
        }

        public override double CalcSalary()
        {
            return 20.8 * 8 * Salary;
        }
        public override string ToString()
        {
            return $"Работник: {GetName} с почасовой оплатой, возраст:     {GetAge}, зп: {CalcSalary():f2}";
        }
    }
}
