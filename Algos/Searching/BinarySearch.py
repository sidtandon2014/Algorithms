import numpy as np

numberArray = np.array([1, 5, 7, 9, 11, 14, 16, 18, 21])
numberToSearch = 10


def searchNumberInArray(left, right):
    middleIndex = left + (right - left) // 2
    if numberArray[middleIndex] == numberToSearch:
        print("Number Found. Index: ${0}".format(middleIndex))
    elif left == right:
        print("Number not found")
    elif numberToSearch < numberArray[middleIndex]:
        searchNumberInArray(left, middleIndex - 1)
    elif numberToSearch > numberArray[middleIndex]:
        searchNumberInArray(middleIndex + 1, right)


searchNumberInArray(0, len(numberArray) - 1)
