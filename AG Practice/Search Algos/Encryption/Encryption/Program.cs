using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Encryption
{
    class Program
    {
        static void Main(string[] args)
        {
            string message = string.Empty;
            message = Console.ReadLine();
            int messageLength = message.Length;
            int rows, columns;
            if (messageLength > 81 || messageLength < 0)
            {
                return;
            }
            double sqRootLength = Math.Sqrt(messageLength);
            rows = (int)Math.Floor(sqRootLength);
            columns = (int)Math.Ceiling(sqRootLength);
            printEncodedMessage(rows, columns, messageLength, message);
            Console.ReadLine();
        }

        static void printEncodedMessage(int rows, int columns, int messageLength, string message)
        {
            int tmpArea;
            int tmpRows, tmpCols;
            tmpRows = rows;
            tmpCols = tmpRows;
            tmpArea = tmpRows * tmpCols;
            while (tmpArea < messageLength)
            {
                if (tmpCols == tmpRows && tmpCols <= columns)
                {
                    tmpCols += 1;
                }
                else if (tmpRows < tmpCols)
                {
                    tmpRows += 1;
                }
                tmpArea = tmpRows * tmpCols;
            }
            for (int indexC = 0; indexC < tmpCols; indexC++)
            {
                for (int index = 0; index < tmpRows; index++)
                {
                    int letterIndex = indexC + (index * tmpCols);
                    if(letterIndex < messageLength)
                        Console.Write(message[letterIndex]);
                }
                if (indexC != tmpCols - 1)
                {
                    Console.Write(" ");
                }
            }
        }
    }
}
