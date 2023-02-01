using System;
using System.Collections.Generic;
using System.Linq;

namespace CodeTestHackerRank
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Quiz 1
            //string[] picture = { "bbba", "abba", "acaa", "aaac" };
            string[] picture = { "aabba", "aabba", "aaacb" };
            //string[] picture = { "aaaba", "ababa", "aaaca" };

            //string[] picture = { "bbba", "abba", "acaa", "aaac" };


            int numberOfFills = MinFills(picture);
            Console.WriteLine(numberOfFills);

            //----------------------------------------

            //Quiz 2
            //var kthFactor = KthFactor1(12, 3);
            var kthFactor = PthFactor1(10, 5);
            //var kthFactor = KthFactor(4, 4);
            Console.WriteLine(kthFactor);


            Console.ReadKey();
        }

        //The code above is a function in C# to calculate the number of strokes required to paint all the distinct colors in a given 2D string array "picture".
        //It first converts the input string array into a 2D char array "arr".
        //Then, it loops through all cells in the 2D array and for each unprocessed cell (that is not equal to '0'),
        //it calls the "FillColor" function with the current cell's character and indices as inputs.
        //The "FillColor" function then fills the same color starting from the current cell and marking all processed cells with '0'.
        //The outer loop then increments the totalColor count for each new color found.
        //Finally, the function returns the total number of colors, which represents the number of strokes required to paint all the distinct colors.
        static int StrokesRequired(string[] picture)
        {
            char[][] arr = new char[picture.Length][];
            int index = 0;

            foreach (var str in picture)
            {
                arr[index++] = str.ToCharArray();
            }

            int totalColor = 0;
            for (int i = 0; i < arr.Length; i++)
            {
                for (int j = 0; j < arr[i].Length; j++)
                {
                    if (arr[i][j] != '0')
                    {
                        var a = arr[i][j];
                        // '0' mark visited
                        FillColor(arr, arr[i][j], i, j);
                        totalColor++;
                    }
                }
            }

            return totalColor;
        }

        //The code above is a recursive function in C# to fill a color in a 2D char array.
        //It takes a 2D char array "arr", a character "c", and the starting indices "i" and "j" as input.
        //The function works by first checking if the current cell (arr[i][j]) is not equal to "c".
        //If it's not, the function returns without doing anything.
        //If it's equal, the function changes the value of the current cell to '0' to mark it as processed,
        //and then calls itself on all its unprocessed neighbors (top, bottom, left, and right) to continue the color fill.
        static void FillColor(char[][] arr, char c, int i, int j)
        {
            if (arr[i][j] != c)
            {
                return;
            }

            arr[i][j] = '0';

            if (i - 1 >= 0)
            {
                FillColor(arr, c, i - 1, j);
            }

            if (j - 1 >= 0)
            {
                FillColor(arr, c, i, j - 1);
            }

            if (i + 1 < arr.Length)
            {
                FillColor(arr, c, i + 1, j);
            }

            if (j + 1 < arr[i].Length)
            {
                FillColor(arr, c, i, j + 1);
            }
        }

        //In this solution, the input picture is first converted into a 2-dimensional character array grid.
        //Then, the main method MinFills iterates through each cell of the grid and, for each cell that has a color, it calls the Fill method to paint the connected cells of the same color.
        //The Fill method implements a breadth-first search algorithm to paint connected cells and mark them as '#' to avoid visiting them again.
        //The fillCount variable keeps track of the number of separate fill operations, which is the minimum number of fills needed to completely repaint the picture.
        static int MinFills(string[] picture)
        {
            int row = picture.Length;
            int col = picture[0].Length;
            int fillCount = 0;
            char[,] grid = new char[row, col];

            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    grid[i, j] = picture[i][j];
                }
            }

            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    if (grid[i, j] != '#')
                    {
                        fillCount++;
                        Fill(grid, i, j, row, col, grid[i, j]);
                    }
                }
            }

            return fillCount;
        }

        static void Fill(char[,] grid, int i, int j, int row, int col, char c)
        {
            if (i < 0 || i >= row || j < 0 || j >= col)
            {
                return;
            }

            if (grid[i, j] != c)
            {
                return;
            }

            grid[i, j] = '#';

            Fill(grid, i + 1, j, row, col, c);
            Fill(grid, i - 1, j, row, col, c);
            Fill(grid, i, j + 1, row, col, c);
            Fill(grid, i, j - 1, row, col, c);
        }
    


        //This optimization is based on the fact that
        //if i is a factor of n, then n / i is also a factor of n.By checking both i and n / i in each iteration,
        //the number of iterations can be reduced.Additionally, the loop only needs to go up to the square root of n, as the maximum possible factor of n is sqrt(n).
        static long PthFactor(long n, long p)
            {
                long count = 0;
                long max = (long)Math.Sqrt(n);
                for (long i = 1; i <= max; i++)
                {
                    if (n % i == 0)
                    {
                        count++;
                        if (count == p)
                        {
                            return i;
                        }
                        long j = n / i;
                        if (i != j && n % j == 0)
                        {
                            count++;
                            if (count == p)
                            {
                                return j;
                            }
                        }
                    }
                }
                return 0;
            }

        //This function calculates the pth factor of a positive integer n. The algorithm generates a list of factors of n and then returns the pth factor.
        //If the pth factor does not exist, the function returns 0.
        //The algorithm loops through the numbers from 1 to the square root of n and checks if each number is a factor of n.If it is, it adds it to the list of factors, lst1.
        //The corresponding other factor, j, is also computed and added to lst2 if it's not equal to the current factor.
        //If the pth factor is found while generating lst1, it is returned.
        //If not, the list lst2 is added to lst1 and the function checks if the pth factor exists in the combined list.If it does, it returns that factor,
        //otherwise, it returns 0.

        static long PthFactor1(long n, long p)
        {
            var lst1 = new List<long>();
            var lst2 = new List<long>();

            var count = 0;
            long result = 0;

            for (long i = 1; i * i < n; i++)
            {
                if (n % i == 0)
                {
                    lst1.Add(i);
                    count++;
                    if (count == p)
                    {
                        result = i;
                        break;
                    }

                    var j = n / i;

                    if (j != i)
                    {
                        lst2.Insert(0, j);
                    }
                }
            }

            if (result > 0)
            {
                return result;
            }
            else
            {
                lst2.AddRange(lst1);
                var countList = lst1.Count();

                if (countList > 0 && p <= countList)
                {
                    result = lst1.ToArray()[p - 1];
                }
                else
                {
                    result = 0;
                }
            }

            return result;
        }
    }
}
