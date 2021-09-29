using AlgorithmsComplexityLOGIC;
using System;
using System.Drawing;
using System.IO;

namespace AlgorithmsComplexityEXCEL
{
    class Program
    {
        static string Path = Directory.GetCurrentDirectory() + @"\results.csv";
        const int N = 100;                     //размер графика по оси Х 
        const int repeatNum = 5;                //количество повторений вычислений для среза выбросов 
        static int[] nums = Logic.GetRndNumbesList(N); //массив случайных чисел 
        static long[] resultX = new long[N];          //массив точек для отрисовки графика 
        static void Main(string[] args)
        {
            Execute(3, N, true);

            File.Delete(Path);
            for (int i = 1; i <= N; i++)
            {
                WriteCSV(i, resultX[i - 1]);
            }
            Console.WriteLine("ГОТОВО!");
        }

        static void Execute(int funcNum, int N, bool showAverage)
        {
            long[][] results = new long[repeatNum][];
            for (int i = 0; i < repeatNum; i++)
            {
                results[i] = Logic.GetExecutingTimeArray(funcNum, nums, N, true);
            }

            double maxValue = 0;
            for (int i = 0; i < N; i++)
            {
                //long medium = 0; 
                long min = results[0][i];
                //Logic.DeleteSurges(i, ref results); 
                for (int ii = 0; ii < 5; ii++)
                {
                    //medium += results[ii][i]; 

                    if (results[ii][i] < min)
                        min = results[ii][i];
                }

                resultX[i] = min;

                if (min > maxValue)
                    maxValue = min;
            }
        }

        public static void WriteCSV(int n, long seconds)
        {
            string csv = n.ToString() + ";" + seconds.ToString() + "\n";

            File.AppendAllText(Path, csv);
        }
    }
}
