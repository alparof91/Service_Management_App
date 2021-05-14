using Service_Management_App.Data;
using System;
using System.Diagnostics;
using System.Windows.Input;

namespace Service_Management_App
{
    internal class RelayCommand : ICommand
    {
        private Action action;

        public RelayCommand(Action action)
        {
            this.action = action;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            //throw new NotImplementedException();
            return true;
        }

        public void Execute(object parameter)
        {
            action();
        }
    }
}