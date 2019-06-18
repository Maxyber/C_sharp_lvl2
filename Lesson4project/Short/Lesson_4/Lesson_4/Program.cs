using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson_4
{
    class Program
    {
        public static Random r = new Random();
        static void Main(string[] args)
        {
            // Задание 2.2.
            List<string> sList = new List<string>();
            for (int i = 0; i < 100; i++)
            {
                sList.Add(RandomStringGenerate(3));
            }
            Dictionary<string, int> sDict = CreateDict(sList);
            PrintDict(sDict);
            Console.WriteLine("--- --- ---");
            Console.ReadLine();
            // Задание 2.3.
            MyQuery<string>(sList);
            Console.ReadLine();
        }
        // Метод генерации случайной строки из 3 случайных символов {'.', ':', '!') в количестве 3 элементов
        static string RandomStringGenerate(int count)
        {
            char[] chars = new char[3] { '.', ':', '!' };
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < count; i++) result.Append(chars[r.Next(3)]);
            return result.ToString();
        }
        // Метод подсчета и вывода вхождений элемента в обобщенную коллекцию
        public static Dictionary<T, int> CreateDict<T>(List<T> list)
        {
            Dictionary<T, int> result = new Dictionary<T, int>();
            foreach (T item in list)
            {
                if (result.ContainsKey(item)) result[item]++;
                else result.Add(item, 1);
            }
            return result;
        }
        public static void PrintDict<T>(Dictionary<T, int> dict)
        {
            foreach (KeyValuePair<T, int> pair in dict)
                Console.WriteLine($"элемент {pair.Key.ToString()} встречается {pair.Value} раз");
        }
        // Запрос Linq на вывод частотного массива обобщенной коллекции list
        public static void MyQuery<T>(List<T> list)
        {
            var query = (from n in list select n).GroupBy(n => n);
            foreach (var item in query)
                Console.WriteLine($"{item.Key} ... {item.Count().ToString()}");
        }
    }

}
