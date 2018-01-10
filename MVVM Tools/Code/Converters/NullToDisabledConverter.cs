using System.Globalization;
using System.Windows;
using MVVM_Tools.Code.Classes;

namespace MVVM_Tools.Code.Converters
{
    /// <summary>
    /// Converts <code>null</code> to <see cref="Visibility.Collapsed"/> and other values to <see cref="Visibility.Visible"/>
    /// </summary>
    public class NullToCollapsedConverter : ConverterBase<object, Visibility>
    {
        /// <inheritdoc />
        public override Visibility ConvertInternal(object value, object parameter, CultureInfo culture)
        {
            return value == null ? Visibility.Collapsed : Visibility.Visible;
        }
    }
}
