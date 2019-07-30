#https://www.geeksforgeeks.org/bubble-sort/
def bubbleSort(arr, asc):
    multiplicationFactor = -1
    if asc:
        multiplicationFactor = 1
    while(True):
        isSwapped = False
        for index,item in enumerate(arr[:len(arr)-1]):
            nextIndex = index + 1
            nextItem = arr[nextIndex]
            if multiplicationFactor * item > multiplicationFactor * nextItem:
                tmp = arr[nextIndex]
                arr[nextIndex] = arr[index]
                arr[index] = tmp
                isSwapped = True
        if not isSwapped:
            return arr

if __name__ == '__main__':
    print(bubbleSort([1000,5,1,4,2,8,100], False))