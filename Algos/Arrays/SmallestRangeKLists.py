# https://practice.geeksforgeeks.org/problems/find-smallest-range-containing-elements-from-k-lists/1
from math import floor
import numpy as np


class Element:
    def __init__(self, item, arrIndex, index):
        self.item = item
        self.arrIndex = arrIndex
        self.index = index


class Heap:
    def __init__(self, arr, typeMulFactor):
        self.arr = arr
        self.typeMulFactor = typeMulFactor
        self.min = float("inf")
        self.max = float("-inf")
        self.range = float("inf")

    def getRange(self):
        return self.range

    def shiftUp(self, index):
        parentIndex = (index - 1) // 2
        if parentIndex < 0:
            return

        if self.typeMulFactor * self.arr[parentIndex].item > self.typeMulFactor * self.arr[index].item:
            self.arr[index], self.arr[parentIndex] = self.arr[parentIndex], self.arr[index]
            self.shiftUp(parentIndex)

    def shiftDown(self, index):
        childIndex = -1
        leftChild = (index + 1) * 2 - 1
        rightChild = (index + 1) * 2

        if leftChild >= len(self.arr) and rightChild >= len(self.arr):
            self.arr.pop(index)
        elif leftChild < len(self.arr) <= rightChild:
            self.arr[index] = self.arr[leftChild]
            self.arr.pop(leftChild)
        elif self.arr[leftChild].item > self.arr[rightChild].item:
            childIndex = leftChild
        elif self.arr[rightChild].item > self.arr[leftChild].item:
            childIndex = rightChild

        if childIndex == -1:
            return

        self.arr[index] = self.arr[childIndex]
        self.shiftDown(childIndex)

    def heapPush(self, item, arrIndex):
        ele = Element(item, arrIndex, -1)
        self.arr.append(ele)
        index = len(self.arr) - 1
        self.shiftUp(index)
        self.assignMinMax(item)

    # ---------Priority queue
    def heapPop(self, index):
        topElement = self.arr[index]
        self.shiftDown(index)
        return topElement

    def heapify(self, index):
        smallest = index
        totalEle = len(self.arr)
        leftChild = (index + 1) * 2 - 1
        rightChild = (index + 1) * 2

        if leftChild < totalEle and self.typeMulFactor * self.arr[index].item > self.typeMulFactor * self.arr[
            leftChild].item:
            smallest = leftChild
        if rightChild < totalEle and self.typeMulFactor * self.arr[smallest].item > self.typeMulFactor * self.arr[
            rightChild].item:
            smallest = rightChild
        if smallest != index:
            self.arr[index], self.arr[smallest] = self.arr[smallest], self.arr[index]
            self.heapify(smallest)

    def getarrIndexMinEle(self):
        return self.arr[0].arrIndex

    def build_heap(self):
        for index in range(floor(len(self.arr) / 2) - 1, -1, -1):
            self.heapify(index)
        self.assignRange()

    def replaceRoot(self, item, arrIndex):
        self.assignMinMax(item)
        ele = Element(item, arrIndex, -1)
        self.arr[0] = ele
        self.build_heap()

    def assignMinMax(self, item):
        if self.max < item:
            self.max = item

    def assignRange(self):
        newRange = self.max - self.arr[0].item
        if self.range > newRange:
            self.range = newRange
            self.min = self.arr[0].item


def smallestRange(arr, k, n):
    arrIndexes = np.zeros(k, int)
    heap = Heap([], 1)
    [heap.heapPush(int(arr[index, 0]), index) for index, item in enumerate(arrIndexes)]
    heap.assignRange()
    minIndex = heap.getarrIndexMinEle()
    arrIndexes[minIndex] += 1
    while np.max(arrIndexes) < n:
        heap.replaceRoot(int(arr[minIndex, arrIndexes[minIndex]]), minIndex)
        minIndex = heap.getarrIndexMinEle()
        arrIndexes[minIndex] += 1
    return heap.getRange(), heap.max, heap.min


str = "1 3 5 7 9 0 2 4 6 8 2 3 5 7 11"
str = "1 2 3 4 5 6 7 8 9 10 11 12"
k = 3
n = 4

ele = np.reshape(str.split(" "), (k, n))
print(smallestRange(ele, k, n))
#print("Smallest Range {0}. Min: {1}, Max: {2}".format(smallestRange(ele, k, n)))
