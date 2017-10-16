using System;
using System.Windows.Input;

namespace MorninBytes.ViewModels
{
    public class DelegateCommand<T> : ICommand
    {
        private readonly Action<object> _actionWithParam;
        private readonly Action _action;


        public DelegateCommand(Action action)
        {
            _action = action;
        }

        public DelegateCommand(Action<object> action)
        {
            _actionWithParam = action;
        }

        public void Execute(object parameter)
        {
            if (parameter == null)
                _action();
            else
                _actionWithParam(parameter);
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

#pragma warning disable 67
        public event EventHandler CanExecuteChanged { add { } remove { } }
#pragma warning restore 67
    }
}
