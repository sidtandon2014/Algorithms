using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


class Solution
{
    static void Main(string[] args)
    {
        int testCases, totalEdges, totalVertices, maxFeet = 0;
        if (int.TryParse(Console.ReadLine(), out testCases))
        {
            if (testCases < 1 || testCases > 10)
                return;
            for (int testCaseIndex = 0; testCaseIndex < testCases; testCaseIndex++)
            {
                Graph grph = new Graph();
                string[] input = Console.ReadLine().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                if (input.Length == 3 && int.TryParse(input[0], out totalVertices) && int.TryParse(input[1], out maxFeet) && int.TryParse(input[2], out totalEdges))
                {
                    if (maxFeet < 2 || maxFeet > 100 || totalVertices < 2 || totalVertices > 100 || totalEdges < 0 || totalEdges > (totalVertices * (totalVertices - 1) / 2))
                        return;
                    for (int index = 0; index < totalVertices; index++)
                    {
                        Vertex vrtx = new Vertex(index + 1);
                        grph.addVertex(vrtx);
                    }
                    for (int index = 0; index < totalEdges; index++)
                    {
                        string[] edgePair = Console.ReadLine().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                        if (edgePair.Length == 2)
                        {
                            int vrtxA = Convert.ToInt32(edgePair[0]);
                            int vrtxB = Convert.ToInt32(edgePair[1]);
                            if (vrtxA < 1 || vrtxA > totalVertices || vrtxB < 1 || vrtxB > totalVertices)
                                return;
                            Vertex v1 = grph.getVertex(vrtxA);
                            Vertex v2 = grph.getVertex(vrtxB);
                            grph.addNeighbours(v1, v2);
                        }
                    }
                }
                int maxVerticesCrabCovers = 0, tmpMaxVerticesCrabCovers = 0;
                
                if (hasMultipleGraphs(grph))
                {
                    maxVerticesCrabCovers += grph.totalVerticesContributingToGraph;
                }
                else
                {

                    List<Edge> edgeList = grph.getAllEdges();
                    foreach (Edge edge in edgeList)
                    {
                        tmpMaxVerticesCrabCovers = getTotalVerticesInvolved(edge, maxFeet);
                        if (tmpMaxVerticesCrabCovers > maxVerticesCrabCovers)
                        {
                            maxVerticesCrabCovers = tmpMaxVerticesCrabCovers;
                        }
                    }
                }
                Console.WriteLine("{0}", maxVerticesCrabCovers);
            }
            Console.ReadLine();
        }
    }

    public static bool hasMultipleGraphs(Graph g)
    {
        List<Vertex> allVertices = g.getAllVertices();
        List<Int64> SameCountryAstronautsNumber = new List<long>();
        int totalDFSCall = 0;
        foreach (Vertex v in allVertices)
        {
            if (!v.isVertexExplored() )
            {
                DFS(g, v);
                totalDFSCall += 1;
            }

            if (totalDFSCall > 1 )
            {
               return true;
            }
        }
        return false;
    }
    public static void DFS(Graph g, Vertex root)
    {
        if (!root.isVertexExplored())
        {
            root.setVertexExplored();
            List<Vertex> neighbours = root.getNeighbours();
            foreach (Vertex v in neighbours)
            {
                DFS(g, v);
            }
        }
    }
    public static int getTotalVerticesInvolved(Edge edge, int feet)
    {
        int totalVerticesInvolved = 0;
        Vertex u = edge.getVertices(1);
        Vertex v = edge.getVertices(2);
        int tmpDegreeU = u.getDegree() - 1;
        int tmpDegreeV = v.getDegree() - 1;
        if ((tmpDegreeU > 1 || tmpDegreeV > 1) && (tmpDegreeU != 0 && tmpDegreeV != 0))
        {
            totalVerticesInvolved += Math.Min(tmpDegreeU, feet) + Math.Min(tmpDegreeV, feet) + 2 - getTotalNeighboursShared(u.getNeighbours(), v.getNeighbours());
        }
        return totalVerticesInvolved;
        //totalVerticesInvolved = Math.Min(feet)
    }

    public static int getTotalNeighboursShared(List<Vertex> neighboursU, List<Vertex> neighboursV)
    {
        int totalNeighboursShared = 0;
        foreach (Vertex vrtxU in neighboursU)
        {
            foreach (Vertex vrtxV in neighboursV)
            {
                if (vrtxU.data == vrtxV.data)
                {
                    totalNeighboursShared++;

                }
            }
        }
        return totalNeighboursShared;
    }
}
class Vertex
{
    public int data;
    private List<Vertex> neighbours;
    private bool isTraversed;
    private int degree;
    public bool isCounted = false;

    public int getDegree()
    {
        return this.degree;
    }
    public Vertex(int data)
    {
        this.data = data;
        isTraversed = false;
        neighbours = new List<Vertex>();
        degree = 0;
    }
    public void addNeighbour(Vertex vertexB)
    {
        if (!neighbours.Any(f => f.data == vertexB.data))
        {
            this.neighbours.Add(vertexB);
            this.degree += 1;
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

class Edge
{
    private Vertex u;
    private Vertex v;
    public Edge(Vertex u, Vertex v)
    {
        this.u = u;
        this.v = v;
    }

    public Vertex getVertices(int vertexNumber)
    {
        if (vertexNumber == 1)
            return this.u;
        else
            return this.v;
    }
}
class Graph
{
    private List<Vertex> vertexList;
    private List<Edge> edgeList;
    public int totalVerticesContributingToGraph;
    public Graph()
    {
        if (vertexList == null)
        {
            vertexList = new List<Vertex>();
        }
        if (edgeList == null)
        {
            edgeList = new List<Edge>();
        }
        totalVerticesContributingToGraph = 0;
    }

    public void addVertex(Vertex vertex)
    {
        this.vertexList.Add(vertex);
    }

    private void addEdge(Edge edge)
    {
        this.edgeList.Add(edge);
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
        Edge e = new Edge(from, to);
        addEdge(e);
        if (!from.isCounted)
        {
            totalVerticesContributingToGraph++;
            from.isCounted = true;
        }
        if (!to.isCounted)
        {
            totalVerticesContributingToGraph++;
            to.isCounted = true;
        }
        
    }

    public List<Vertex> getAllVertices()
    {
        return vertexList;
    }

    public List<Edge> getAllEdges()
    {
        return this.edgeList;
    }

    //public List<Vertex>
}
