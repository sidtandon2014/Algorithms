using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CountLuck
{
    class Program
    {
        static int rows, cols;
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
                        int NumberOfTimesWandWaved;
                        string[] gridSize = Console.ReadLine().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                        if (gridSize.Length == 2 && int.TryParse(gridSize[0], out rows) && int.TryParse(gridSize[1], out cols))
                        {
                            if (rows > 100 || rows < 1 || cols > 100 || cols < 1)
                                return;

                            char[][] ForestGrid = new char[rows][];
                            int[,] ForestVisited = new int[rows, cols];
                            string gridRow = string.Empty;
                            int startRow = -1, startCol = -1;
                            bool isGridContainsM = false;
                            for (int indexR = 0; indexR < rows; indexR++)
                            {
                                //gridRow = input().Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries)[indexR].Replace("\r","");//
                                gridRow = Console.ReadLine();
                                if (gridRow.Length != cols)
                                    return;
                                else
                                {
                                    ForestGrid[indexR] = gridRow.ToCharArray();
                                    if (gridRow.Contains("M"))
                                    {
                                        if (isGridContainsM)
                                            return;
                                        isGridContainsM = true;
                                        startCol = gridRow.IndexOf('M');
                                        startRow = indexR;
                                    }

                                }
                            }
                            if (int.TryParse(Console.ReadLine(), out NumberOfTimesWandWaved))
                            {
                                if (NumberOfTimesWandWaved < 0 || NumberOfTimesWandWaved > 10000)
                                { return; }
                            }
                            else { return; }

                            if (startRow == -1 || startCol == -1)
                                return;
                            string[] path = getCorrectPath(ForestGrid, ForestVisited, startRow, startCol, startRow, startCol).Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                            int tmpIndex = 0, tmpWandWaved = 0;
                            int prevRow = startRow, prevCol = startCol;
                            while (ForestGrid[startRow][startCol] != '*')
                            {
                                tmpWandWaved += isWandWaved(ForestGrid, path, startRow, startCol, prevRow, prevCol);
                                tmpIndex++;
                                if (tmpIndex < path.Length)
                                {
                                    string[] pathCoord = path[tmpIndex].Split(new char[] { '_' });
                                    prevRow = startRow;
                                    prevCol = startCol;
                                    if (!(pathCoord.Length == 2 && int.TryParse(pathCoord[0], out startRow) && int.TryParse(pathCoord[1], out startCol)))
                                    { return; }
                                }
                            }
                            if (tmpWandWaved == NumberOfTimesWandWaved)
                            { Console.WriteLine("Impressed"); }
                            else { Console.WriteLine("Oops!"); }
                        }
                        else
                            return;
                    }

                }
            }
            Console.ReadLine();
        }

        public static string getCorrectPath(char[][] ForestGrid, int[,] ForestVisited, int curRow, int curCol, int prevRow, int prevCol)
        {
            if (curRow < 0 || curCol < 0 || curRow > (rows - 1) || curCol > (cols - 1) || ForestVisited[curRow, curCol] == 1)
                return "";

            ForestVisited[curRow, curCol] = 1;
            string path = curRow + "_" + curCol + ";";
            if (ForestGrid[curRow][curCol] == 'M' || ForestGrid[curRow][curCol] == '.')
            {
                string tmpPath = string.Empty;
                if ((curRow - 1) != prevRow || curCol != prevCol)
                {
                    tmpPath = getCorrectPath(ForestGrid, ForestVisited, curRow - 1, curCol, curRow, curCol);
                    if (tmpPath.Contains("*"))
                        return path + tmpPath;
                }

                if (curRow != prevRow || (curCol - 1) != prevCol)
                {
                    tmpPath = getCorrectPath(ForestGrid, ForestVisited, curRow, curCol - 1, curRow, curCol);
                    if (tmpPath.Contains("*"))
                        return path + tmpPath;
                }

                if ((curRow + 1) != prevRow || curCol != prevCol)
                {
                    tmpPath = getCorrectPath(ForestGrid, ForestVisited, curRow + 1, curCol, curRow, curCol);
                    if (tmpPath.Contains("*"))
                        return path + tmpPath;
                }

                if (curRow != prevRow || (curCol + 1) != prevCol)
                {
                    tmpPath = getCorrectPath(ForestGrid, ForestVisited, curRow, curCol + 1, curRow, curCol);
                    if (tmpPath.Contains("*"))
                        return path + tmpPath;
                }
            }
            else if (ForestGrid[curRow][curCol] == '*')
            {
                path += "*;";
            }
            else if (ForestGrid[curRow][curCol] == 'X')
            {
                path += "X;";
            }
            return path;
        }

        public static int isWandWaved(char[][] ForestGrid, string[] path, int startRow, int startCol, int prevRow, int prevCol)
        {
            int paths = 0;
            if (startCol - 1 >= 0 && (prevRow != startRow || prevCol != (startCol - 1)))
            {
                paths += (ForestGrid[startRow][startCol - 1] == '.' || ForestGrid[startRow][startCol - 1] == '*') ? 1 : 0;
            }
            if (startCol + 1 < cols && (prevRow != startRow || prevCol != (startCol + 1)))
            {
                paths += ((ForestGrid[startRow][startCol + 1] == '.' || ForestGrid[startRow][startCol + 1] == '*') ? 1 : 0);
            }
            if (startRow - 1 >= 0 && (prevRow != (startRow - 1) || prevCol != startCol))
            {
                paths += ((ForestGrid[startRow - 1][startCol] == '.' || ForestGrid[startRow - 1][startCol] == '*') ? 1 : 0);
            }
            if (startRow + 1 < rows && (prevRow != (startRow + 1) || prevCol != startCol))
            {
                paths += ((ForestGrid[startRow + 1][startCol] == '.' || ForestGrid[startRow + 1][startCol] == '*') ? 1 : 0);
            }
            if (paths > 1)
            {
                return 1;
            }
            return 0;
        }

        static string input()
        {

            return @".X.XXXXXXXXXXXXXXXXXXX.X.X.X.X.X.X.X.X.X.
...XXXXXXXXXXXXXXXXXXX...................
.X..X.X.X.X.X.X.X..XXXX*X.X.X.X.X.X.X.XX.
.XXXX.X.X.X.X.X.X.XX.X.X.X.X.X.X.X.X.X.X.
.........................................
.XX.X.X.X.X.X.X.X.X.X.X.X.X.X.X.X.X.X.X.X
.X.X.X.X.X.X.X.X.X.X.X.X.X.X.X.X.X.X.X.X.
.........................................
X.X.X.X.X.X.X.X.X.X.X.X.X.X.X.X.X.X.X.XX.
.X.X.X.X.X.X.X.X.X.X.X.X.X.X.X.X.X.X.X.X.
.........................................
.XX.X.X.X.X.X.X.X.X.X.X.X.X.X.X.X.X.X.X.X
.X.X.X.X.X.X.X.X.X.X.X.X.X.X.X.X.X.X.X.X.
.........................................
X.X.X.X.X.X.X.X.X.X.X.X.X.X.X.X.X.X.X.XX.
.X.X.X.X.X.X.X.X.X.X.X.X.X.X.X.X.X.X.X.X.
.........................................
.XX.X.X.X.X.X.X.X.X.X.X.X.X.X.X.X.X.X.X.X
.X.X.X.X.X.X.X.X.X.X.X.X.X.X.X.X.X.X.X.X.
.........................................
X.X.X.X.X.X.X.X.X.X.X.X.X.X.X.X.X.X.X.XX.
.X.X.X.X.X.X.X.X.X.X.X.X.X.X.X.X.X.X.X.X.
.........................................
.XX.X.X.X.XX.X.XX.X.X.X.X.X.X.X.X.X.X.X.X
.X.X.X.X.X.XXX.X.X.X.X.X.X.X.X.X.X.X.X.X.
X........................................
X.X.X.X.X.X.X.X.X.X.X.X.X.X.X.X.X.X.X.XX.
.X.X.X.X.X.X.X.X.X.X.X.X.X.X.X.X.X.X.X.X.
.........................................
.X.XX.X.X.X.X.X.X.X.X.X.X.X.X.X.X.X.XX.XX
.X.X.X.X.X.X.X.X.X.X.X.X.X.X.X.X.X.X.XMX.
.X....................................X..
..X.X.X.X.X.X.X.X.X.X.X.X.X.X.X.X.X.X.XX.
.X.X.X.X.X.X.X.X.X.X.X.X.X.X.X.X.X.X.X.X.
.X...................................X...
.XX.X.X.X.X.X.X.X.X.X.X.X.X.X.XX.XX.XXXX.
.X.X.X.X.X.X.X.X.X.X.X.X.X.X.X.X.X.X.X.X.
.........................................
X.X.X.X.X.X.X.X.X.X.X.X.X.X.X.X.X.X.X.XX.
.X.X.X.X.X.X.X.X.X.X.X.X.X.X.X.X.X.X.X.X.
.........................................";
        }
    }
}
