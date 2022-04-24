using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Unusual_Timer.Commands;

namespace Unusual_Timer.ViewModel
{
    public class MainWindowViewModel
    {
        private readonly MyTimer _timer;

        #region Commands
        public ICommand StartTimerCommand { get; }

        private void OnStartTimerCommandExecuted(object parametr) => Timer.Start();
        private bool CanStartTimerCommandExecute(object parametr)
        {
            return true;
        }

        public ICommand ResetTimerCommand { get; }
        private void OnResetTimerCommandExecuted(object parametr) => Timer.Reset();
        private bool CanResetTimerCommandExecute(object parametr) => true;

        public ICommand StopTimerCommand { get; }
        private void OnStopTimerCommandExecuted(object parametr) => Timer.Stop();
        private bool CanStopTimerCommandExecute(object parametr) => true;
        #endregion

        public MainWindowViewModel()
        {
            StartTimerCommand = new LambdaCommand(OnStartTimerCommandExecuted, CanStartTimerCommandExecute);
            ResetTimerCommand = new LambdaCommand(OnResetTimerCommandExecuted, CanResetTimerCommandExecute);
            StopTimerCommand  = new LambdaCommand(OnStopTimerCommandExecuted, CanStopTimerCommandExecute);

            _timer = new MyTimer(new TimeSpan(0, 0, 20));
        }

        public MyTimer Timer { get => _timer; }
    }
}
