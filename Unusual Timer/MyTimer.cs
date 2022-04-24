using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
        private double _progress;
        private double _unitDuration;
        public MyTimer(TimeSpan duration)
        {
            ListTimerUnits = new();
            CountUnitsInRow = 5;
            Duration = duration;
            _timer = new Timer();
            _timer.Elapsed += Timer_Elapsed;
            this.Reset();
        }

        #region Properties
        public int CountUnitsInRow
        {
            get => _countUnitsInRow;
            set
            {
                _countUnitsInRow = value;
                this.Reset();
            }
        }

        public ObservableCollection<TimerUnit> ListTimerUnits { get; private set; }

        public TimeSpan Duration
        {
            get => _duration;
            set
            {
                _duration = value;
                this.Reset();
            }
        }

        public bool IsEnabled { get => _isEnabled; }
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
            _isEnabled = true;
            /*if (_progress == 0)*/ Timer_Elapsed(null, null);
            _timer.Start();
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (_progress >= Duration.TotalMilliseconds)
            {
                this.Stop();
                return;
            }
            _progress += _timer.Interval;
            if (_units[0, 0].Time > 0 && !_units[0, 0].IsEnabled)
            {
                for (int j = _units.GetLength(1) - 1; j >= 0; j--)
                {
                    if (_units[0, j].Time == 0 || _units[0, j].IsEnabled) continue;

                    try
                    {
                        for (int i = 0; i < _units.Length; i++)
                        {
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
                if (_units[i, 0].Time == 0 || _units[i, 0].IsEnabled) continue;

                try
                {
                    for (int j = 0; j < _units.Length; j++)
                    {
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
            foreach (var item in ListTimerUnits)
            {
                if (item.IsEnabled) item.Stop();
            }
        }

        public void Reset()
        {
            if (_timer == null) return;
            this.Stop();

            _progress = 0;
            _unitDuration = Duration.TotalMilliseconds / (CountUnitsInRow * 2 - 1);
            _timer.Interval = _unitDuration;

            _units = new TimerUnit[CountUnitsInRow, CountUnitsInRow];

            for (int i = 0; i < _units.GetLength(0); i++)
            {
                for (int j = 0; j < _units.GetLength(1); j++)
                {
                    _units[i, j] = new TimerUnit(_unitDuration);
                }
            }

            ListTimerUnits.Clear();
            foreach (var item in _units)
            {
                ListTimerUnits.Add(item);
            }
        }
    }
}
