from collections import defaultdict

class Graph:
    def __init__(self):
        self.vertices = defaultdict(list)

    def addEdge(self, startingNode, endingNode):
        self.vertices[startingNode].append(endingNode)

    def graphSearch(self, startingPoint, IS_BFS):
        queue = []
        visited = []
        queue.append(startingPoint)
        visited.append(startingPoint)

        while len(queue) > 0:
            if IS_BFS:
                item = queue.pop(0)
            else:
                item = queue.pop()
            print(item)
            for child in self.vertices[item]:
                if child not in visited:
                    visited.append(child)
                    queue.append(child)


g = Graph()
g.addEdge(0, 1)
g.addEdge(0, 2)
g.addEdge(1, 2)
g.addEdge(2, 0)
g.addEdge(2, 3)
g.addEdge(3, 3)

g.graphSearch(2, False)
