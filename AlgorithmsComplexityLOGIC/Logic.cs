using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Drawing;

namespace AlgorithmsComplexityLOGIC
{
    public static class Logic
    {
        delegate void Function(int[] array, int count);
        static Random rnd = new Random();
        public static long[] GetExecutingTimeArray(int funcNum, int[] array, int count)
        {
            long[] result = new long[count];

            Parallel.For(0, count, (int i) => result[i] = GetTimeOfFunctionExecuting(functions[funcNum], array, i));

            //for (int i = 0; i < count; i++)
            //    result[i] = GetTimeOfFunctionExecuting(functions[funcNum], array, i);

            return result;
        }

        static long GetTimeOfFunctionExecuting(Function function,int[] array, int count)
        {
            int[] nums = new int[array.Length];
            array.CopyTo(nums, 0);

            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            function(nums, count);
            stopWatch.Stop();
            return stopWatch.Elapsed.Ticks * Stopwatch.Frequency / 1000000;
        }
        public static int[] GetRndNumbesList(int count)
        {
            int[] output = new int[count];
            for (int i = 0; i < count; i++)
                output[i] = rnd.Next(int.MaxValue);

            return output;
        }

        static List<Function> functions = new List<Function>()
        {
            (int[] array, int count) =>
            {

            },
            //0
            (int[] array, int count) =>
            {
                for (int i = 0; i < count; i++)
                {
                    int ii = 0;
                    if (i * 2 * 2 * 2 * 2 * 2 == -1)
                    {
                        ii++;
                        ii--;
                    }
                }
            },
            //1
            (int[] array, int count) =>
            {
                long sum = 0;
                for (int i = 0; i < count; i++)
                {
                    sum += array[i];
                }
            },
            //2
            (int[] array, int count) =>
            {
                long mult = 1;
                for (int i = 0; i < count; i++)
                {
                    mult *= array[i];
                }
            },
            //3
            (int[] array, int count) =>
            {
                double sum = 0;
                for (int i = 0; i < count; i++)
                    sum += array[i] * Math.Pow(1.5, i - 1);
            },
            //4
            (int[] array, int count) =>
            {
                for (int ii = 0; ii < count - 1; ii++)
                    for (int i = 1; i < count; i++)
                        if (array[i - 1] > array[i])
                        {
                            int localNum = array[i - 1];
                            array[i - 1] = array[i];
                            array[i] = localNum;
                        }
            },
            //5
            (int[] array, int count) =>
            {
                if (count > 1)
                {
                    Qsort(array, 0, count - 1);
                }
            },
            //6
            (int[] array, int count) =>
            {

            },
        };

        static int[] Qsort(int[] arr, int a, int b)
        {
            int lastB = b;
            for (int i = a; i < b; i++)
            {
                if (arr[i] > arr[b])
                {
                    int temp = arr[b];
                    arr[b] = arr[i];
                    arr[i] = arr[b - 1];
                    arr[b - 1] = temp;
                    b--;
                    i--;
                }
            }
            if (lastB - b != 0)
                arr = Qsort(arr, 0, b - 1);
            if (lastB - b > 1)
                arr = Qsort(arr, b + 1, arr.Length - 1);
            return arr;
        }

        public static void DeleteSurges(ref long[] list)
        {
            for (int i = 0; i < list.Length - 1; i++)
            {
                if ((list[i + 1] - list[i]) / list[i] > 3)
                    list[i + 1] = list[i];
            }

            int ii = list.Length - 1;
            if ((list[0] - list[1]) / list[1] > 3)
                list[0] = list[1];
        }
        public static void DeleteSurges(int pos, ref long[][] list)
        {
            for (int i = 0; i < 4; i++)
            {
                if ((list[i + 1][pos] - list[i][pos]) / list[i][pos] > 3)
                    list[i + 1][pos] = list[i][pos];
            }

            if ((list[0][pos] - list[1][pos]) / list[1][pos] > 3)
                list[0][pos] = list[1][pos];
        }
    }
}
