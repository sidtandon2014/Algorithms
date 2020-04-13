using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BFS
{
    class Program
    {

        static void Main(string[] args)
        {
            int testCases;
            string sTestCase = Console.ReadLine();
            if (int.TryParse(sTestCase, out testCases) && testCases <= 10 && testCases >= 1)
            {
                for (int testCaseIterator = 0; testCaseIterator < testCases; testCaseIterator++)
                {
                    int totalEdges, totalVertices;
                    string[] input = Console.ReadLine().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    Graph grph = new Graph();
                    if (input.Length == 2 && int.TryParse(input[0], out totalVertices) && int.TryParse(input[1], out totalEdges))
                    {
                        if (totalVertices > 1000 || totalVertices < 2 || totalEdges < 1 || totalEdges > (totalVertices * (totalVertices - 1) / 2))
                            return;
                        List<Vertex> queue = new List<Vertex>();
                        for (int index = 1; index <= totalVertices; index++)
                        {
                            Vertex vertex = new Vertex(index);
                            grph.addVertex(vertex);
                        }
                        for (int index = 0; index < totalEdges; index++)
                        {
                            string[] edgePair = Console.ReadLine().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                            if (edgePair.Length == 2)
                            {
                                int vrtxA = Convert.ToInt32(edgePair[0]);
                                int vrtxB = Convert.ToInt32(edgePair[1]);
                                if (vrtxA > totalVertices || vrtxA < 1 || vrtxB > totalVertices || vrtxB < 1)
                                    return;
                                Vertex v1 = grph.getVertex(vrtxA);
                                Vertex v2 = grph.getVertex(vrtxB);
                                grph.addNeighbours(v1, v2);
                            }
                        }
                        int startingPos = Convert.ToInt32(Console.ReadLine());
                        if (startingPos > totalVertices || startingPos < 1)
                            return;
                        Vertex startingVertex = grph.getVertex(startingPos);
                        startingVertex.level = 0;
                        startingVertex.distance = 0;
                        queue.Add(startingVertex);
                        BFS(grph, queue);
                        for (int index = 1; index <= totalVertices; index++)
                        {
                            if (index != startingPos)
                                Console.Write(grph.getVertex(index).distance + " ");
                        }
                        //Console.WriteLine("{0}", TotalAstronautsPairPossible);
                    }
                    Console.WriteLine();
                }

            }
        }

        static void BFS(Graph g, List<Vertex> queue)
        {
            int index = 0;
            while (queue.Count > index)
            {
                List<Vertex> neighbours = queue[index].getNeighbours();
                queue[index].setVertexExplored();
                foreach (Vertex vrtx in neighbours)
                {
                    if (!vrtx.isVertexExplored())
                    {
                        queue.Add(vrtx);
                        if (vrtx.level == -1)
                        {
                            vrtx.level = queue[index].level + 1;
                            vrtx.distance = vrtx.level * 6;
                        }
                    }
                }
                index++;
            }
        }
    }
    class Vertex
    {
        public int data;
        private List<Vertex> neighbours;
        private bool isTraversed;
        public int distance;
        public int level;
        public Vertex(int data)
        {
            this.data = data;
            isTraversed = false;
            neighbours = new List<Vertex>();
            this.distance = -1;
            this.level = -1;
        }
        public void addNeighbour(Vertex vertexB)
        {
            if (!neighbours.Any(f => f.data == vertexB.data))
            {
                this.neighbours.Add(vertexB);
            }
        }

        public void removeNeighbour(Vertex vertexB)
        {
            int totalNeighbours = neighbours.Count;
            for (int index = 0; index < totalNeighbours; index++)
            {
                if (neighbours[index].data == vertexB.data)
                {
                    neighbours.RemoveAt(index);
                    break;
                }
            }
        }
        public List<Vertex> getNeighbours()
        {
            return neighbours;
        }

        public string printNeighbours()
        {
            string neighboursList = string.Empty;
            foreach (Vertex tmpVertex in this.neighbours)
            {
                neighboursList += tmpVertex.data.ToString() + ",";
            }
            return neighboursList;
        }

        public bool isVertexExplored()
        {
            return isTraversed;
        }

        public void setVertexExplored()
        {
            isTraversed = true;
        }
    }

    class Graph
    {
        private List<Vertex> vertexList;
        public Graph()
        {
            if (vertexList == null)
            {
                vertexList = new List<Vertex>();
            }
        }

        public void addVertex(Vertex vertex)
        {
            this.vertexList.Add(vertex);
        }

        public bool isVertexExists(Vertex vertex)
        {
            foreach (Vertex tmpVertex in vertexList)
            {
                if (tmpVertex.data == vertex.data)
                    return true;
            }
            return false;
        }

        public Vertex getVertex(int key)
        {
            foreach (Vertex tmpVertex in vertexList)
            {
                if (tmpVertex.data == key)
                    return tmpVertex;
            }
            Vertex vertex = new Vertex(key);
            this.addVertex(vertex);
            return vertex;
        }
        public void addNeighbours(Vertex from, Vertex to)
        {
            from.addNeighbour(to);
            to.addNeighbour(from);
        }

        public List<Vertex> getAllVertices()
        {
            return vertexList;
        }
        //public List<Vertex>
    }
}
