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
        /// Creates a new instance of the <see cref="PropertyRef{TPropertyType}"/> class
        /// </summary>
        public PropertyRef() { }

        /// <summary>
        /// Creates a new instance of the <see cref="Property{TPropertyType}"/> class
        /// </summary>
        /// <param name="initialValue">Initial value for the backing field</param>
        public PropertyRef(TPropertyType initialValue)
        {
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
    }
}
