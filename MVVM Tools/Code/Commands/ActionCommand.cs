using System;
using System.Windows.Input;

namespace MVVM_Tools.Code.Commands
{
    /// <summary>
    /// Command that implements <see cref="ICommand"/> interface. Parameters is of the <see cref="object"/> type
    /// </summary>
    public class ActionCommand : ActionCommand<object>
    {
        /// <summary>
        /// Creates a new instance of the <see cref="ActionCommand"/>
        /// </summary>
        /// <param name="executeAction">Action that is called when a command is executed (after <see cref="canExecuteAction"/> is called)</param>
        /// <param name="canExecuteAction">Action that checks parameter reters whether the command can execute</param>
        /// <exception cref="ArgumentNullException">Is thrown if <see cref="executeAction"/> is null</exception>
        public ActionCommand(Action executeAction, Func<bool> canExecuteAction = null)
            : base(
                  parameter => executeAction(),
                  canExecuteAction == null ? (Func<object, bool>)null : parameter => canExecuteAction()
              )
        { }
    }
}
