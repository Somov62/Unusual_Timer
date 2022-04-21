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
        private List<TimerUnit> _listTimerUnits;
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

        private void StartTimer()
        {
            unitDuration = Duration.TotalMilliseconds / (CountUnitsInRow * 2 - 1);
            millisecondIncrease = 1 / unitDuration;

            _isEnabled = true;
            _timer = new Timer();
            _timer.Interval = 100;
            _timer.Elapsed += _timer_Elapsed;
            _timer.Start();
        }

        private void _timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (progress >= Duration.TotalMilliseconds) this.Stop();
            progress += _timer.Interval;
            if (_units[0, 0].Time > 0)
            {

                for (int j = _units.GetLength(1) - 1; j > -1; j--)
                {
                    if (_units[0, j].Time > 0)
                    {
                        try
                        {
                            int j1 = j;
                            for (int i = j; i < _units.GetLength(0); i++)
                            {
                                _units[i, j1].Time -= millisecondIncrease * _timer.Interval;
                                if (_units[i, j1].Time < 0) _units[i, j1].Time = 0;
                                j1++;
                            }
                        }
                        catch
                        {
                        }
                        break;
                    }
                }
                return;
            }

            for (int i = 1; i < _units.GetLength(0); i++)
            {
                if (_units[i, 0].Time > 0)
                {
                    try
                    {
                        int i1 = i;
                        for (int j = i; i < _units.GetLength(0); j++)
                        {
                            _units[i1, j].Time -= millisecondIncrease * _timer.Interval;
                            if (_units[i1, j].Time < 0) _units[i1, j].Time = 0;
                            i1++;
                        }
                    }
                    catch
                    {
                    }
                    break;
                }
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
