using System;
using System.Windows.Input;
using MVVM_Tools.Code.Utils;

namespace MVVM_Tools.Code.Commands
{
    public class ActionCommand<TParameter> : ICommand
    {
        private readonly Func<TParameter, bool> _canExecuteAction;
        private readonly Action<TParameter> _executeAction;

        public ActionCommand(Action<TParameter> executeAction, Func<TParameter, bool> canExecuteAction = null)
        {
            // ReSharper disable once JoinNullCheckWithUsage
            if (executeAction == null)
                throw new ArgumentNullException(nameof(executeAction));

            _canExecuteAction = canExecuteAction ?? TrueCanExecute;
            _executeAction = executeAction;
        }

        public bool CanExecute(object parameter)
        {
            return _canExecuteAction(CommonUtils.CheckValueTypeAndCast<TParameter>(parameter));
        }

        public void Execute(object parameter)
        {
            var typedParameter = CommonUtils.CheckValueTypeAndCast<TParameter>(parameter);

            if (!_canExecuteAction(typedParameter))
                return;

            _executeAction(typedParameter);
        }

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        public event EventHandler CanExecuteChanged;

        private static bool TrueCanExecute(TParameter obj) => true;
    }
}
