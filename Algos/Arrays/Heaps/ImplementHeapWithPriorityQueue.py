from math import floor


class Heap:
    def __init__(self, arr, typeMulFactor):
        self.arr = arr
        self.typeMulFactor = typeMulFactor

    def shiftUp(self, index):
        parentIndex = (index - 1) // 2
        if parentIndex < 0:
            return

        if self.typeMulFactor * self.arr[parentIndex] > self.typeMulFactor * self.arr[index]:
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
        elif self.arr[leftChild] > self.arr[rightChild]:
            childIndex = leftChild
        elif self.arr[rightChild] > self.arr[leftChild]:
            childIndex = rightChild

        if childIndex == -1:
            return

        self.arr[index] = self.arr[childIndex]
        self.shiftDown(childIndex)

    def heapPush(self, item):
        self.arr.append(item)
        index = len(self.arr) - 1
        self.shiftUp(index)

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

        if leftChild < totalEle and self.typeMulFactor * self.arr[index] > self.typeMulFactor * self.arr[leftChild]:
            smallest = leftChild
        if rightChild < totalEle and self.typeMulFactor * self.arr[smallest] > self.typeMulFactor * self.arr[
            rightChild]:
            smallest = rightChild
        if smallest != index:
            self.arr[index], self.arr[smallest] = self.arr[smallest], self.arr[index]
            self.heapify(smallest)

    def build_heap(self):
        for index in range(floor(len(self.arr) / 2) - 1, -1, -1):
            self.heapify(index)


elements = [1, 3, 8, 2, 9, 10, 14, 7, 16, 4]
heap = Heap(elements, -1)
heap.build_heap()

heap.heapPush(24)
heap.heapPush(30)
heap.heapPush(32)

heap.heapPop(0)
heap.heapPop(0)
heap.heapPop(0)
print(heap.arr)
