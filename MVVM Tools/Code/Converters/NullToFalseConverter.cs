using System.Globalization;
using MVVM_Tools.Code.Classes;

namespace MVVM_Tools.Code.Converters
{
    /// <summary>
    /// Converts <code>null</code> to <code>False</code> and other values to <code>True</code>
    /// </summary>
    public class NullToFalseConverter : ConverterBase<object, bool>
    {
        /// <inheritdoc />
        public override bool ConvertInternal(object value, object parameter, CultureInfo culture)
        {
            return value != null;
        }
    }
}
