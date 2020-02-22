elements = [8, 3, 1]


class Basic:
    def checkMultiples(self, endIndex):
        index = 0
        multiples = 0
        item = elements[endIndex]
        while index < endIndex:
            if elements[index] % item == 0:
                multiples += 1
            index += 1
        return multiples

    def getMultiples(self, arr1):
        maxMultiples = 0
        for index in range(len(arr1)):
            multiples = self.checkMultiples(index)
            if multiples > maxMultiples:
                maxMultiples = multiples
        return maxMultiples


class Advanced:
    def __init__(self):
        self.MAX = 70
        self.divisors = [0] * self.MAX

    # Function to generate the divisors
    # of all the array elements
    def generateDivisors(self, n):
        from math import ceil, sqrt
        for i in range(1, ceil(sqrt(n)) + 1):
            if (n % i == 0):
                if (n // i == i):
                    self.divisors[i] += 1
                else:
                    self.divisors[i] += 1
                    self.divisors[n // i] += 1

    # Function to find the maximum number
    # of multiples in an array before it
    def findMaxMultiples(self, arr, n):
        # To store the maximum divisor count
        ans = 0
        for i in range(n):
            # Update ans if more number
            # of divisors are found
            ans = max(self.divisors[arr[i]], ans)

            # Generating all the divisors of
            # the next element of the array
            self.generateDivisors(arr[i])
        return ans


print("Max Mutliples {0}".format(Advanced().findMaxMultiples(elements, len(elements))))
# print("Max Mutliples {0}".format(Basic().getMultiples(elements)))
