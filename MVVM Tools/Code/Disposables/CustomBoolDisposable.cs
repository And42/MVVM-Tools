using System;

namespace MVVM_Tools.Code.Disposables
{
    /// <summary>
    /// Class that sets the value to true on load and to false on dispose
    /// </summary>
    public class CustomBoolDisposable : IDisposable
    {
        private Action<bool> _setValue;

        /// <summary>
        /// Creates new instance of a class with the provided value setter
        /// </summary>
        /// <param name="setValue">Value setter</param>
        /// <exception cref="ArgumentNullException">thrown if setValue is null</exception>
        public CustomBoolDisposable(Action<bool> setValue)
        {
            if (setValue == null)
                throw new ArgumentNullException(nameof(setValue));

            _setValue = setValue;

            _setValue(true);
        }

        /// <inheritdoc />
        public void Dispose()
        {
            if (_setValue == null)
                throw new ObjectDisposedException(nameof(CustomBoolDisposable));

            _setValue(false);

            _setValue = null;
        }
    }
}
