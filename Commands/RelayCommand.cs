using System.Windows.Input;

namespace WheatGrainClassifierWpfApp.Commands
{
    public class RelayCommand : ICommand
    {
        private readonly Predicate<object?>? _canExecute;
        private readonly Action<object?> _execute;

        // Constructeur pour commandes sans paramètre.
        public RelayCommand(Action execute, Predicate<bool?>? canExecute = null)
        {
            _execute = _ => execute();
            _canExecute = canExecute is null ? null : _ => canExecute();
        }

        // Constructeur pour commandes avec paramètre.
        public RelayCommand(Action<object?> execute, Predicate<object>? canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        public bool CanExecute(object? parameter) => _canExecute?.Invoke(parameter) ?? true;
        public void Execute(object? parameter) => _execute(parameter);

        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }
    }
}
