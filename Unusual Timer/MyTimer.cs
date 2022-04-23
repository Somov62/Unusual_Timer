using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Unusual_Timer
{
    public class MyTimer
    {
        private int _countUnitsInRow;
        private TimerUnit[,] _units;
        private bool _isEnabled;
        private TimeSpan _duration;
        private Timer _timer;

        public MyTimer()
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

        public List<TimerUnit> ListTimerUnits { get; private set; }

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
        private double unitDuration;

        private void StartTimer()
        {
            unitDuration = Duration.TotalMilliseconds / (CountUnitsInRow * 2 - 1);

            _isEnabled = true;
            _timer = new Timer();
            _timer.Interval = 1;
            _timer.Elapsed += Timer_Elapsed;
            _timer.Start();
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            _timer.Interval = unitDuration + 500;
            if (progress >= Duration.TotalMilliseconds)
            {
                this.Stop();
                return;
            }
            progress += _timer.Interval;
            if (_units[0, 0].Time > 0)
            {
                for (int j = _units.GetLength(1) - 1; j >= 0; j--)
                {
                    if (_units[0, j].Time == 0) continue;

                    try
                    {
                        for (int i = 0; i < _units.Length; i++)
                        {
                            _units[i, j + i].Duration = unitDuration;
                            _units[i, j + i].Start();
                        }
                    }
                    catch { }
                    break;
                }
                return;
            }

            for (int i = 1; i < _units.GetLength(0); i++)
            {
                if (_units[i, 0].Time == 0) continue;

                try
                {
                    for (int j = 0; j < _units.Length; j++)
                    {
                        _units[i + j, j].Duration = unitDuration;
                        _units[i + j, j].Start();
                    }
                }
                catch { }
                break;
            }
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
