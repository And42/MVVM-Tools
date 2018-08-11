using MVVM_Tools.Code.Classes;

namespace MVVM_Tools.Code.Providers
{
    /// <summary>
    /// Provider for the properties (use <see cref="BindableBase.PropertyChanged"/> to subscribe for changes).
    /// Uses <see cref="BindableBase.SetPropertyRef{TPropertyType}"/> when setting new value
    /// </summary>
    /// <typeparam name="TPropertyType">Property type</typeparam>
    public class PropertyRef<TPropertyType> : BindableBase where TPropertyType : class
    {
        private TPropertyType _value;

        /// <summary>
        /// Property initial value (passed to the constructor or null)
        /// </summary>
        public TPropertyType InitialValue { get; }

        /// <inheritdoc />
        /// <summary>
        /// Creates a new instance of the <see cref="T:MVVM_Tools.Code.Providers.PropertyRef`1" /> class
        /// </summary>
        public PropertyRef(): this(default) { }

        /// <summary>
        /// Creates a new instance of the <see cref="Property{TPropertyType}"/> class
        /// </summary>
        /// <param name="initialValue">Initial value for the backing field</param>
        public PropertyRef(TPropertyType initialValue)
        {
            InitialValue = initialValue;
            _value = initialValue;
        }

        /// <summary>
        /// Current value of the provider
        /// </summary>
        public TPropertyType Value
        {
            get => _value;
            set => SetPropertyRef(ref _value, value);
        }

        /// <summary>
        /// Restores <see cref="InitialValue"/> by assigning it to the <see cref="Value"/> property
        /// </summary>
        public void ResetValue()
        {
            Value = InitialValue;
        }
    }
}
