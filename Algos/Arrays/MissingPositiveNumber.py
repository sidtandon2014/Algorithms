# https://www.geeksforgeeks.org/find-the-smallest-positive-number-missing-from-an-unsorted-array/

def findMissingNumber(arr):
    size = len(arr)
    for i in range(size):
        if abs(arr[i]) - 1 < size and arr[abs(arr[i]) - 1] > 0:
            arr[abs(arr[i]) - 1] = -arr[abs(arr[i]) - 1]

    for i in range(size):
        if arr[i] > 0:
            return i + 1

    return size + 1


ele = [2, 1, 3]
print("Smallest postive number {0}".format(findMissingNumber(ele)))
