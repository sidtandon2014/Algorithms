using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JourneyToTheMoon
{
    class Program
    {
        static Int64 TotalAstronautsPairPossible = 0;
        static int TotalAstronautsOfSameCountry = 0;
        static void Main(string[] args)
        {
            int totalAstronauts, totalPairs;
            string[] input = Console.ReadLine().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            Graph grph = new Graph();
            if (input.Length == 2 && int.TryParse(input[0], out totalAstronauts) && int.TryParse(input[1], out totalPairs))
            {
                if (totalPairs < 1 || totalPairs > 10000 || totalAstronauts < 1 || totalAstronauts > 100000)
                    return;
                for (int index = 0; index < totalAstronauts; index++)
                {
                    Vertex vrtx = new Vertex(index);
                    grph.addVertex(vrtx);
                }
                for (int index = 0; index < totalPairs; index++)
                {
                    string[] astronauts = Console.ReadLine().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    if (astronauts.Length == 2)
                    {
                        int vrtxA = Convert.ToInt32(astronauts[0]);
                        int vrtxB = Convert.ToInt32(astronauts[1]);
                        if (vrtxA < 0 || vrtxA > (totalAstronauts - 1) || vrtxB < 0 || vrtxB > (totalAstronauts - 1))
                            return;
                        Vertex v1 = grph.getVertex(vrtxA);
                        Vertex v2 = grph.getVertex(vrtxB);
                        grph.addNeighbours(v1, v2);
                    }
                }
                DFSLook(grph);
                Console.WriteLine("{0}", TotalAstronautsPairPossible);
                Console.ReadLine();
            }
        }

        public static void DFSLook(Graph g)
        {
            List<Vertex> allVertices = g.getAllVertices();
            List<Int64> SameCountryAstronautsNumber = new List<long>();
            int totalDFSCall = 0;
            long previousSum = 0;
            foreach (Vertex v in allVertices)
            {
                DFS(g, v);
                totalDFSCall += 1;
                if(totalDFSCall > 1)
                {
                    TotalAstronautsPairPossible += previousSum * TotalAstronautsOfSameCountry;
                }
                previousSum += TotalAstronautsOfSameCountry;
                TotalAstronautsOfSameCountry = 0;
            }
        }
        public static void DFS(Graph g, Vertex root)
        {
            if (!root.isVertexExplored())
            {
                TotalAstronautsOfSameCountry += 1;
                root.setVertexExplored();
                List<Vertex> neighbours = root.getNeighbours();
                foreach (Vertex v in neighbours)
                {
                    DFS(g, v);
                }
            }
        }
    }

    class Vertex
    {
        public int data;
        private List<Vertex> neighbours;
        private bool isTraversed;

        public Vertex(int data)
        {
            this.data = data;
            isTraversed = false;
            neighbours = new List<Vertex>();
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
