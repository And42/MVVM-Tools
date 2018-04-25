using MVVM_Tools.Code.Classes;

namespace MVVM_Tools.Code.Providers
{
    /// <summary>
    /// Provider for the properties (use <see cref="BindableBase.PropertyChanged"/> to subscribe for changes).
    /// Uses <see cref="BindableBase.SetProperty{TPropertyType}"/> when setting new value
    /// </summary>
    /// <typeparam name="TPropertyType">Property type</typeparam>
    public class Property<TPropertyType> : BindableBase
    {
        private TPropertyType _value;

        /// <summary>
        /// Creates a new instance of the <see cref="Property{TPropertyType}"/> class
        /// </summary>
        public Property() { }

        /// <summary>
        /// Creates a new instance of the <see cref="Property{TPropertyType}"/> class
        /// </summary>
        /// <param name="initialValue">Initial value for the backing field</param>
        public Property(TPropertyType initialValue)
        {
            _value = initialValue;
        }

        /// <summary>
        /// Current value of the provider
        /// </summary>
        public TPropertyType Value
        {
            get => _value;
            set => SetProperty(ref _value, value);
        }
    }
}
