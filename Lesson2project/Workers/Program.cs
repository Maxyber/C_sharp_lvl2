using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workers
{
    class Program
    {
        public static Random r;

        static void Main(string[] args)
        {
            r = new Random();
            int index = 0;
            int count = 5;
            Company list = new Company(new Worker_basic[10], 0);
            // Формируем список из 5 персон с почасовой оплатой
            for (int i = 0; i < count; i++)
            {
                list.Add(new Workers_hour($"Name_{index}", r.Next(45) + 20, r.Next(180) + 200));
                index++;
            }
            // Формируем список из 5 персон с фиксированной оплатой
            for (int i = 0; i < count; i++)
            {
                list.Add(new Workers_month($"Name_{index}", r.Next(45) + 20, r.Next(36000) + 20000));
                index++;
            }
            // Выводим в консоль список компании
            do
            {
                Console.WriteLine(list.Current.ToString());
            } while (list.MoveNext());
            // Сортируем список
            list.Sort();
            Console.WriteLine("Далее выводим отсортированный по возрасту массив с использованием интерфейса ISort");
            // Выводим на экран отсортированный список компании
            do
            {
                Console.WriteLine(list.Current.ToString());
            } while (list.MoveNext());
            Console.ReadKey();
        }
    }
}