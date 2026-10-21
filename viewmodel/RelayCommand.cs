using System;
using System.Windows.Input;

namespace GestiCoop3.viewmodel
{
    // commande simple qui exécute une action définie dans le viewmodel
    public class RelayCommand : ICommand
    {
        private readonly Action<object?> executer;
        private readonly Predicate<object?>? peutExecuter;

        public RelayCommand(Action<object?> executer, Predicate<object?>? peutExecuter = null)
        {
            this.executer = executer;
            this.peutExecuter = peutExecuter;
        }

        public event EventHandler? CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object? parameter)
        {
            return peutExecuter == null || peutExecuter(parameter);
        }

        public void Execute(object? parameter)
        {
            executer(parameter);
        }
    }
}