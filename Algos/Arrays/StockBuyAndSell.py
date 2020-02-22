# https://practice.geeksforgeeks.org/problems/stock-buy-and-sell2615/1

def stockBuySell(arr):
    size = len(arr)
    oi = 0
    days = []
    startPrice = -1
    startPriceIndex = -1
    while oi < size:
        if startPrice == -1:
            startPrice = arr[oi]
            startPriceIndex = oi
        todayPrice = arr[oi]
        if oi + 1 < size:
            nextDayPrice = arr[oi + 1]
            if nextDayPrice < todayPrice :
                if startPrice < todayPrice:
                    days.append([startPriceIndex, oi])
                startPrice = -1
        elif startPrice < todayPrice:
            days.append([startPriceIndex, oi])
        oi += 1

    if len(days) == 0:
        days.append("No Profit")
    return days


str = "100 180 260 310 40 535 695"
#str = "23 13 25 29 33 19 34 45 65 67"
#str = "100 90 84 12 1"
ele = [int(x) for x in str.split(" ")]
print(stockBuySell(ele))
