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
using System.Timers;

namespace Unusual_Timer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MyTimer _timer;
        public int MyProperty { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            _timer = new MyTimer();
            _timer.Duration = new TimeSpan(0, 0, 18);
            timerContainer.DataContext = _timer;
        }

        private void Start_Click(object sender, RoutedEventArgs e)
        {
            _timer.Start();
        }

    }
}
