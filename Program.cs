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
            delegate void Function(int count);
            static Random rnd = new Random();
            const int N = 20000;
            static string Path = Directory.GetCurrentDirectory() + @"\results.csv";
            static void Main(string[] args)
            {
                File.Delete(Path);
                for (int i = 1; i <= N; i++)
                {
                    long[] results = new long[5];
                    for (int j = 0; j < 5; j++)
                    {
                        results[j] = GetTimeOfFunctionExecuting(Function1, i);
                    }
                    WriteCSV(i, results.Sum() / 5);
                }
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
                for (int i = 1; i <= count; i++)
                {
                    sum += i;
                }
            }

            static void Function3(int count)
            {
                long mult = 1;
                for (int i = 1; i <= count; i++)
                {
                    mult *= i;
                }
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
                    if ((list[i] - list[i + 1]) / list[i + 1] > 3)
                        list[i + 1] = list[i];
                }

                int ii = list.Length - 1;
                if ((list[ii + 1] - list[ii]) / list[ii] > 3)
                    list[ii] = list[ii + 1];
            }
        }
    }
}
