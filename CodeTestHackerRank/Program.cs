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

            //string[] picture = { "bbba", "abba", "acaa", "aaac" };


            //int numberOfFills = StrokesRequired(picture);
            //Console.WriteLine(numberOfFills);

            //----------------------------------------

            //Quiz 2
            //var kthFactor = KthFactor1(12, 3);
            var kthFactor = KthFactor2(10, 5);
            //var kthFactor = KthFactor(4, 4);
            Console.WriteLine(kthFactor);


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
            for (long i = 1; i * i < n; i++)
            {
                if (n % i == 0)
                {
                    k--;
                    if (k == 0) return i;
                }
            }

            for (long i = (long)Math.Sqrt(n); i >= 1; i--)
            {
                if (n % i == 0)
                {
                    k--;
                    if (k == 0) return n / i;             
                }
            }

            return -0;
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

        static long KthFactor1(long n, long p)
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
                    result= 0;
                }
            }

            return result;
        }

        static long KthFactor2(long n, long p)
        {
            var factors = new List<long>();

            for (long factor = 1; factor <= (long)Math.Sqrt(n); factor++) 
            {
                if (n % factor == 0)
                {
                    factors.Add(factor);
                    if (factor != n / factor) 
                        factors.Add(n / factor);
                }
            }

            factors.Sort();

            if (p-1 < factors.Count)
            {
                return factors.ToArray()[p - 1];
            }

            return 0;
        }
    }
}
