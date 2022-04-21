using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace Unusual_Timer
{
    public class Timer
    {
        private int _countUnitsInRow;
        private List<TimerUnit> _listTimerUnits;
        private TimerUnit[,] _units;
        private bool _isEnabled;
        private TimeSpan _duration;
        private DispatcherTimer _timer;

        public Timer()
        {
            CountUnitsInRow = 5;
        }


        public int CountUnitsInRow
        {
            get => _countUnitsInRow;
            set
            {
                _countUnitsInRow = value;
                this.Reset();
            }
        }

        public List<TimerUnit> ListTimerUnits 
        { 
            get => _listTimerUnits; 
            private set => _listTimerUnits = value; 
        }
        public TimeSpan Duration
        {
            get => _duration;
            set
            {
                _duration = value;
                this.Reset();
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
        private double unitDuration;
        public void Timer_Tick(object sender, EventArgs e)
        {
            if (progress >= Duration.TotalMilliseconds) this.Stop();
            progress += _timer.Interval.TotalMilliseconds;
            for (int j = _units.GetLength(1) - 1; j > -1; j--)
            {
                if (_units[0, j].Time > 0)
                {
                    try
                    {
                        _units[0, j].Time -= unitDuration / (millisecondIncrease * _timer.Interval.TotalMilliseconds);
                    }
                    catch 
                    {
                    }
                    break;
                }
            }

        }
        private void StartTimer()
        {
            unitDuration = Duration.TotalMilliseconds / CountUnitsInRow * 2 - 1;
            millisecondIncrease = 1 / unitDuration;

            _isEnabled = true;
            _timer = new DispatcherTimer();
            _timer.Interval = new TimeSpan(0, 0, 0, 0, 5);
            _timer.Tick += (object sender, EventArgs e) => 
            {
                if (progress >= Duration.TotalMilliseconds) this.Stop();
                progress += _timer.Interval.TotalMilliseconds;
                for (int j = _units.GetLength(1) - 1; j > -1; j--)
                {
                    if (_units[0, j].Time > 0)
                    {
                        try
                        {
                            _units[0, j].Time -= unitDuration / (millisecondIncrease * _timer.Interval.TotalMilliseconds);
                        }
                        catch
                        {
                        }
                        break;
                    }
                }
            };
            _timer.IsEnabled = true;
            _timer.Start();
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

            _units = new TimerUnit[CountUnitsInRow, CountUnitsInRow];

            for (int i = 0; i < _units.GetLength(0); i++)
            {
                for (int j = 0; j < _units.GetLength(1); j++)
                {
                    _units[i, j] = new TimerUnit();
                }
            }

            ListTimerUnits = new();
            foreach (var item in _units)
            {
                ListTimerUnits.Add(item);
            }
        }
    }
}
