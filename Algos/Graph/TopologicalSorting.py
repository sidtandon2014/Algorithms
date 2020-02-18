from collections import defaultdict

class Graph:
    def __init__(self):
        self.vertices = defaultdict(list)
        self.topologicalMapping = []

    def addEdge(self, startingNode, endingNode):
        self.vertices[startingNode].append(endingNode)

    def topologicalSortChild(self, vertex):
        if vertex in self.vertices.keys():
            for item in self.vertices[vertex]:
                if item not in self.topologicalMapping:
                    self.topologicalSortChild(item)
        self.topologicalMapping.append(vertex)

    def topologicalSorting(self):
        keyList= self.vertices.keys()
        for key in keyList:
            if key not in self.topologicalMapping:
                self.topologicalSortChild(key)

g = Graph()

# ------First test case
g.addEdge("5", "0")
g.addEdge("5", "2")
g.addEdge("2", "3")
g.addEdge("3", "1")
g.addEdge("4", "0")
g.addEdge("4", "1")

g.topologicalSorting()
print(g.topologicalMapping[::-1])