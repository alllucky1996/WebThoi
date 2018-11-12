using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace One
{
    class Program
    {
        static void Main(string[] args)
        {
            int a = 0;
            Console.WriteLine("Hello World!");
            List<int> list = new List<int>();
            
            while (a!=42)
            {
                string temp = Console.ReadLine();
                a = int.Parse(temp);
                list.Add(a);
                
            }
            for (int i = 0; i < list.Count-1; i++)
            {
                Console.WriteLine(list[i]);
            }
            Console.ReadKey();
        }
    }
}
