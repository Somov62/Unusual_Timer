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
        private Timer _timer;
        private double _bindableTime;
        private double _duration;
        private bool _isEnabled;
        private double _progress;
        private double _millisecondIncrease;

        public TimerUnit(double duration)
        {
            Time = 1;
            _timer = new Timer(20);
            _timer.Elapsed += Timer_Elapsed;
            _duration = duration;
            Reset();
        }

        #region Properties
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
                if (value > 1) value = 1;
                _bindableTime = value;
                OnPropertyChanged("Time");
            }
        }
        public bool IsEnabled
        {
            get => _isEnabled;
            set
            {
                _isEnabled = value;
                OnPropertyChanged("IsEnabled");
            }
        }
        #endregion

        public void Start()
        {
            if (_isEnabled)
            {
                throw new Exception();
            }
            Task.Run(StartTimer);
        }

        private void StartTimer()
        {
            IsEnabled = true;
            /*if (_progress == 0) */Timer_Elapsed(null, null);
            _timer.Start();
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (_progress >= Duration)
            {
                this.Stop();
                return;
            }
            _progress += _timer.Interval;

            this.Time -= _millisecondIncrease * _timer.Interval;
        }

        public void Stop()
        {
            if (!_isEnabled) return;
            IsEnabled = false;
            _timer.Stop();
        }

        public void Reset()
        {
            this.Stop();
            Time = 1;
            _progress = 0;
            _millisecondIncrease = 1 / Duration;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
