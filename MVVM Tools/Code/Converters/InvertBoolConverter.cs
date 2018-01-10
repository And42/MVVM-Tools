using System.Globalization;
using MVVM_Tools.Code.Classes;

namespace MVVM_Tools.Code.Converters
{
    internal class InvertBoolConverter : ConverterBase<bool, bool>
    {
        protected override bool ConvertInternal(bool value, object parameter, CultureInfo culture)
        {
            return !value;
        }

        protected override bool ConvertBackInternal(bool value, object parameter, CultureInfo culture)
        {
            return !value;
        }
    }
}
