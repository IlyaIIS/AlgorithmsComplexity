using AlgorithmsComplexityLOGIC;
using System;
using System.Drawing;
using System.IO;
using System.Text;

namespace AlgorithmsComplexityEXCEL
{
    class Program
    {
        static string Path = Directory.GetCurrentDirectory() + @"\results.csv";
<<<<<<< Updated upstream
        const int N = 20000;                     //размер графика по оси Х 
        const int repeatNum = 5;                 //количество повторений вычислений для среза выбросов 
=======
        const int N = 13;                     //размер графика по оси Х 
        const int repeatNum = 5;                //количество повторений вычислений для среза выбросов 
>>>>>>> Stashed changes
        static int[] nums = Logic.GetRndNumbesList(N); //массив случайных чисел 
        static long[] resultX = new long[N];           //массив точек для отрисовки графика 
        static StringBuilder csv = new StringBuilder();
        static void Main(string[] args)
        {
<<<<<<< Updated upstream
            Execute(1, N, true);
=======
            Execute(14, N, true);
>>>>>>> Stashed changes

            File.Delete(Path);
            for (int i = 0; i < N; i++)
            {
                AddCSVLine(i+1, resultX[i]);
            }
            File.AppendAllText(Path, csv.ToString());
            Console.WriteLine("ГОТОВО!");
        }

        static void Execute(int funcNum, int N, bool showAverage)
        {
            long[][] results = new long[repeatNum][];
            for (int i = 0; i < repeatNum; i++)
            {
                Console.SetCursorPosition(0, 0);
                Console.WriteLine(i * 20 + "%");
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

        public static void AddCSVLine(int n, long seconds)
        {
            csv.Append(n.ToString() + ";" + seconds.ToString() + "\n");
        }
    }
}
