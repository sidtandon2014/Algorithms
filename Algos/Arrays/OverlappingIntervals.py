# https://practice.geeksforgeeks.org/problems/overlapping-intervals4919/1

import numpy as np

arr = [[6,8], [1,9], [2,4], [4,7]]


def swapInterval(ele, oi, ii):
    if ele[oi][0] > ele[ii][0] or (ele[oi][0] == ele[ii][0] and ele[oi][1] > ele[ii][1]):
        ele[oi], ele[ii] = ele[ii], ele[oi]


def sortList(ele):
    size = len(ele)
    for oi in range(size):
        for ii in range(oi + 1, size):
            swapInterval(ele, oi, ii)


def mergeIntervals(ele):
    size = len(ele)
    oi = 0
    while oi < size - 1:
        ii = oi + 1
        if ele[oi][0] <= ele[ii][0] < ele[oi][1]:
            ele[oi] = [ele[oi][0], max(ele[ii][1], ele[oi][1])]
            size -= 1
            del ele[ii]
        else:
            oi += 1


sortList(arr)
mergeIntervals(arr)
print(arr)
