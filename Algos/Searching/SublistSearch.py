import numpy as np

mainList = [2, 3, 4, 6, 7, 8, 10, 12]
listToSearch = [7, 8, 10]


def useAllToSearch():
    if (all(item in mainList for item in listToSearch)):
        print("List found")
    else:
        print("List not found")


def useSetFunctionToSearch():
    if (set(listToSearch).issubset(set(mainList))):
        print("List found")
    else:
        print("List not found")


def orderedSearch():
    smallListIndex = 0
    bigListIndex = 0
    smallListLen = len(listToSearch)
    bigListLen = len(mainList)
    while True:
        if smallListIndex >= smallListLen:
            print("List found. Index {0}".format(bigListIndex - smallListLen))
            break
        elif (bigListIndex >= bigListLen) & (smallListIndex <= smallListLen):
            print("List not found")
            break
        elif mainList[bigListIndex] == listToSearch[smallListIndex]:
            bigListIndex += 1
            smallListIndex += 1
        else:
            bigListIndex += 1
            smallListIndex = 0


# useAllToSearch()
# useSetFunctionToSearch()
orderedSearch()
