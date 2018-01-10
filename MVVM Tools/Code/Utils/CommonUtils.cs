using System;

namespace MVVM_Tools.Code.Utils
{
    public static class CommonUtils
    {
        public static TValidType CheckValueTypeAndCast<TValidType>(object value)
        {
            if (ReferenceEquals(value, null))
            {
                var defaultValue = default(TValidType);

                if (defaultValue == null)
                    return default;

                throw new ArgumentException($"Provided value is null while `{typeof(TValidType).FullName}` is not nullable");
            }

            if (value is TValidType)
                return (TValidType)value;

            throw new ArgumentException(
                "Invalid parameter type\n" +
                $"Expected: `{typeof(TValidType).FullName}`\n" +
                $"Actual: `{value.GetType().FullName}`"
            );
        }
    }
}
