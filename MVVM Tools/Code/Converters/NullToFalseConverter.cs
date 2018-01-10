using System.Globalization;
using MVVM_Tools.Code.Classes;

namespace MVVM_Tools.Code.Converters
{
    public class NullToFalseConverter : ConverterBase<object, bool>
    {
        protected override bool ConvertInternal(object value, object parameter, CultureInfo culture)
        {
            return value != null;
        }
    }
}
