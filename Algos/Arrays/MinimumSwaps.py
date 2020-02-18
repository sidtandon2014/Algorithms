arr = [7, 1, 3, 2, 4, 5, 6]

index = 0;
swaps = 0
while index < len(arr):
    item = arr[index]
    if (item - 1) != index:
        temp = arr[item - 1]
        arr[item - 1] = item
        arr[index] = temp
        swaps += 1
    else:
        index += 1

print("Total swaps required: {0}".format(swaps))