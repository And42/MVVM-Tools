using System.Globalization;
using System.Windows;
using MVVM_Tools.Code.Classes;

namespace MVVM_Tools.Code.Converters
{
    public class TrueToVisibleConverter : ConverterBase<bool, Visibility>
    {
        protected override Visibility ConvertInternal(bool value, object parameter, CultureInfo culture)
        {
            return value ? Visibility.Visible : Visibility.Collapsed;
        }

        protected override bool ConvertBackInternal(Visibility value, object parameter, CultureInfo culture)
        {
            return value == Visibility.Visible;
        }
    }
}
