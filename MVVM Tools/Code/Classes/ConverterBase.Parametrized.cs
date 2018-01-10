using System;
using System.Globalization;
using System.Windows.Data;
using MVVM_Tools.Code.Utils;

namespace MVVM_Tools.Code.Classes
{
    public abstract class ConverterBase<TSource, TParameter, TTarget> : IValueConverter
    {
        private static readonly bool IsSourceNullable = default(TSource) == null;
        private static readonly bool IsParameterNullable = default(TParameter) == null;
        private static readonly bool IsTargetNullable = default(TSource) == null;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            TSource typedSource = value == null ? GetSourceIfNull() : CommonUtils.CheckValueTypeAndCast<TSource>(value);
            TParameter typedParameter = parameter == null ? GetParameterIfNull() : CommonUtils.CheckValueTypeAndCast<TParameter>(parameter);

            return ConvertInternal(typedSource, typedParameter, culture);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            TTarget typedTarget = value == null ? GetTargetIfNull() : CommonUtils.CheckValueTypeAndCast<TTarget>(value);
            TParameter typedParameter = parameter == null ? GetParameterIfNull() : CommonUtils.CheckValueTypeAndCast<TParameter>(parameter);

            return ConvertBackInternal(typedTarget, typedParameter, culture);
        }

        protected abstract TTarget ConvertInternal(TSource value, TParameter parameter, CultureInfo culture);

        protected virtual TSource ConvertBackInternal(TTarget value, TParameter parameter, CultureInfo culture)
        {
            throw new InvalidOperationException($"'{GetType().FullName}' converter can't handle back conversation");
        }

        protected virtual TSource GetSourceIfNull()
        {
            if (IsSourceNullable)
                return default;

            throw new InvalidOperationException("Operation is not supported on structs");
        }

        protected virtual TParameter GetParameterIfNull()
        {
            if (IsParameterNullable)
                return default;

            throw new InvalidOperationException("Operation is not supported on structs");
        }

        protected virtual TTarget GetTargetIfNull()
        {
            if (IsTargetNullable)
                return default;

            throw new InvalidOperationException("Operation is not supported on structs");
        }
    }
}
