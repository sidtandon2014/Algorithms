def mergeSort(arr, left, right):
    middle = left + (right - left) // 2
    if left < middle:
        mergeSort(arr, left, middle)
    if (middle + 1) < right:
        mergeSort(arr, middle + 1, right)
    merge(arr, left, middle, right)


def merge(arr, left, middle, right):
    leftArrIndex = 0
    rightArrIndex = 0
    arrLeft = arr[left:middle + 1]
    arrRight = arr[middle + 1: right + 1]

    while True:
        if (leftArrIndex < len(arrLeft)) & (rightArrIndex < len(arrRight)):
            if arrLeft[leftArrIndex] >= arrRight[rightArrIndex]:
                arr[left] = arrRight[rightArrIndex]
                rightArrIndex += 1
            else:
                arr[left] = arrLeft[leftArrIndex]
                leftArrIndex += 1
        elif leftArrIndex < len(arrLeft):
            arr[left] = arrLeft[leftArrIndex]
            leftArrIndex += 1
        elif rightArrIndex < len(arrRight):
            arr[left] = arrRight[rightArrIndex]
            rightArrIndex += 1
        else:
            break;
        left += 1

if __name__ == '__main__':
    arr = [2, 1, 3,-1, 0, 10, 56]
    mergeSort(arr, 0, len(arr) - 1)
    print(arr)
