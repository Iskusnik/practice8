using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace practice8
{
    //Генерация N рёбер при M вершин
    //Рёбра разные
    //Ребро AA - возможно

    class TestGen
    {
        static Line[] graph = new Line[10001];
        static Line[] lines;
        static Random random = new Random();
        static public Line newLine(int M)
        {
            Line line;
            do
                line = new Line(char.ConvertFromUtf32(random.Next(0, M + 1) + 'A'), char.ConvertFromUtf32(random.Next(0, M + 1) + 'A'));
            while (graph[line.GetHashCode() % 10001] != null); //Надо убрать генерацию двух одинаковых рёбер

            graph[line.GetHashCode() % 10001] = line;
            return line;
        }
        static public Line[] gen(int N, int M)
        {
            lines = new Line[N];
            for (int i = 0; i < N; i++)
                lines[i] = newLine(M);
            return lines;
        }
    }
}
