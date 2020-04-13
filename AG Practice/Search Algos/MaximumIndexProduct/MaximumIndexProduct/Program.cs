using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaximumIndexProduct
{
    class Program
    {
        static long[] number;
        static long[] left;
        static long[] right;
        static long totalNumbers;
        static void Main(string[] args)
        {
            long max = 0;
            if (Int64.TryParse(Console.ReadLine(), out totalNumbers))
            {
                if (totalNumbers < 1 || totalNumbers > 100000)
                {
                    return;
                }
                else
                {
                    number = new long[totalNumbers];
                    left = new long[totalNumbers];
                    right = new long[totalNumbers];
                    string[] arr = Console.ReadLine().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    if (arr.Length != totalNumbers)
                    {
                        return;
                    }
                    else
                    {
                        //----------Create array
                        for (long index = 0; index < totalNumbers; index++)
                        {
                            left[index] = 0;
                            right[index] = 0;
                            if (!Int64.TryParse(arr[index], out number[index]))
                            {
                                return;
                            }
                            if (number[index] < 0 || number[index] > 1000000000)
                            {
                                return;
                            }
                        }

                        //-----------Create left array
                        for (long index = 1; index < totalNumbers - 1; index++)
                        {
                            long tmpIndex = index - 1;
                            while (tmpIndex != -1)
                            {
                                if (number[index] < number[tmpIndex])
                                {
                                    left[index] = tmpIndex + 1;
                                    break;
                                }
                                else
                                {
                                    tmpIndex = left[tmpIndex] - 1;
                                }
                            }

                        }

                        //------------Create right array
                        for (long index = totalNumbers - 2; index > 0; index--)
                        {
                            long tmpIndex = index + 1;
                            while (tmpIndex != -1)
                            {
                                if (number[index] < number[tmpIndex])
                                {
                                    right[index] = tmpIndex + 1;
                                    break;
                                }
                                else
                                {
                                    tmpIndex = right[tmpIndex] - 1;
                                }
                            }
                        }

                        for (long index = 1; index < totalNumbers - 1; index++)
                        {
                            //int curVal = IndexProduct(index);
                            long curVal = left[index] * right[index];
                            if (curVal > max)
                            {
                                max = curVal;
                            }
                        }
                        Console.Write(max);
                    }
                }
            }
            Console.ReadLine();
        }
        //static int IndexProduct(int index)
        //{
        //    return Left(index) * Right(index);
        //}
        //static int Left(int index)
        //{
        //    for (int iterator = index - 1; iterator >= 0; iterator--)
        //    {
        //        if (number[iterator] > number[index])
        //        {
        //            return iterator + 1;
        //        }
        //    }
        //    return 0;
        //}
        //static int Right(int index)
        //{
        //    for (int iterator = index + 1; iterator < totalNumbers; iterator++)
        //    {
        //        if (number[iterator] > number[index])
        //        {
        //            return iterator + 1;
        //        }
        //    }
        //    return 0;
        //}
    }
}
