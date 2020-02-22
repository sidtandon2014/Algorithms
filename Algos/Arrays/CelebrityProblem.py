# https://practice.geeksforgeeks.org/problems/the-celebrity-problem/1
import numpy as np


def getId(m, n):
    import numpy as np
    matrix = np.reshape(m, (n, n))
    rows = cols = n
    isCelebrity = False
    if matrix.sum() == 0:
        return -1
    for oi in range(rows):
        isCelebrity = True
        for ii in range(cols):
            if oi != ii:
                if matrix[oi, ii] == 1:
                    isCelebrity = False
                    break;

        if isCelebrity:
            return oi

    return -1


def getAdvancedId(m, n):
    import numpy as np
    matrix = np.reshape(m, (n, n))
    celebrityIndex = -1
    cols = n
    celebrityDict = {index: True for index in range(n)}
    celebrityList = [index for index in range(n)]

    if matrix.sum() == 0:
        return celebrityIndex

    while True:
        if len(celebrityList) > 1:
            firstEle = celebrityList[0]
            secondEle = celebrityList[1]
            if matrix[firstEle,secondEle] == 0:
                celebrityList.remove(secondEle)
            else:
                celebrityList.remove(firstEle)
        elif len(celebrityList) == 1 and matrix[celebrityList[0],:].sum() == 0:
            return celebrityList[0]
        else:
            return -1


from math import sqrt

arr = [0, 1, 1, 0, 0, 1, 0, 1, 0]
#arr = [0, 1, 1, 0, 0, 0, 0, 1, 0]
#arr = [0, 1, 1, 0]
#arr = [0, 0, 0, 0, 0, 0, 0, 0, 0]

n = int(sqrt(len(arr)))
matrix = np.reshape(arr, (n, n))

print("Celebrity Index {0}".format(getAdvancedId(arr, n)))
