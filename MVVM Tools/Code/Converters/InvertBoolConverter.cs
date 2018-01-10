using System.Globalization;
using MVVM_Tools.Code.Classes;

namespace MVVM_Tools.Code.Converters
{
    /// <summary>
    /// Class that converts <code>False</code> to <code>True</code> and <code>True</code> to <code>False</code>/>
    /// </summary>
    public class InvertBoolConverter : ConverterBase<bool, bool>
    {
        /// <inheritdoc />
        public override bool ConvertInternal(bool value, object parameter, CultureInfo culture)
        {
            return !value;
        }

        /// <inheritdoc />
        public override bool ConvertBackInternal(bool value, object parameter, CultureInfo culture)
        {
            return !value;
        }
    }
}
