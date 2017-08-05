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

        static public string ClickSearch(Line[] graph, int K, int N, int M)
        {

            string click = "";

            if (K > M)
                return "В данном графе нет такой клики"; //Мало вершин

            if (K * (K - 1) / 2 > N)
                return "В данном графе нет такой клики"; //Мало рёбер
            
            Node[] info = new Node[M];
            
            for (int k = 0, i = 0; i < N; i++)
            {
                bool check = true;
                string temp = graph[i].A;

                for (int j = 0; j < k; j++)
                    if (info[j] != null)
                    {
                        if (info[j].NodeName == graph[i].A)
                            temp = graph[i].B;
                        else
                            if (info[j].NodeName == graph[i].B)
                            check = false;
                    }

                if (info[k] == null && check)
                {
                    info[k] = new Node(temp);
                    k++;
                }
            }
            
            for (int i = 0; i < N; i++)
            {
                int a = 0, b = 0;

                for (int j = 0; j < M; j++)
                {
                    if (info[j].NodeName == graph[i].A && info[j].NodeName != graph[i].B)
                        a = j;
                    if (info[j].NodeName == graph[i].B && info[j].NodeName != graph[i].A)
                        b = j;
                }

                info[a].ConnectionsInfo[info[a].Connections] = info[b];
                info[a].Connections++;

                info[b].ConnectionsInfo[info[b].Connections] = info[a];
                info[b].Connections++;
            }

            /*
            
            for (int i = 0; i < M; i++) 
            {
                click = info[i].NodeName.ToString();
                int temp = 1;
                while (temp != K)
                {
                    info[i].
                }
            }
            */

            return null;
        }

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


            int K = 1;

            while (K != 0)
            {
                Console.WriteLine("Введите К - число вершин в клике. 0 - для завершения");
                K = int.Parse(Console.ReadLine());
                ClickSearch(lines, K, N, M);
            }
        }
    }
}
