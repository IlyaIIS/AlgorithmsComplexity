using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Drawing;
using System.Text;

namespace AlgorithmsComplexityLOGIC
{
    public static class Logic
    {
        delegate void Function(int[] array, int count);
        static Random rnd = new Random();
        public static long[] GetExecutingTimeArray(int funcNum, int[] array, int count, bool isParallelActive)
        {
            long[] result = new long[count];
            if (isParallelActive)
            {
                Parallel.For(1, count, (int i) => result[i] = GetTimeOfFunctionExecuting(functions[funcNum], array, i));
            }
            else
            {
                for (int i = 3; i < count; i++) //мин длина
                    result[i] = GetTimeOfFunctionExecuting(functions[funcNum], array, i);
            }
            return result;
        }

        static long GetTimeOfFunctionExecuting(Function function, int[] array, int count)
        {
            int[] nums = new int[count];
            Array.Copy(array, nums, count);

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
            //<номер> <название> - <приемлемое количество итераций>
            (int[] array, int count) =>
            {

            },
            //1 - 20000
            (int[] array, int count) =>
            {
                for (int i = 0; i < count; i++)
                {
                    int ii = 0;

                    ii++;
                    ii--;
                }
            },
            //2 - 20000
            (int[] array, int count) =>
            {
                long sum = 0;
                for (int i = 0; i < count; i++)
                {
                    sum += array[i];
                }
            },
            //3 - 20000
            (int[] array, int count) =>
            {
                long mult = 1;
                for (int i = 0; i < count; i++)
                {
                    mult *= array[i];
                }
            },
            //4 - 10000
            (int[] array, int count) =>
            {
                double sum = 0;
                for (int i = 0; i < count; i++)
                    sum += array[i] * Math.Pow(1.5, i - 1);
            },
            //5 сортировка пузырьком - 2000
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
            //6 быстрая сортировка - 6000
            (int[] array, int count) =>
            {
                if (count > 1)
                {
                    Qsort(array, 0, count - 1);
                }
            },
            //7 гибридный алгоритм сортировки - 2000
            (int[] array, int count) =>
            {
                TimSortPacket.TimSort(ref array, count);
            },
            //8 простой алгоритм возведение в степень - 20000
            (int[] array, int count) =>
            {
                Pow(array[count - 1],count);
            },
            //9 рекурсивный алгоритм возведения в степень - 20000
            (int[] array, int count) =>
            {
                long x = 0;
                int num = array[^1];
                x = RecPow(num,count);
            },
            //10 быстрый алгоритм возведения в степень - 20000
            (int[] array, int count) =>
            {
                int num = array[^1];
                long x = array[^1];
                long c = x;
                int k = count;

                x = k % 2 == 1 ? x : 1;

                do
                {
                    k = (int)(k/2);
                    c *= c;
                    x = k % 2 == 1 ? x*c : x;
                }while(k!=0);
            },
            //11 классический быстрый алгоритм возведения в степень - 20000
            (int[] array, int count) =>
            {
                long x = 1;
                long c = x;
                int k = count;
                while(k != 0)
                {
                    if (k % 2 == 1)
                    {
                        x *= c;
                        k--;
                    }else
                    {
                        c *= c;
                        k = (int)(k/2);
                    }
                }
            },
            //12 перемножение матриц - 200
            (int[] array, int count) =>
            {
                var rnd = new Random();
                var a = new int[count, count];
                var b = new int[count, count];
                var result = new int[count, count];
                for(int i = 0; i < count; i++)
                {
                    for(int j = 0; j < count; j++)
                    {
                        a[i, j] = rnd.Next(1000);
                        b[i, j] = rnd.Next(1000);
                    }
                }
                for(int i =0; i < count; i++)
                {
                    for(int k = 0; k < count; k++)
                    {
                        int s = 0;
                        for(int j = 0; j < count; j++)
                        {
                            s += a[i, j] * b[j, k];
                        }
                        result[i, k] = s;
                    }
                }
            },
            //13 Древовидный фрактал - 20
            (int[] array, int count) =>
            {
                DoFractal(new PointD(0,0),Math.PI/2,count);
            },
            //14 L-система - 12
            (int[] array, int count) =>
            {
                string condition1 = "LBFRAFARFBL";
                string condition2 = "RAFLBFBLFAR";
                string str = "A";
                for (int i = 0; i<count; i++)
                    str = LGenerator(str, condition1, condition2);
                str = DeleteConditions(str);
            },
            //15 богосорт
            (int[] array, int count) =>
            {
                BogoSort(array);
            }
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

        static long Pow(long num, int degree)
        {
            long x = 1;
            if (degree >= 0)
            {
                for (int i = 0; i < degree; i++)
                {
                    x *= num;
                }
            }
            else
            {
                throw new Exception("Возведение в отрицательную степень не реализовано");
            }

            return x;
        }

        // способ удаления пик через соседние значения в массиве из вычислений для одного count
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

        static long RecPow(long num, int degree)
        {
            long output = 1;
            if (degree != 0)
            {
                output = RecPow(num, (int)(degree / 2));
                output = num % 2 == 1 ? output * output * num : output * output;
            }
            return output;
        }

        static string LGenerator(string input, string condition1, string condition2)
        {
            StringBuilder output = new StringBuilder(String.Empty);
            foreach (var c in input)
            {
                if (c == 'A')
                {
                    output.Append(condition1);
                }
                else if (c == 'B')
                {
                    output.Append(condition2);
                }
                else
                    output.Append(c);
            }
            return output.ToString();
        }

        static string DeleteConditions(string input)
        {
            input.Replace("A", string.Empty);
            return input.Replace("B", string.Empty);
        }

        static void DoFractal(PointD point, double angle, int deph)
        {
            for (int i = 0; i < FS.branchNum; i++)
            {
                double localAngle = angle - FS.angleDigression + FS.angleInc * i;
                PointD localPoint = new PointD(point.X + Math.Cos(localAngle) *
                                    FS.length, point.Y - Math.Sin(localAngle) * FS.length);
                //сохранить полученный отрезок (от point до localPoint)
                if (deph > 0)
                {
                    DoFractal(localPoint, localAngle, deph - 1);
                }
            }
        }


        public static void BogoSort(int[] arr)
        {
            bool flag = true;
            while (flag)
            {
                arr = Shuffle(arr);
                for (int i = 1; i < arr.Length; i++)
                {
                    if (arr[i - 1] > arr[i])
                    {
                        flag = true;
                        break;
                    }
                    flag = false;
                }
            }
        }

        public static int[] Shuffle(int[] arr)
        {
            for (int i = 0; i < arr.Length; i++)
            {
                int j = rnd.Next(arr.Length);
                int temp = arr[i];
                arr[i] = arr[j];
                arr[j] = temp;
            }
            return arr;
        }
    }

    struct PointD
    {
        public double X;
        public double Y;
        public PointD(double x, double y)
        {
            X = x;
            Y = y;
        }
    }

    static class FS //FractalSettings
    {
        public static int branchNum = 2;
        public static double angleDigression = Math.PI / (branchNum + 1);
        public static double angleInc = Math.PI / (branchNum + 1);
        public static double length = 30;
    }

    class TimSortPacket
    {
        public const int Run = 32; //размер изначальной части массива (minrun)

        public static void InsertionSort(ref int[] arr, int left, int right)
        {
            for (int i = left + 1; i <= right; i++)
            {
                int temp = arr[i];
                int j = i - 1;
                while (j >= left && arr[j] > temp)
                {
                    arr[j + 1] = arr[j];
                    j--;
                }
                arr[j + 1] = temp;
            }
        }

        public static void Merge(ref int[] arr, int l, int m, int r)
        {
            int len1 = m - l + 1, len2 = r - m;
            int[] left = new int[len1];
            int[] right = new int[len2];
            for (int x = 0; x < len1; x++)
                left[x] = arr[l + x];
            for (int x = 0; x < len2; x++)
                right[x] = arr[m + 1 + x];

            int i = 0;
            int j = 0;
            int k = l;

            while (i < len1 && j < len2)
            {
                if (left[i] <= right[j])
                {
                    arr[k] = left[i];
                    i++;
                }
                else
                {
                    arr[k] = right[j];
                    j++;
                }
                k++;
            }

            while (i < len1)
            {
                arr[k] = left[i];
                k++;
                i++;
            }

            while (j < len2)
            {
                arr[k] = right[j];
                k++;
                j++;
            }
        }

        public static void TimSort(ref int[] arr, int n)
        {
            for (int i = 0; i < n; i += Run)
                InsertionSort(ref arr, i, Math.Min((i + Run - 1), (n - 1)));

            for (int size = Run; size < n; size = 2 * size)
            {
                for (int left = 0; left < n; left += 2 * size)
                {
                    int mid = left + size - 1;
                    int right = Math.Min((left + 2 * size - 1), (n - 1));

                    if (mid < right)
                        Merge(ref arr, left, mid, right);
                }
            }
        }
    }
}
