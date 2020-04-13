using System;

class Solution
{
    static int[,] dataArr;// = new int[4, 4] { { 1, 1, 0, 0 }, { 0, 1, 1, 0 }, { 0, 0, 1, 0 }, { 1, 0, 0, 0 } };
    static int[,] visited;// = new int[4, 4] { { 0, 0, 0, 0 }, { 0, 0, 0, 0 }, { 0, 0, 0, 0 }, { 0, 0, 0, 0 } };

    static int m = 0, n = 0;
    static void Main(string[] args)
    {
        int row, col;
        if (Int32.TryParse(Console.ReadLine(), out row) && Int32.TryParse(Console.ReadLine(), out col))
        {
            dataArr = new int[row, col];
            visited = new int[row, col];
            m = row - 1;
            n = col - 1;
            for (int indexR = 0; indexR < row; indexR++)
            {
                string[] matrixR = Console.ReadLine().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                if (matrixR.Length == col)
                {
                    for (int indexC = 0; indexC < col; indexC++)
                    {
                        if (Int32.TryParse(matrixR[indexC], out dataArr[indexR, indexC]))
                        {
                            visited[indexR, indexC] = 0;
                        }
                        else { throw new InvalidCastException(); }
                    }
                }
                else
                {
                    throw new IndexOutOfRangeException();
                }
            }
            Console.WriteLine("{0}", MaxRegion());
        }
        //Console.Read();
    }
    static int MaxRegion()
    {
        int MaxLen = 0, Len = 0;
        for (int row = 0; row <= m; row++)
        {
            for (int col = 0; col <= n; col++)
            {
                if (visited[row, col] == 0 && dataArr[row, col] == 1)
                {
                    Len = getLen(row, col);
                    if (Len > MaxLen)
                    {
                        MaxLen = Len;
                    }
                }
            }

        }
        return MaxLen;
    }
    static int getLen(int curRow, int curCol)
    {
        if (curRow > m || curRow < 0 || curCol > n || curCol < 0)
        {
            return 0;
        }
        if (dataArr[curRow, curCol] == 1 && visited[curRow, curCol] == 0)
        {
            visited[curRow, curCol] = 1;
            return 1 + getLen(curRow - 1, curCol)
                + getLen(curRow, curCol - 1)
                + getLen(curRow - 1, curCol - 1)
                + getLen(curRow + 1, curCol)
                + getLen(curRow, curCol + 1)
                + getLen(curRow + 1, curCol + 1)
                + getLen(curRow - 1, curCol + 1)
                + getLen(curRow + 1, curCol - 1);
        }
        else
        {
            visited[curRow, curCol] = 1;
            return 0;
        }
    }
}
