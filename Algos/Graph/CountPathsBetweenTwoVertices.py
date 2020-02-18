from collections import defaultdict


class Graph:
    def __init__(self):
        self.vertices = defaultdict(list)
        self.count = 0
        self.paths = []

    def addEdge(self, startingNode, endingNode):
        self.vertices[startingNode].append(endingNode)

    def countPaths(self, startPoint, endPoint, currentPath=[]):
        newPath = currentPath.copy()
        print(startPoint, newPath)
        if len(newPath) == 0:
            newPath.append(startPoint)

        for item in self.vertices[startPoint]:
            if item not in newPath:
                newPath.append(item)
                if item == endPoint:
                    self.count += 1
                    self.paths.append(newPath)
                    return
                else:
                    self.countPaths(item, endPoint, newPath)
                    newPath.pop()

        return


g = Graph()

# ------First test case
g.addEdge("A", "B")
g.addEdge("A", "C")
g.addEdge("A", "E")
g.addEdge("B", "D")
g.addEdge("B", "E")
g.addEdge("C", "E")
g.addEdge("D", "C")
g.countPaths("A", "E")

#g.addEdge(0, 1)
#g.addEdge(0, 2)
#g.addEdge(0, 3)
#g.addEdge(2, 0)
#g.addEdge(2, 1)
#g.addEdge(1, 3)
#
#g.countPaths(2, 3)

print(g.paths)
