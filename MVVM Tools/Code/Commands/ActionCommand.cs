using System;

namespace MVVM_Tools.Code.Commands
{
    public class ActionCommand : ActionCommand<object>
    {
        public ActionCommand(Action executeAction, Func<bool> canExecuteAction = null)
            : base(
                  parameter => executeAction(),
                  canExecuteAction == null ? (Func<object, bool>)null : parameter => canExecuteAction()
              )
        { }
    }
}
