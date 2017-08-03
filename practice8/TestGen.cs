using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace practice8
{
    //Генерация N рёбер
    //Рёбра разные
    //Ребро AA - возможно

    class TestGen
    {
        static Line[] graph = new Line[10001];

        static Random random = new Random();
        static public Line newLine(int N)
        {
            Line line;
            do
                line = new Line(char.ConvertFromUtf32(random.Next(0, N) + 'A'), char.ConvertFromUtf32(random.Next(0, N) + 'A'));
            while (graph[line.GetHashCode() % 10001] != null); //Надо убрать генерацию двух одинаковых рёбер

            graph[line.GetHashCode() % 10001] = line;
            return line;
        }
    }
}
