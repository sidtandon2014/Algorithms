def quicksort(arr, left, right):
    if left < right:
        partitionIndex = partitioner(arr, left, right)

        quicksort(arr, left, partitionIndex - 1)
        quicksort(arr, partitionIndex + 1, right)


def partitioner(arr, left, right):
    pivotEle = arr[right]
    arrIndex = left
    swapIndex = left
    while arrIndex <= right - 1:
        if arr[arrIndex] <= pivotEle:
            tmp = arr[arrIndex]
            arr[arrIndex] = arr[swapIndex]
            arr[swapIndex] = tmp
            swapIndex += 1
        arrIndex += 1
    arr[right] = arr[swapIndex]
    arr[swapIndex] = pivotEle
    return swapIndex


if __name__ == '__main__':
    arr = [4, 2, 0, 1, -1, 100, 500, 23, 45]
    quicksort(arr, 0, len(arr) - 1)
    print(arr)
