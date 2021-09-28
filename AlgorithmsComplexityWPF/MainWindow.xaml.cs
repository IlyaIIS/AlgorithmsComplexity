using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using AlgorithmsComplexityLOGIC;

namespace AlgorithmsComplexityWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        const int N = 12;
        const int repeatNum = 5;
        int[] nums = Logic.GetRndNumbesList(N);
        Point[] points = new Point[N];

        public MainWindow()
        {
            InitializeComponent();

            Execute(14, N, Brushes.Blue, true);
        }

        void Execute(int funcNum, int N, Brush color, bool showAverage)
        {
            long[][] results = new long[repeatNum][];
            for (int i = 0; i < repeatNum; i++)
            {
                results[i] = Logic.GetExecutingTimeArray(funcNum, nums, N);
            }

            double xStep = (Width * 0.98) / N;
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

                points[i] = new Point(i * xStep, min);

                if (min > maxValue)
                    maxValue = min;
            }


            double yStep = Height / (maxValue * 1.2);
            Draw(points, color, 1, yStep);

            if (showAverage)
            {
                //Draw(Smooth(points, (int)(N / 10)), Brushes.Green, 3, yStep);
                points = Smooth1(points);
                Draw(points, Brushes.Red, 3, yStep);
            }
        }

        void Draw(Point[] points, Brush color, int thickness, double yStep )
        {
            for (int i = 1; i < points.Length; i++)
            {
                Field.Children.Add(new Line()
                {
                    X1 = points[i - 1].X,
                    X2 = points[i].X,
                    Y1 = -points[i - 1].Y * yStep + Height - 40,
                    Y2 = -points[i].Y * yStep + Height - 40,
                    StrokeThickness = thickness,
                    Stroke = color
                });
            }
        }

        static Point[] Smooth(Point[] results, int w)
        {
            Point[] smoothResults = new Point[results.Length];
            for (int i = 0; i < results.Length; i++)
            {
                double s = 0;
                int count = 0;
                for (int j = i - w; j <= i + w; j++)
                {
                    if (j > 0 && j < results.Length)
                    {
                        s += results[j].Y;
                        count++;
                    }
                }
                smoothResults[i].X = results[i].X;
                smoothResults[i].Y = s / count;
            }
            return smoothResults;
        }

        static Point[] Smooth1(Point[] points)
        {
            for(int i = 0; i < N; i++)
            {
                for(int j = 1; j < points.Length - 1; j++)
                {
                    points[j].Y = (points[j - 1].Y + points[j + 1].Y) / 2;
                }
                //points[^1].Y = (points[points.Length - 2].Y + points[^1].Y) / 2;
            }
            return points;
        }
    }
}
