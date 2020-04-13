using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IceCreamParlour
{
    class Program
    {
        static void Main(string[] args)
        {
            int testCases;
            if (int.TryParse(Console.ReadLine(), out testCases))
            {
                if (testCases > 50 || testCases < 1)
                {
                    return;
                }
                else
                {
                    int[,] TotalAmount = new int[testCases, 1];
                    int[,] Flavours = new int[testCases, 1];
                    int[][] FlavoursCost = new int[testCases][];
                    for (int index = 0; index < testCases; index++)
                    {
                        if (int.TryParse(Console.ReadLine(), out TotalAmount[index, 0]))
                        {
                            if (TotalAmount[index, 0] > 10000 || TotalAmount[index, 0] < 2)
                            { return; }
                        }
                        if (int.TryParse(Console.ReadLine(), out Flavours[index, 0]))
                        {
                            if (Flavours[index, 0] > 10000 || Flavours[index, 0] < 2)
                            { return; }
                        }
                        FlavoursCost[index] = new int[Flavours[index, 0]];
                        string[] tmpArr = Console.ReadLine().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                        if (tmpArr.Length != Flavours[index, 0]) { return; }
                        for (int iterator = 0; iterator < Flavours[index, 0]; iterator++)
                        {
                            if (int.TryParse(tmpArr[iterator], out FlavoursCost[index][iterator]))
                            {
                                if (FlavoursCost[index][iterator] > 10000 || FlavoursCost[index][iterator] < 1)
                                    return;
                            }
                            else
                                return;
                        }
                    }
                    //------------print solution
                    for (int index = 0; index < testCases; index++)
                    {
                        int firstIndex, seconodIndex;
                        getCostIndices(FlavoursCost[index], TotalAmount[index, 0], out firstIndex, out seconodIndex);
                        Console.WriteLine("{0} {1}", firstIndex + 1, seconodIndex + 1);
                    }
                }
            }
            Console.ReadLine();
        }

        static void getCostIndices(int[] Cost, int amount, out int firstIndex, out int secondIndex)
        {
            firstIndex = -1;
            secondIndex = -1;
            int totalCosts = Cost.Length;
            for (int outer = 0; outer < totalCosts - 1; outer++)
            {
                firstIndex = outer;
                for (int inner = outer + 1; inner < totalCosts; inner++)
                {
                    if (Cost[outer] + Cost[inner] == amount)
                    {
                        secondIndex = inner;
                        return;
                    }
                }
            }
        }
    }
}
