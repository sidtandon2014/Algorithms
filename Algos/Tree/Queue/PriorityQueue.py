import numpy as np


class Node:
    def __init__(self, key):
        self.level = None
        self.leftNode = None
        self.rightNode = None
        self.key = key


class Tree:
    def __init__(self, rootNode):
        self.rootNode = rootNode
        self.rootNode.level = 0

    def addEdge(self, parentNode, leftNode, rightNode):
        parentNode.leftNode = leftNode
        parentNode.rightNode = rightNode

    def isEmpty():


    def getChildren(self, node):
        childrens = [node.leftNode if node.leftNode is not None else None,
                     node.rightNode if node.rightNode is not None else None]
        return childrens

    def sumkthLevelNodes(self, parentNode, k, level=0):
        total = 0
        if k == 0:
            return parentNode.key
        children = self.getChildren(parentNode)
        level += 1
        for item in children:
            if level == k:
                total += item.key
            else:
                total += self.sumkthLevelNodes(item, k, level)
        return total


n1 = Node(10)
n2 = Node(20)
n3 = Node(50)
n4 = Node(50)
n5 = Node(23)
n6 = Node(10)
n7 = Node(15)

tree = Tree(n1)
tree.addEdge(n1, n2, n3)
tree.addEdge(n2, n4, n5)
tree.addEdge(n3, n6, n7)

print("Total sum {0}".format(tree.sumkthLevelNodes(n1, 0)))
