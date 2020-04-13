using System;
using System.Collections.Generic;
using System.Linq;

namespace EvenTree
{
    class Program
    {
        static void Main(string[] args)
        {
            int totalVertices, totalEdges;
            string[] input = Console.ReadLine().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            Graph grph = new Graph();
            if (input.Length == 2 && int.TryParse(input[0], out totalVertices) && int.TryParse(input[1], out totalEdges))
            {
                if (totalVertices < 2 || totalVertices > 100)
                    return;
                for (int index = 0; index < totalEdges; index++)
                {
                    string[] edge = Console.ReadLine().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    if (edge.Length == 2)
                    {
                        int vrtxA, vrtxB;
                        vrtxA = Convert.ToInt32(edge[0]);
                        vrtxB = Convert.ToInt32(edge[1]);

                        Vertex vertexA = grph.getVertex(vrtxA);
                        Vertex vertexB = grph.getVertex(vrtxB);

                        grph.addNeighbours(vertexA, vertexB);
                    }
                }
                List<Vertex> vertices = grph.getAllVertices();
                List<Vertex> verticesToBeRemoved;
                int disjointGraphs = 0;
                foreach (Vertex vrtx in vertices)
                {
                    verticesToBeRemoved = new List<Vertex>();
                    foreach (Vertex neighbourVertex in vrtx.getNeighbours())
                    {
                        if (getVertexCount(vrtx, neighbourVertex) % 2 == 0)
                        {
                            verticesToBeRemoved.Add(neighbourVertex);
                            neighbourVertex.removeNeighbour(vrtx);
                            disjointGraphs++;
                        }
                    }
                    foreach(Vertex removeVertex in verticesToBeRemoved)
                    {
                        vrtx.removeNeighbour(removeVertex);
                    }
                    //Console.WriteLine("Key :{0}, Edges:{1}", vertices[index].data, vertices[index].printNeighbours());

                }
                Console.WriteLine(disjointGraphs);
                Console.ReadLine();
            }
        }

        static int getVertexCount(Vertex exclusionVertex, Vertex startingVertex)
        {
            int count = 1, totalNeighbours;
            List<Vertex> neighbours = startingVertex.getNeighbours();
            totalNeighbours = neighbours.Count;
            if (totalNeighbours > 1)
            {
                foreach (Vertex vrtx in neighbours)
                {
                    if (vrtx.data != exclusionVertex.data)
                    {
                        count += getVertexCount(startingVertex, vrtx);
                    }
                }
                //count+=getVertexCount()
            }
            return count;
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

        private void addVertex(Vertex vertex)
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
