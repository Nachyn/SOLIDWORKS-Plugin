using System;
using System.Windows.Input;

namespace Drawer3D.ViewWpf.Commands
{
    /// <summary>
    ///     Класс, позволяющий форме выполнять команды
    /// </summary>
    public class RelayCommand : ICommand
    {
        /// <summary>
        ///     Условия, указывающие может ли команда выполняться
        /// </summary>
        private readonly Func<object, bool> _canExecute;

        /// <summary>
        ///     Команда
        /// </summary>
        private readonly Action<object> _execute;

        /// <summary>
        ///     Конструктор
        /// </summary>
        /// <param name="execute">Действие</param>
        /// <param name="canExecute">Условия, указывающие может ли команда выполняться</param>
        public RelayCommand(Action<object> execute
            , Func<object, bool> canExecute = null)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        /// <summary>
        ///     Событие вызывается при изменении условий, указывающих, может ли команда выполняться.
        /// </summary>
        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        /// <summary>
        ///     Определяет, может ли команда выполняться
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public bool CanExecute(object parameter)
        {
            return _canExecute == null || _canExecute(parameter);
        }

        /// <summary>
        ///     Вызывает команду
        /// </summary>
        /// <param name="parameter">Любой параметр</param>
        public void Execute(object parameter)
        {
            _execute(parameter);
        }
    }
}