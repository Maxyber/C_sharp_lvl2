using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson_4._3
{
    class Program
    {
        public delegate int _orderByValue(KeyValuePair<string, int> pair);
        static void Main(string[] args)
        {
            Dictionary<string, int> dict = NewDictCreate();
            #region Базовое задание
            var d = dict.OrderBy(delegate (KeyValuePair<string, int> pair) { return pair.Value; });
            foreach (var pair in d)
            {
                Console.WriteLine("{0} - {1}", pair.Key, pair.Value);
            }
            #endregion

            Console.WriteLine("--- --- ---");
            Console.ReadKey();
            dict = NewDictCreate();
            #region 4.3.1. свернуть OrderBy с помощью лямбда-выражений
            d = dict.OrderBy(pair => pair.Value);
            foreach (var pair in d)
            {
                Console.WriteLine("{0} - {1}", pair.Key, pair.Value);
            }

            #endregion

            Console.WriteLine("--- --- ---");
            Console.ReadKey();
            dict = NewDictCreate();
            #region 4.3.2. развернуть OrderBy с использованием делегата
            _orderByValue mydelegate = _delegateOrderByValue;
            d = dict.OrderBy(mydelegate.Invoke);
            foreach (var pair in d)
            {
                Console.WriteLine("{0} - {1}", pair.Key, pair.Value);
            }
            #endregion

            Console.WriteLine("--- --- ---");
            Console.ReadKey();
        }
        static Dictionary<string, int> NewDictCreate()
        {
            Dictionary<string, int>  dict = new Dictionary<string, int>()
            {
                {"four",4 },
                {"two",2 },
                { "one",1 },
                {"three",3 },
            };
            return dict;
        }
        static int _delegateOrderByValue(KeyValuePair<string, int> pair)
        {
            return pair.Value;
        }
    }
}
