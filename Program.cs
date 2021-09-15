using System;

namespace AlgorithmsComplexity
{
    using System;
    using System.Timers;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;
    using System.Linq;
    using System.Diagnostics;

    namespace task1
    {
        class Program
        {
            const int N = 1000;
            const int repeatNum = 5;
            delegate void Function(int count);
            static Random rnd = new Random();
            static List<int> Numbers = GetRndNumbesList(N);
            static string Path = Directory.GetCurrentDirectory() + @"\results.csv";
            static void Main(string[] args)
            {

                File.Delete(Path);

                long[,] results = new long[N,5];

                for (int ii = 0; ii < repeatNum - 1; ii++)
                    for (int i = 0; i < N; i++)
                        results[i,ii] = GetTimeOfFunctionExecuting(Function6, i);
                for (int i = 0; i < N; i++)
                {
                    long medium = 0;
                    DeleteSurges(i,ref results);
                    for (int ii = 0; ii < 5; ii++)
                        medium += results[i, ii];
                    
                    WriteCSV(i, medium/5);
                }

                /*long[] results = new long[N];
                for (int i = 0; i < N; i++)
                    results[i] = GetTimeOfFunctionExecuting(Function3, i);
                for (int ii = 0; ii < repeatNum-1; ii++)
                    for (int i = 0; i < N; i++)
                        results[i] = Math.Min(results[i], GetTimeOfFunctionExecuting(Function3, i));
                for (int i = 0; i < N; i++)
                    WriteCSV(i, results[i]);*/

                /*for (int i = 1; i <= N; i++)
                {
                    long[] results = new long[5];
                    for (int j = 0; j < 5; j++)
                    {
                        results[j] = GetTimeOfFunctionExecuting(Function3, i);
                    }
                    DeleteSurges(ref results);
                    WriteCSV(i, results.Sum()/5);
                }*/

            }


            public static void WriteCSV(int n, long seconds)
            {
                string csv = n.ToString() + ";" + seconds.ToString() + "\n";

                File.AppendAllText(Path, csv);
            }

            static long GetTimeOfFunctionExecuting(Function function, int count)
            {
                Stopwatch stopWatch = new Stopwatch();
                stopWatch.Start();
                function(count);
                stopWatch.Stop();
                return stopWatch.Elapsed.Ticks * Stopwatch.Frequency / 1000000;
            }

            static void Function1(int count)
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
            }

            static void Function2(int count)
            {
                long sum = 0;
                for (int i = 0; i < count; i++)
                {
                    sum += Numbers[i];
                }
            }

            static void Function3(int count)
            {
                long mult = 1;
                for (int i = 0; i < count; i++)
                {
                    mult *= Numbers[i];
                }
            }
            static void Function4(int count)
            {
                double sum = 0;
                for (int i = 0; i < count; i++)
                    sum += Numbers[i] * Math.Pow(1.5, i - 1);
            }
            static void Function5(int count)
            {
                int[] subArray = new int[count];
                Numbers.CopyTo(0, subArray,0,count);
                

                for (int ii = 0; ii < count-1; ii++)
                    for (int i = 1; i < count; i++)
                        if (subArray[i-1] > subArray[i])
                        {
                            int localNum = subArray[i - 1];
                            subArray[i - 1] = subArray[i];
                            subArray[i] = localNum;
                        }
            }

            static void Function6(int count)
            {
                if (count > 1)
                {
                    int[] subArray = new int[count];
                    Numbers.CopyTo(0, subArray, 0, count);

                    Qsort(subArray, 0, count - 1);
                }
            }

            static int[] Qsort(int[] arr, int a, int b)
            {
                int lastB = b;
                for(int i = a; i < b; i++)
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
                if(lastB - b != 0)
                    arr = Qsort(arr, 0, b - 1);
                if(lastB - b > 1)
                    arr = Qsort(arr, b + 1, arr.Length - 1);
                return arr;
            }

            static List<int> AddRndElement(List<int> list)
            {
                list.Add(rnd.Next(int.MaxValue));
                return list;
            }

            static void DeleteSurges(ref long[] list)
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
            static void DeleteSurges(int pos, ref long[,] list)
            {
                for (int i = 0; i < 4; i++)
                {
                    if ((list[pos, i + 1] - list[pos, i]) / list[pos, i] > 3)
                        list[pos, i + 1] = list[pos, i];
                }

                if ((list[pos, 0] - list[pos, 1]) / list[pos, 1] > 3)
                    list[pos, 0] = list[pos, 1];
            }

            static List<int> GetRndNumbesList(int count)
            {
                List<int> output = new List<int>();
                for (int i = 0; i < count; i++)
                    output.Add(rnd.Next(int.MaxValue));

                return output;
            }
        }
    }
}
