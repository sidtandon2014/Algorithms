using System;
using System.Collections.Generic;
using System.Text;

namespace SearchMethods
{
    public class LinearSearch
    {
        int [] x= new int[] { 1, 5, 7, 1, 10 };
        public void SearchNumber(int searchNumber)
        {
            for(int index = 0;index<x.Length; index++)
            {
                if(x[index] == searchNumber)
                {
                    Console.WriteLine("Number found: Index - {0}", (index+1).ToString());
                    return;
                }
            }
            Console.WriteLine("Number not found");
        }
    }
}
