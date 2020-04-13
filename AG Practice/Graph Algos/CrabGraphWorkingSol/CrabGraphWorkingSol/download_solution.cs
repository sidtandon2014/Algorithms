using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

public class Solution
{
    static void Main(string[] args)
    {
        int C = ReadNextInt();
        for (int c = 0; c < C; c++)
        {
            int N = ReadNextInt();
            T = ReadNextInt();
            int M = ReadNextInt();

            Graph g = new Graph(N);

            for (int m = 0; m < M; m++)
            {
                g.AddEdge(ReadNextInt() - 1, ReadNextInt() - 1);
            }

            Console.WriteLine(g.Transform().FindMaxFlow());
        }
    }

    private static int T;

    private class Graph
    {
        private readonly int size;
        private readonly List<Arc>[] edges;

        private struct Arc
        {
            public readonly int node;
            public readonly int capacity;

            public Arc(int n, int c)
            {
                this.node = n;
                this.capacity = c;
            }

            public override string ToString()
            {
                if (this.capacity == int.MaxValue)
                {
                    return string.Format("n:{0} *", this.node, this.capacity);
                }
                else
                {
                    return string.Format("n:{0} {1}", this.node, this.capacity);
                }
            }
        }

        public Graph(int size)
        {
            this.size = size;
            this.edges = new List<Arc>[size];
            for (int i = 0; i < size; i++)
            {
                this.edges[i] = new List<Arc>();
            }
        }

        public void AddEdge(int x, int y)
        {
            this.edges[x].Add(new Arc(y, 1));
            this.edges[y].Add(new Arc(x, 1));
        }

        public void AddArc(int x, int y, int capacity)
        {
            this.edges[x].Add(new Arc(y, capacity));
        }

        public int start { get { return this.size - 2; } }
        public int end { get { return this.size - 1; } }

        public Graph Transform()
        {
            Graph result = new Graph(this.size * 2 + 2);

            int start = result.start;
            int end = result.end;

            for (int i = 0; i < this.size; i++)
            {
                int ii = i * 2;

                result.AddArc(start, ii, T);
                result.AddArc(ii + 1, end, 1);

                foreach (var other in this.edges[i])
                {
                    result.AddArc(ii, other.node * 2 + 1, Int32.MaxValue);
                }
            }

            return result;
        }

        private struct Cell
        {
            public int capacity;
            public int flow;
            public int residualCapacity { get { return capacity - flow; } }
        }

        public int FindMaxFlow()
        {
            int start = this.start;
            int end = this.end;

            Cell[,] table = new Cell[this.size, this.size];
            for (int i = 0; i < this.size; i++)
            {
                foreach (var e in this.edges[i])
                {
                    table[i, e.node].capacity = e.capacity;
                }
            }

            return PushRelabel(table, start, end);
        }

        private void Push(Cell[,] table, int[] excess, int u, int v)
        {
            int rc = table[u, v].residualCapacity;
            int send = excess[u] > rc ? rc : excess[u];
            table[u, v].flow += send;
            table[v, u].flow -= send;
            excess[u] -= send;
            excess[v] += send;
        }

        private void Relabel(Cell[,] table, int[] heights, int u)
        {
            int min = int.MaxValue;
            for (int v = 0; v < heights.Length; v++)
            {
                if (table[u, v].residualCapacity > 0)
                {
                    min = min < heights[v] ? min : heights[v];
                    heights[u] = min + 1;
                }
            }
        }

        private void Discharge(Cell[,] table, int[] excess, int[] heights, int[] seen, int u)
        {
            while (excess[u] > 0)
            {
                if (seen[u] < seen.Length)
                {
                    int v = seen[u];
                    if ((table[u, v].residualCapacity > 0) && (heights[u] > heights[v]))
                    {
                        Push(table, excess, u, v);
                    }
                    else
                    {
                        seen[u] += 1;
                    }
                }
                else
                {
                    Relabel(table, heights, u);
                    seen[u] = 0;
                }
            }
        }

        private void MoveToFront(int i, int[] a)
        {
            int temp = a[i];
            for (int n = i; n > 0; n--)
            {
                a[n] = a[n - 1];
            }
            a[0] = temp;
        }

        private int PushRelabel(Cell[,] table, int start, int end)
        {
            int[] excess = new int[this.size];
            int[] heights = new int[this.size];
            int[] seen = new int[this.size];
            int[] list = new int[this.size - 2];

            for (int i = 0, p = 0; i < this.size; i++)
            {
                if ((i != start) && (i != end))
                {
                    list[p] = i;
                    p++;
                }
            }
 
            heights[start] = this.size;
            excess[start] = int.MaxValue;

            for (int i = 0; i < this.size; i++)
            {
                Push(table, excess, start, i);
            }
 
            int pp = 0;
            while (pp < (this.size - 2))
            {
                int u = list[pp];
                int old_height = heights[u];
                Discharge(table, excess, heights, seen, u);
                if (heights[u] > old_height)
                {
                    MoveToFront(pp, list);
                    pp = 0;
                }
                else
                {
                    pp += 1;
                }
            }

            int result = 0;
            for (int i = 0; i < this.size; i++)
            {
                result += table[start, i].flow;
            }
            return result;
        }
    }

    private static int ReadNextInt()
    {
        char c = (char)Console.Read();
        while (!char.IsDigit(c) && c != '-')
        {
            c = (char)Console.Read();
        }

        bool minus = (c == '-');
        if (minus)
        {
            c = (char)Console.Read();
        }

        int value = 0;
        do
        {
            value *= 10;
            value += c - '0';
        }
        while (char.IsDigit(c = (char)Console.Read()));

        return minus ? -value : value;
    }
}
