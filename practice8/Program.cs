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

            #region Перевод из списка рёбер в массив вершин
            if (K > M)
                return "В данном графе нет такой клики"; //Мало вершин

            if (K * (K - 1) / 2 > N)
                return "В данном графе нет такой клики"; //Мало рёбер

            Node[] info = new Node[M];

            for (int k = 0, i = 0; i < N && k < M; i++)
            {
                bool check = true;

                string temp = graph[i].A;

                for (int j = 0; j < k; j++)
                    if (info[j] != null)
                        if (info[j].NodeName == temp)
                            check = false;
                   

                if (info[k] == null && check)
                {
                    info[k] = new Node(temp);
                    k++;
                }

                temp = graph[i].B;

                for (int j = 0; j < k; j++)
                    if (info[j] != null)
                        if (info[j].NodeName == temp)
                            check = false;

                if (k < M)
                    if (info[k] == null && check)
                    {
                        info[k] = new Node(temp);
                        k++;
                    }
            }

            M = 0;
            for (int i = 0; i < info.Length; i++)
                if (info[i] != null)
                    M++;

            Node[] buf = new Node[M];
            for (int i = 0; i < M; i++)
                buf[i] = info[i];

            info = buf;

            for (int i = 0; i < N; i++)
            {
                int a = -1, b = -1;

                for (int j = 0; j < M; j++)
                {
                    if (graph[i].A == graph[i].B)
                        continue;
                    if (info[j].NodeName == graph[i].A && info[j].NodeName != graph[i].B)
                        a = j;
                    if (info[j].NodeName == graph[i].B && info[j].NodeName != graph[i].A)
                        b = j;
                }

                if (a != -1 && b != -1)
                {
                    info[a].ConnectionsInfo[info[a].Connections] = info[b];
                    info[a].Connections++;

                    info[b].ConnectionsInfo[info[b].Connections] = info[a];
                    info[b].Connections++;
                }
            }


            #endregion

            int clickLenght = 0;
            string result = "";
            Click click = null;
            for (int i = 0; i < M && clickLenght < K; i++)
            {
                click = new Click(info[i]);
                info[i].Visited = -1;
                clickLenght = 1;
                Click mark = click;
                DFS(ref click, ref clickLenght, K);
            }
            for (int i = 0; i < K; i++)
            {
                if (click == null)
                    return "В данном графе нет такой клики";
                result = result + click.Info.ToString() + " ";
                click = click.Next;
            }
            return result;   
        }
        static public bool IsItPartOfClick(Node graph, Click click)
        {
            Click mark = click;
            

            while (mark != null)
            {
                int i = 0;

                while (i != graph.Connections && graph.ConnectionsInfo[i] != mark.Info)
                    i++;

                if (i != graph.Connections)
                    mark = mark.Next;
                else
                    return false;
            }

           
            return true;
        }
        static public void PushToClick(ref Click click, Node graph)
        {
            Click mark = new Click(graph);
            mark.Next = click;
            click = mark;
        }
        static public Node PopFromClick(Click click)
        {
            if (click != null)
            {
                Node result;
                result = click.Info;
                click = click.Next;
                return result;
            }
            else
                return null;
        }
        static public void DFS(ref Click click, ref int clickLength, int K, int deep = 0)
        {
            
            Click mark = click;
            deep++;

            int j = 0;
            while (j < mark.Info.Connections)
            {
                if (IsItPartOfClick(mark.Info.ConnectionsInfo[j], click) && mark.Info.ConnectionsInfo[j].Visited == 0)
                {
                    PushToClick(ref click, mark.Info.ConnectionsInfo[j]);
                    clickLength++;
                    mark.Info.ConnectionsInfo[j].Visited = deep;
                    mark = click;
                    DFS(ref click, ref clickLength, K, deep);
                }
                j++;
            }

            if (clickLength != K)
                {
                    Node temp = PopFromClick(click);
                    clickLength--;
                    for (int t = 0; t < temp.Connections; t++)
                        if (temp.ConnectionsInfo[t].Visited == deep)
                            temp.ConnectionsInfo[t].Visited = 0;
                }
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

            Console.WriteLine("Рёбра:");
            for (int i = 0; i < N; i++)
                Console.WriteLine("{0}  ", lines[i]);

            int K = 1;

            while (K != 0)
            {
                Console.WriteLine("Введите К - число вершин в клике. 0 - для завершения");
                K = int.Parse(Console.ReadLine());
                Console.WriteLine(ClickSearch(lines, K, N, M));
            }
        }
    }
}
