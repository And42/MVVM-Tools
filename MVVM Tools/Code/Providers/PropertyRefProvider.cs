using MVVM_Tools.Code.Classes;

namespace MVVM_Tools.Code.Providers
{
    public class PropertyRefProvider<TPropertyType> : BindableBase where TPropertyType : class
    {
        private TPropertyType _value;

        public PropertyRefProvider() { }

        public PropertyRefProvider(TPropertyType initialValue)
        {
            _value = initialValue;
        }

        public TPropertyType Value
        {
            get => _value;
            set => SetPropertyRef(ref _value, value);
        }
    }
}
