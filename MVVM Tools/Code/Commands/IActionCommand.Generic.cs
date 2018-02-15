using System.Windows.Input;

namespace MVVM_Tools.Code.Commands
{
    public interface IActionCommand<in TParameter> : ICommand
    {
        bool CanExecute(TParameter parameter);

        void Execute(TParameter parameter);

        /// <summary>
        /// Raises <see cref="CanExecuteChanged"/> event
        /// </summary>
        void RaiseCanExecuteChanged();
    }
}