using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Drawing;
using System.Text;
using System.Linq;

namespace AlgorithmsComplexityLOGIC
{
    public static class Logic
    {
        public static Node RBT = new Node(0, "white");
        delegate void Function(int[] array, int count);
        static Random rnd = new Random();
        public static long[] GetExecutingTimeArray(int funcNum, int[] array, int count, bool isParallelActive)
        {
            long[] result = new long[count];
            if (isParallelActive)
            {
                Parallel.For(1, count, (int i) => result[i] = GetTimeOfFunctionExecuting(newFunctions[funcNum], array, i));
            }
            else
            {
                for (int i = 3; i < count; i++) //мин длина
                    result[i] = GetTimeOfFunctionExecuting(newFunctions[funcNum], array, i);
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
            
        };

        static List<Function> newFunctions = new List<Function>()
        {
            //<номер> <название> - <приемлемое количество итераций>
            (int[] array, int count) =>
            {

            },
            //1 богосорт
            (int[] array, int count) =>
            {
                BogoSort(array);
            },
            //2 Алгоритм Хельда – Карпа - 13
            (int[] array, int count) =>
            {
                int[,] matrix = HeldKarpAlgorithmPacket.GenerateMatrix(count);//при финальном замере вынести формирование матрицы наружу!!
                HeldKarpAlgorithmPacket.GetMinCostRoute(matrix, count);
            },
            //3 <Red-black tree, insert> - 10000
            (int[] array, int count) =>
            {
                for(int i = 0; i < array.Length; i++)
                {
                    Insert(array[i]);
                }
            },
            //4 GnomeSort - 2000
            (int[] array, int count) =>
            {
                int position = 0;
                while(position < array.Length)
                {
                    if(position == 0 || array[position] >= array[position - 1])
                    {
                        position++;
                    }
                    else
                    {
                        int temp = array[position];
                        array[position] = array[position - 1];
                        array[position - 1] = temp;
                        position--;
                    }
                }
            },
            //5 расстояние Хэминга
            (int[] array, int count) =>
            {
                for(int i = 0; i < array.Length; i++)
                    HammingDistance();
            },
            //6 битонная сортировка
            (int[] array, int count) =>
            {
                List<int> list = new List<int>();//при финальном замере вынести формирование листа наружу!!
                int i = 1;                      
                while(count > Math.Pow(2,i))
                {
                    i++;
                }
                for (int ii = 0; ii < Math.Pow(2,i); ii++)
                {
                    if (ii < count)
                        list.Add(array[ii]);
                    else
                        list.Add(0);
			    }

                BitonicSortPacket.Sort(list.ToArray(), 1);
            },
        };

        static void Insert(int key)
        {
            Node node = new Node(key, "red", null, null);
            if (RBT.Parent == null)
            {
                node.Color = "black";
                RBT = node;
                return;
            }
            var parentNode = SearchNull(RBT, key);
            if (parentNode.Key <= key)
                parentNode.Left = node;
            else
                parentNode.Right = node;
            node.Parent = parentNode;
            FixColor(node);
        }

        static void FixColor(Node node)
        {
            while (node != RBT && node.Parent.Color == "red")
            {
                if (node.Parent == node.Parent.Parent.Left)
                {
                    Node Y = node.Parent.Parent.Right;
                    if (Y != null && Y.Color == "red")
                    {
                        Y.Color = "black";
                        node = FlipColors(node);
                        node = node.Parent.Parent;
                    }
                    else
                    {
                        if (node == node.Parent.Right)
                        {
                            node = node.Parent;
                            RotateLeft(node);
                        }
                        node = FlipColors(node);
                        RotateRight(node.Parent.Parent);
                    }

                }
                else
                {
                    Node X = null;

                    X = node.Parent.Parent.Left;
                    if (X != null && X.Color == "black")//Case 1
                    {
                        X.Color = "red";
                        node = FlipColors(node);
                        node = node.Parent.Parent;
                    }
                    else
                    {
                        if (node == node.Parent.Left)
                        {
                            node = node.Parent;
                            RotateRight(node);
                        }
                        node = FlipColors(node);
                        RotateLeft(node.Parent.Parent);

                    }

                }
                RBT.Color = "black";
            }
        }

        static Node SearchNull(Node node, int key)
        {
            if(node != null)
            {
                if (node.Key <= key && node.Left != null)
                {
                    SearchNull(node.Left, key);
                }
                else if(node.Right != null)
                {
                    SearchNull(node.Right, key);
                }
            }
            return node;
        }

        static Node FlipColors(Node n)
        {
            if (n.Parent.Color == "red")
                n.Parent.Color = "black";
            else
                n.Parent.Color = "red";
            if (n.Parent.Parent.Color == "red")
                n.Parent.Parent.Color = "black";
            else
                n.Parent.Parent.Color = "red";
            return n;
        }

        static Node RotateLeft(Node n)
        {
            var x = n.Right;
            n.Right = x.Left;
            x.Left = n;
            x.Color = n.Color;
            n.Color = "red";
            return x;
        }

        static Node RotateRight(Node n)
        {
            var x = n.Left;
            n.Left = x.Right;
            x.Right = n;
            x.Color = n.Color;
            n.Color = "red";
            return x;
        }

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

        public static void HammingDistance()
        {
            string str1 = rnd.Next(100000, 999999).ToString();
            string str2 = rnd.Next(100000, 999999).ToString();
            int count = 0;
            for(int i = 0; i < str1.Length; i++)
            {
                if (str1[i] != str2[i])
                    count++;
            }
        }
        public static void Swap<T>(ref T l, ref T r)
        {
            T temp = l;
            l = r;
            r = temp;
        }

        
    }

    class RouteNode
    {
        public int Value { get; set; }
        public RouteNode[] ChildNodes { get; set; }
        public bool Selected { get; set; }
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

    public class Node // класс для представления узлов дерева
    {
        public int Key;
        public string Color = "black";
        public Node Left = null;
        public Node Right = null;
        public Node Parent = null;
        public Node(int key, string color, Node left, Node right)
        {
            Key = key;
            Color = color;
            Left = left;
            Right = right;
        }
        public Node(int key, string color)
        {
            Key = key;
            Color = color;
        }
    };

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
    class BitonicSortPacket
    {
        /// <param name="dir"> Направление сортировки (1 - возрастание, 0 - убывание)</param>
        public static void Sort(int[] arr, int dir)
        {
            BitonicSort(arr, 0, arr.Length, dir);
        }
        /// <summary> Рекурсивно сортирует битоническую последовательность </summary>
        /// <param name="from"> Начало последовательности для сортировки </param>
        /// <param name="count"> Количество элементов для сортировки </param>
        /// <param name="dir"> Направление сортировки (1 - возрастание, 0 - убывание) </param>
        static void BitonicSort(int[] arr, int from, int count, int dir)
        {
            if (count > 1)
            {
                BitonicSort(arr, from, count / 2, 1);            
                BitonicSort(arr, from + count / 2, count / 2, 0);
                BitonicMerge(arr, from, count, dir);             
            }
        }

        static void BitonicMerge(int[] arr, int from, int count, int dir)
        {
            if (count > 1)
            {
                for (int i = from; i < from + count / 2; i++)
                    CompAndSwap(arr, i, i + count / 2, dir);
                BitonicMerge(arr, from, count / 2, dir);
                BitonicMerge(arr, from + count / 2, count / 2, dir);
            }
        }

        static void CompAndSwap(int[] arr, int i, int j, int dir)
        {
            int k = (arr[i] > arr[j]) ? 1 : 0;

            if (dir == k)
                Logic.Swap(ref arr[i], ref arr[j]);
        }
    }
    static class HeldKarpAlgorithmPacket
    {
        static Random rnd = new Random();
        public static int GetMinCostRoute(int[,] matrix, int count)
        {
            List<int> way = Enumerable.Range(0, count).ToList();
            List<int> subList = new List<int>(way);
            subList.RemoveAt(0);
            int cost = GetMinCostRouteRec(matrix, way[0], subList);

            return cost;
        }
        static int GetMinCostRouteRec(int[,] matrix, int num, List<int> way)
        {
            if (way.Count > 0)
            {
                int min = int.MaxValue;
                for (int i = 0; i < way.Count; i++)
                {
                    List<int> subList = new List<int>(way);
                    subList.RemoveAt(i);
                    int localCost = matrix[num, way[i]] + GetMinCostRouteRec(matrix, way[i], subList);
                    if (localCost < min)
                        min = localCost;
                }
                return min;
            }
            else
            {
                return matrix[num, 0];
            }
        }
        public static int[,] GenerateMatrix(int n)
        {
            int[,] matrix = new int[n, n];
            for (int row = 0; row < n; row++)
            {
                for (int column = 0; column < n; column++)
                {
                    if (row != column)
                        matrix[row, column] = rnd.Next();
                }
            }
            return matrix;
        }
    }
}
