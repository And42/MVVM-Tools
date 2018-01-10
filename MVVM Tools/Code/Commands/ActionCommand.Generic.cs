using System;
using System.Windows.Input;
using MVVM_Tools.Code.Utils;

namespace MVVM_Tools.Code.Commands
{
    /// <summary>
    /// Command that implements <see cref="ICommand"/> interface
    /// </summary>
    /// <typeparam name="TParameter">Parameter value type</typeparam>
    public class ActionCommand<TParameter> : ICommand
    {
        private readonly Func<TParameter, bool> _canExecuteAction;
        private readonly Action<TParameter> _executeAction;

        /// <summary>
        /// Creates a new instance of the <see cref="ActionCommand"/>
        /// </summary>
        /// <param name="executeAction">Action that is called when a command is executed (after <see cref="canExecuteAction"/> is called)</param>
        /// <param name="canExecuteAction">Action that checks parameter reters whether the command can execute</param>
        /// <exception cref="ArgumentNullException">Is thrown if <see cref="executeAction"/> is null</exception>
        public ActionCommand(Action<TParameter> executeAction, Func<TParameter, bool> canExecuteAction = null)
        {
            // ReSharper disable once JoinNullCheckWithUsage
            if (executeAction == null)
                throw new ArgumentNullException(nameof(executeAction));

            _canExecuteAction = canExecuteAction ?? TrueCanExecute;
            _executeAction = executeAction;
        }

        /// <inheritdoc />
        public bool CanExecute(object parameter)
        {
            return _canExecuteAction(CommonUtils.CheckValueTypeAndCast<TParameter>(parameter));
        }

        /// <inheritdoc />
        public void Execute(object parameter)
        {
            var typedParameter = CommonUtils.CheckValueTypeAndCast<TParameter>(parameter);

            if (!_canExecuteAction(typedParameter))
                return;

            _executeAction(typedParameter);
        }

        /// <summary>
        /// Raises <see cref="CanExecuteChanged"/> event
        /// </summary>
        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        /// <inheritdoc />
        public event EventHandler CanExecuteChanged;

        private static bool TrueCanExecute(TParameter obj) => true;
    }
}
