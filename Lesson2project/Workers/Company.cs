using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace Workers
{
    class Company : IEnumerable
    {
        Worker_basic[] List;
        int Count;

        public Company(Worker_basic[] list, int count)
        {
            List = list;
            Count = count;
        }
        // Добавление работника в компанию
        public void Add(Worker_basic wb)
        {
            if (Count < List.Length)
            {
                List[Count] = wb;
                Count++;
            }
            else Console.WriteLine($"{Count} < {List.Length}");
        }
        // Метод использования для foreach
        public IEnumerator GetEnumerator()
        {
            for (int i = 0; i < this.List.Length; i++)
            {
                yield return this.List[i];
            }
        }
        // Метод сортировки компании по возрасту/зарплате
        public void Sort()
        {
            for (int i = 0; i < Count; i++)
            {
                bool flag = false;
                Worker_basic temp;
                for (int j = 0; j < Count - 1; j++)
                {
                    if (List[j].CompareTo(List[j + 1]) == 1)
                    {
                        temp = List[j];
                        List[j] = List[j + 1];
                        List[j + 1] = temp;
                        flag = true;
                    }
                }
                if (!flag) break;
            }
        }
    }
}
