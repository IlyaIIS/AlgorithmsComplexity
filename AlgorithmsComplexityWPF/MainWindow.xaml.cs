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
        const int N = 1000;
        const int repeatNum = 5;
        int[] nums = Logic.GetRndNumbesList(N);
        Point[] points = new Point[N];

        public MainWindow()
        {
            InitializeComponent();


            long[][] results = new long[repeatNum][];
            for (int i = 0; i < repeatNum; i++)
            {
                results[i] = Logic.GetExecutingTimeArray(5, nums, N);
            }

            double xStep = Width / N;
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

            double yStep = Height / (maxValue*1.2);
            for (int i = 1; i < points.Length; i++)
            {
                Field.Children.Add(new Line()
                {
                    X1 = points[i - 1].X,
                    X2 = points[i].X,
                    Y1 = -points[i - 1].Y*yStep + Height - 40,
                    Y2 = -points[i].Y * yStep + Height - 40,
                    StrokeThickness = 1,
                    Stroke = Brushes.Black
                }); 
            }

        }
    }
}
