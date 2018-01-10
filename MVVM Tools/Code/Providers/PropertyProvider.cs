using MVVM_Tools.Code.Classes;

namespace MVVM_Tools.Code.Providers
{
    public class PropertyProvider<TPropertyType> : BindableBase
    {
        private TPropertyType _value;

        public PropertyProvider() { }

        public PropertyProvider(TPropertyType initialValue)
        {
            _value = initialValue;
        }

        public TPropertyType Value
        {
            get => _value;
            set => SetProperty(ref _value, value);
        }
    }
}
