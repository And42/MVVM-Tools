using System.Globalization;
using System.Windows;
using MVVM_Tools.Code.Classes;

namespace MVVM_Tools.Code.Converters
{
    public class NullToCollapsedConverter : ConverterBase<object, Visibility>
    {
        protected override Visibility ConvertInternal(object value, object parameter, CultureInfo culture)
        {
            return value == null ? Visibility.Collapsed : Visibility.Visible;
        }
    }
}
