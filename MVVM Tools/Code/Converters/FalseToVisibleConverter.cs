using System.Globalization;
using System.Windows;
using MVVM_Tools.Code.Classes;

namespace MVVM_Tools.Code.Converters
{
    /// <summary>
    /// Class that converts <code>False</code> to <see cref="Visibility.Visible"/> and <code>True</code> to <see cref="Visibility.Collapsed"/>
    /// </summary>
    public class FalseToVisibleConverter : ConverterBase<bool, Visibility>
    {
        /// <inheritdoc />
        public override Visibility ConvertInternal(bool value, object parameter, CultureInfo culture)
        {
            return value ? Visibility.Collapsed : Visibility.Visible;
        }

        /// <inheritdoc />
        public override bool ConvertBackInternal(Visibility value, object parameter, CultureInfo culture)
        {
            return value != Visibility.Visible;
        }
    }
}
