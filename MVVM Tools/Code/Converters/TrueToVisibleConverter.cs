using System.Globalization;
using System.Windows;
using MVVM_Tools.Code.Classes;

namespace MVVM_Tools.Code.Converters
{
    /// <summary>
    /// Converts <code>True</code> to <see cref="Visibility.Visible"/> and other values to <see cref="Visibility.Collapsed"/>
    /// </summary>
    public class TrueToVisibleConverter : ConverterBase<bool, Visibility>
    {
        /// <inheritdoc />
        public override Visibility ConvertInternal(bool value, object parameter, CultureInfo culture)
        {
            return value ? Visibility.Visible : Visibility.Collapsed;
        }

        /// <inheritdoc />
        public override bool ConvertBackInternal(Visibility value, object parameter, CultureInfo culture)
        {
            return value == Visibility.Visible;
        }
    }
}
