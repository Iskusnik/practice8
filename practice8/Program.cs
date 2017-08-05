using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace practice8
{
    class Program
    {
     /// <summary>
     /// 27.	Граф задан списком вершин и ребер. 
     ///        Найти в нем какую-либо клику из K вершин
     /// Реализовать программу, решающую задачу, соответствующую Вашему варианту.
     /// Для тестирования программы разработать генератор тестов, который позволит сгенерировать набор входных данных, используемых при тестировании.
     ///    клика - подмножество, где любые две вершины подмножества соединены ребром 
     /// </summary>
     /// <param name="args"></param>
        static void Main(string[] args)
        {
            Console.WriteLine("Ввод количества вершин");
            int M = int.Parse(Console.ReadLine());
            
            int N;
            do
            {
                Console.WriteLine("Ввод количества рёбер");
                N = int.Parse(Console.ReadLine());
            } while (N > (M - 1) * M / 2 + M);

            Line[] lines = new Line[N];

            Console.WriteLine("0 - Ручной ввод");
            Console.WriteLine("Не 0 - Генерация");
            int choose = int.Parse(Console.ReadLine());

            if (choose == 0)
            {
                for (int i = 0; i < N; i++)
                {
                    Console.Clear();

                    Console.Write("Рёбра:");
                    for (int j = 0; j < i; j++)
                        Console.Write("{0}  ", lines[i]);
                    Console.WriteLine();

                    Console.WriteLine("Формат: AB");
                    Console.WriteLine("Ввод ребра {0}", i + 1);
                    string temp = Console.ReadLine();
                    lines[i] = new Line(temp[0].ToString(), temp[1].ToString());
                }
            }
            else
                lines = TestGen.gen(N, M);
        }
    }
}
