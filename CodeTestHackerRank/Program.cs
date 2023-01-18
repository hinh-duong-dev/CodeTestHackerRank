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
            //string[] picture = { "aabba", "aabba", "aaacb" };
            //string[] picture = { "aaaba", "ababa", "aaaca" };

            string[] picture = { "bbba", "abba", "acaa", "aaac" };


            int numberOfFills = StrokesRequired(picture);
            Console.WriteLine(numberOfFills);

            //----------------------------------------

            //Quiz 2
            //var kthFactor = KthFactor(12, 3);
            //var kthFactor = KthFactor(7, 2);
            //var kthFactor = KthFactor(4, 4);
            // Console.WriteLine(kthFactor);


            //var h = 4;
            var pictures = new[] { "bbba", "abba", "acaa", "aaac" };

            var pictureArrs = pictures.Select(x => x.ToList()).ToList();

            var strokeList = new List<List<(int, int)>>();

            // find list of items with their nearby items has same value 
            for (var row = 0; row < pictureArrs.Count; row++)
            {
                for (var col = 0; col < pictureArrs[row].Count; col++)
                {
                    // if item is not in any nearby item list
                    if (strokeList.FirstOrDefault(x => x.Any(p =>
                            p == (row, col) && pictureArrs[p.Item1][p.Item2] == pictureArrs[row][col])) is null)
                    {
                        var nearbyItems = new List<(int, int)>();

                        FindNearBy(pictureArrs, nearbyItems, (row, col));

                        strokeList.Add(nearbyItems);
                    }
                }
            }

            Console.WriteLine(strokeList.Count);

            Console.ReadKey();
        }

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

        static void FillColor(char[][] arr, char c, int i, int j)
        {
            var a = arr[i][j];

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

        static long KthFactor(long n, long k)
        {
            long count = 1;
            for (long i = 1; i <= n; i++)
            {
                if (i != 0 && n % i == 0)
                {
                    if (count == k)
                    {
                        return i;
                    }
                    else
                    {
                        count++;
                    }
                }
            }

            return -1;

            //var listFactor = new List<long>();

            //int factor;

            //for (factor = 1; factor <= n / 2; factor++)
            //{
            //    if (factor != 0 && n % factor == 0)
            //    {
            //        listFactor.Add(factor);
            //    }
            //}

            //listFactor.Add(n);

            //listFactor.Sort();

            //if (k - 1 >= listFactor.Count)
            //{
            //    return -1;
            //}

            //return listFactor.ToArray()[k - 1];
        }


        private static void FindNearBy(List<List<char>> pictureArrs, List<(int, int)> nearByItems, (int, int) currentPoint)
        {
            nearByItems.Add(currentPoint);

            if (currentPoint.Item1 != 0 && pictureArrs[currentPoint.Item1][currentPoint.Item2] ==
                pictureArrs[currentPoint.Item1 - 1][currentPoint.Item2] &&
                !nearByItems.Contains((currentPoint.Item1 - 1, currentPoint.Item2)))
            {
                FindNearBy(pictureArrs, nearByItems, (currentPoint.Item1 - 1, currentPoint.Item2));
            }

            if (currentPoint.Item1 < pictureArrs.Count - 1 && pictureArrs[currentPoint.Item1][currentPoint.Item2] ==
                pictureArrs[currentPoint.Item1 + 1][currentPoint.Item2] &&
                !nearByItems.Contains((currentPoint.Item1 + 1, currentPoint.Item2)))
            {
                FindNearBy(pictureArrs, nearByItems, (currentPoint.Item1 + 1, currentPoint.Item2));
            }

            if (currentPoint.Item2 != 0 && pictureArrs[currentPoint.Item1][currentPoint.Item2] ==
                pictureArrs[currentPoint.Item1][currentPoint.Item2 - 1] &&
                !nearByItems.Contains((currentPoint.Item1, currentPoint.Item2 - 1)))
            {
                FindNearBy(pictureArrs, nearByItems, (currentPoint.Item1, currentPoint.Item2 - 1));
            }

            if (currentPoint.Item2 < pictureArrs[currentPoint.Item1].Count - 1 &&
                pictureArrs[currentPoint.Item1][currentPoint.Item2] ==
                pictureArrs[currentPoint.Item1][currentPoint.Item2 + 1] &&
                !nearByItems.Contains((currentPoint.Item1, currentPoint.Item2 + 1)))
            {
                FindNearBy(pictureArrs, nearByItems, (currentPoint.Item1, currentPoint.Item2 + 1));
            }
        }
    }
}
