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
            // Задание 2.1.
            List<int> list = new List<int>();
            ListCreate(ref list, 10);
            Console.WriteLine("--- выводим на экран сформированную коллекцию целых чисел от 0 до 10");
            ListPrint(list);
            Console.WriteLine("--- далее выводим на экран словарь с количествами повторений для элементов коллекции");
            Dictionary<int, int> dict = CreateDictionary(list);
            PrintDictionary(dict);
            //Console.WriteLine("--- далее выводим на экран Linq запрос всех чисел коллекции больших, чем 2");
            //MyQuery(list, 2);
            Console.WriteLine("--- конец задание 2.1");
            Console.ReadLine();
            Console.Clear();
            // Задание 2.2.
            List<string> sList = new List<string>();
            for (int i = 0; i < 100; i++)
            {
                sList.Add(RandomStringGenerate(3));
            }
            Dictionary<string, int> sDict = CreateDict(sList);
            PrintDict(sDict);
            Console.WriteLine("--- конец программы");
            Console.ReadLine();
            Console.Clear();
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
        // Создаем и выводим коллекцию целых чисел из count элементов
        public static void ListCreate(ref List<int> list, int count)
        {
            Random r = new Random();
            for (int i = 0; i < count; i++)
            {
                list.Add(r.Next(10));
            }
            list.Sort();
        }
        public static void ListPrint(List<int> list)
        {
            int i = 0;
            foreach (var item in list)
            {
                Console.WriteLine($"{i + 1}. {item.ToString()}");
                i++;
            }
        }
        // Создаем и выводим словарь повторений каждого уникального элемента в коллекции list
        public static Dictionary<int, int> CreateDictionary(List<int> list)
        {
            Dictionary<int, int> dict = new Dictionary<int, int>();
            foreach (var item in list)
            {
                if (dict.ContainsKey(item)) dict[item]++;
                else dict.Add(item, 1);
            }
            return dict;
        }
        public static void PrintDictionary(Dictionary<int, int> dict)
        {
            foreach (KeyValuePair<int, int> item in dict)
            {
                Console.WriteLine($"Значение {item.Key} содержится в массиве {item.Value} раз");
            }
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
