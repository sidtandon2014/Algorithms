using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kruskal
{
    class Program
    {
        static void Main(string[] args)
        {
            Graph grph = new Graph();
            int edges = 14, vertices = 9;
            for (int index = 1; index < vertices; index++)
            {
                grph.getVertex(index);
            }
            for (int index = 1; index <= edges; index++)
            {
                string[] input = Console.ReadLine().Split(new string[] { " " },StringSplitOptions.RemoveEmptyEntries);
                Vertex v1 = grph.getVertex(Convert.ToInt16(input[0]));
                Vertex v2 = grph.getVertex(Convert.ToInt16(input[1]));
                int weight = Convert.ToInt16(input[2]);
                Edge e1 = new Edge(v1,v2,weight,index.ToString());
                grph.addEdges(e1);
            }
            int minWeight = 0;
            List<Edge> sortedEdges = grph.getSortedEdges();
            foreach(Edge edge in sortedEdges)
            {
                Vertex v1 = edge.vrtxA;
                Vertex v2 = edge.vrtxB;
                if(v1.representative.name != v2.representative.name)
                {
                    minWeight += edge.weight;
                    v1.set.mergeSet(v1.representative, v2.set);
                }
            }
            Console.WriteLine(minWeight);
            Console.ReadLine();
        }

       
    }
    class Graph
    {
        List<Vertex> lstVertices;
        List<Edge> lstEdges;
        public Graph()
        {
            if (lstVertices == null)
            {
                lstVertices = new List<Vertex>();
            }
            if (lstEdges == null)
            {
                lstEdges = new List<Edge>();
            }
        }
        public Vertex getVertex(int name)
        {
            foreach (Vertex vrtx in lstVertices)
            {
                if (vrtx.name == name)
                    return vrtx;
            }
            Vertex newVertex = new Vertex(name);
            this.addVertex(newVertex);
            return newVertex;
        }
        public void addVertex(Vertex vrtx)
        {
            lstVertices.Add(vrtx);
        }
        public void addEdges(Edge edge)
        {
            lstEdges.Add(edge);
        }
        public List<Edge> getSortedEdges()
        {
            return lstEdges.OrderBy(edge => edge.weight).ToList();
        }
    }
    class Vertex
    {
        public int name;
        List<Vertex> neighbours;
        public Vertex representative;
        public Set set;
        public Vertex(int name)
        {
            this.name = name;
            this.set = new Set(this);
            this.representative = this;
            set.addMemberInSet(this);
        }
        public void addNeighBours(Vertex neighbour)
        {
            this.neighbours.Add(neighbour);
        }
    }
    class Edge
    {
        public int weight;
        string name;
        public Vertex vrtxA, vrtxB;
        public Edge(Vertex vrtxA, Vertex vrtxB, int weight, string name)
        {
            this.vrtxA = vrtxA;
            this.vrtxB = vrtxB;
            this.weight = weight;
            this.name = name;
        }
    }

    class Set
    {
        public List<Vertex> lstVertices;
        public Set(Vertex representative)
        {
            lstVertices = new List<Vertex>();
        }
        public void addMemberInSet(Vertex vrtx)
        {
            lstVertices.Add(vrtx);
        }
        public void mergeSet(Vertex representative, Set set)
        {
            foreach(Vertex vrtx in set.lstVertices)
            {
                vrtx.set = this;
                vrtx.representative = representative;
                this.lstVertices.Add(vrtx);
            }
        }
    }
}
