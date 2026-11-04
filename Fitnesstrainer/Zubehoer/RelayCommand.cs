using System;
using System.Windows.Input;
namespace CommandHelper
{
    /// <summary>
    /// AUFGABE 7 – Command-Pattern:
    /// Kapselt eine Aktion, sodass die View sie auslösen kann, ohne eine
    /// konkrete Methode des ViewModels direkt zu kennen.
    /// Diese eigene Klasse ersetzt die alte externe CommandHelper.dll.
    /// </summary>
    public sealed class RelayCommand : ICommand
    {
        private readonly Action<object> _execute;
        private readonly Predicate<object> _canExecute;
        public RelayCommand(Action<object> execute, Predicate<object> canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }
        public bool CanExecute(object parameter) => _canExecute?.Invoke(parameter) ?? true;
        public void Execute(object parameter) => _execute(parameter);
        // WPF prüft bei Änderungen der Oberflächenbedingungen erneut CanExecute.
        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }
    }
}
