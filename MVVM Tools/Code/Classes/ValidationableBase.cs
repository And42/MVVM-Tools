using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace MVVM_Tools.Code.Classes
{
    public abstract class ValidationableBase : BindableBase, IDataErrorInfo
    {
        private readonly Dictionary<string, List<Func<string>>> _rules = new Dictionary<string, List<Func<string>>>(); 

        public string this[string columnName]
        {
            get
            {
                if (_rules.TryGetValue(columnName, out List<Func<string>> checkers))
                {
                    foreach (Func<string> checker in checkers)
                    {
                        string result = checker() ?? string.Empty;

                        if (result != string.Empty)
                            return result;
                    }
                }

                return string.Empty;
            }
        }

        public string Error { get; private set; }

        protected bool HasErrors()
        {
            return _rules.Any(ruleColl => ruleColl.Value.Any(checker => (checker() ?? string.Empty) != string.Empty));
        }

        protected void AddValidationRule<TProperty>(Expression<Func<TProperty>> propertyExpression, Func<string> errorChecker)
        {
            if (errorChecker == null)
                throw new ArgumentNullException(nameof(errorChecker));

            PropertyInfo propInfo = GetPropertyInfo(propertyExpression);

            if (_rules.TryGetValue(propInfo.Name, out var rules))
                rules.Add(errorChecker);
            else
                _rules.Add(propInfo.Name, new List<Func<string>> {errorChecker});
        }

        protected bool HasErrors<TProperty>(Expression<Func<TProperty>> propertyExpression)
        {
            PropertyInfo propInfo = GetPropertyInfo(propertyExpression);

            if (_rules.TryGetValue(propInfo.Name, out var items))
                return items.Any(checker => (checker() ?? string.Empty) != string.Empty);

            return false;
        }

        private static PropertyInfo GetPropertyInfo<TProperty>(Expression<Func<TProperty>> propertyExpression)
        {
            if (propertyExpression == null)
                throw new ArgumentNullException(nameof(propertyExpression));
            

            var member = propertyExpression.Body as MemberExpression;
            if (member == null)
                throw new ArgumentException($"Expression '{propertyExpression}' refers to a method, not a property.");

            var propInfo = member.Member as PropertyInfo;
            if (propInfo == null)
                throw new ArgumentException($"Expression '{propertyExpression}' refers to a field, not a property.");

            return propInfo;
        }
    }
}
