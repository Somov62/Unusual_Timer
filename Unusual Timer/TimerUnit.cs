using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Unusual_Timer
{
    public class TimerUnit : INotifyPropertyChanged
    {
        private double _bindableTime;
        private bool _isEnabled;
        private double _duration;
        private Timer _timer;

        public TimerUnit(double time = 1)
        {
            if (time < 1) time = 1;
            Time = time;
        }

        public double Duration
        {
            get => _duration;
            set
            {
                _duration = value;
                this.Reset();
            }
        }

        public double Time
        {
            get => _bindableTime;
            set
            {
                if (value < 0) value = 0;
                _bindableTime = value;
                OnPropertyChanged("Time");
            }
        }
        public bool IsEnabled
        {
            get => _isEnabled;
            set
            {
                _isEnabled= value;
                OnPropertyChanged("IsEnabled");
            }
        }

        public void Start()
        {
            if (_isEnabled)
            {
                throw new Exception();
            }
            Task.Run(StartTimer);
        }
        private double progress;
        private double millisecondIncrease;

        private void StartTimer()
        {
            millisecondIncrease = 1 / Duration;
            progress = 0;
            _isEnabled = true;
            _timer = new Timer();
            _timer.Interval = 200;
            _timer.Elapsed += Timer_Elapsed;
            _timer.Start();
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (progress >= Duration)
            {
                this.Stop();
                return;
            }
            progress += _timer.Interval;

            this.Time -= millisecondIncrease * _timer.Interval;
        }

        public void Stop()
        {
            if (!_isEnabled) return;
            _isEnabled = false;
            _timer.Stop();
        }

        public void Reset()
        {
            this.Stop();
            Time = 1;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
