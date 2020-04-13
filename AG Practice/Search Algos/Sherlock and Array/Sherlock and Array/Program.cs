using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sherlock_and_Array
{
    class Program
    {
        static void Main(string[] args)
        {
            int testCases;
            if (int.TryParse(Console.ReadLine(), out testCases))
            {
                if (testCases > 10 || testCases < 1)
                {
                    return;
                }
                else
                {
                    for (int index = 0; index < testCases; index++)
                    {
                        int arrayElements;
                        if (int.TryParse(Console.ReadLine(), out arrayElements) && arrayElements > 0 && arrayElements < 100000)
                        {
                            int[] elements = new int[arrayElements];
                            string[] ele = Console.ReadLine().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                            if (ele.Length == arrayElements)
                            {
                                for (int enumerator = 0; enumerator < arrayElements; enumerator++)
                                {
                                    int value = Convert.ToInt32(ele[enumerator]);
                                    if (value > 20000 || value < 1)
                                    {
                                        return;
                                    }
                                    elements[enumerator] = value;
                                }
                            }
                            bool isIndexPresent = false;
                            for (int enumerator = 0; enumerator < arrayElements; enumerator++)
                            {
                                if (checkLeftRightSumEqual(elements, enumerator))
                                {
                                    isIndexPresent = true;
                                    Console.WriteLine("YES");
                                    break;
                                }
                            }
                            if (!isIndexPresent)
                                Console.WriteLine("NO");
                        }
                    }

                }
            }
            Console.ReadLine();
        }
        static bool checkLeftRightSumEqual(int[] elements, int index)
        {
            int rightIndex = index + 1, length = elements.Length, leftIndex = index - 1;
            long leftResult = 0, rightResult = 0;
            int tmpIndex = ((leftIndex + 1) > (length - rightIndex)) ? leftIndex : (length - rightIndex);
            while (tmpIndex >= 0)
            {
                if (leftIndex >= 0)
                {
                    leftResult += elements[leftIndex];
                    leftIndex--;
                }
                if (rightIndex < length)
                {
                    rightResult += elements[rightIndex];
                    rightIndex++;
                }
                tmpIndex--;
            }
            if (leftResult == rightResult)
                return true;
            else
                return false;
        }
    }
}
