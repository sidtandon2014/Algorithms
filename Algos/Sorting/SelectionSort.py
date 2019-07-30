def selectionSort(arr):
    for outerIndex, outerItem in enumerate(arr):
        minVal = outerItem
        minIndex = -1
        for innerIndex, innerItem in enumerate(arr[outerIndex + 1:]):
            tmpIndex = innerIndex + outerIndex + 1
            if (minVal > innerItem):
                minVal = innerItem
                minIndex = tmpIndex
        if minIndex != -1:
            arr[minIndex] = outerItem
            arr[outerIndex] = minVal
            #print(outerItem, outerIndex, minIndex + outerIndex)
    return arr


if __name__ == '__main__':
    arr = selectionSort([64, 11, 10, 9])
    print(arr)
