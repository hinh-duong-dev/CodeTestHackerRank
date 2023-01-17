using System;
using System.Collections.Generic;

namespace CodeTestHackerRank
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Quiz 1
            //string[] picture = { "bbba", "abba", "acaa", "aaac" };
            //string[] picture = { "aabba", "aabba", "aaacb" };
            string[] picture = { "aaaba", "ababa", "aaaca" };


            int numberOfFills = StrokesRequired(picture);
            Console.WriteLine(numberOfFills);

            //----------------------------------------

            //Quiz 2
            var kthFactor = KthFactor(12, 3);
            //var kthFactor = KthFactor(7, 2);
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
                        Fill(arr, arr[i][j], i, j);
                        totalColor++;
                    }
                }
            }

            return totalColor;
        }

        static void Fill(char[][] arr, char c, int i, int j)
        {
            var a = arr[i][j];

            if (arr[i][j] != c)
            {
                return;
            }

            arr[i][j] = '0';

            if (i - 1 >= 0)
            {
                Fill(arr, c, i - 1, j);
            }

            if (j - 1 >= 0)
            {
                Fill(arr, c, i, j - 1);
            }

            if (i + 1 < arr.Length)
            {
                Fill(arr, c, i + 1, j);
            }

            if (j + 1 < arr[i].Length)
            {
                Fill(arr, c, i, j + 1);
            }
        }

        static long KthFactor(long n, long k)
        {
            //long count = 1;
            //for (long i = 1; i <= n; i++)
            //{
            //    if (n % i == 0)
            //    {
            //        if (count == k)
            //        {
            //            return i;
            //        }
            //        else
            //        {
            //            count++;
            //        }
            //    }
            //}

            //return -1;

            var listFactor = new List<long>();

            int factor;

            for (factor = 1; factor <= n / 2; factor++)
            {
                if (factor != 0 && n % factor == 0)
                {
                    listFactor.Add(factor);
                }
            }

            listFactor.Add(n);

            listFactor.Sort();

            if (k - 1 >= listFactor.Count)
            {
                return -1;
            }

            return listFactor.ToArray()[k - 1];
        }
    }
}
