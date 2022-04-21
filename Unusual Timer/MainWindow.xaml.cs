using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace Unusual_Timer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Timer _timer;
        public int MyProperty { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            _timer = new Timer();
            _timer.Duration = new TimeSpan(0, 0, 18);
            timerContainer.DataContext = _timer;
        }

        private void Start_Click(object sender, RoutedEventArgs e)
        {
            //Task.Run(To10000);
            _timer.Start();
        }
        private void To10000()
        {

            for (int i = 0; i < 5; i++)
            {

                this.Dispatcher.Invoke(() =>
                {
                    var a = timerContainer.DataContext as Timer;
                    a.ListTimerUnits[0].Time -= 0.2;
                });
                Thread.Sleep(500);
            }
            
        }

        private void Border_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            RectangleGeometry rg = new RectangleGeometry(new Rect(0, 0, e.NewSize.Width, e.NewSize.Height) { });
            var border = sender as Border;
            Grid grid = border.Child as Grid;
            grid.Clip = rg;
        }
    }
}
