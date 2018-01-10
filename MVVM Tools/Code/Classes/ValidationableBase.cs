using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace MVVM_Tools.Code.Classes
{
    /// <summary>
    /// Base class for handling property binding errors
    /// </summary>
    public abstract class ValidationableBase : BindableBase, IDataErrorInfo
    {
        private readonly Dictionary<string, List<Func<string>>> _rules = new Dictionary<string, List<Func<string>>>(); 

        /// <summary>
        /// Performs checks and returns the first error related to the property
        /// </summary>
        /// <param name="propertyName">Property name used for checking</param>
        /// <returns>Error text if there is at least one error; otherwise, <see cref="string.Empty"/></returns>
        public string this[string propertyName]
        {
            get
            {
                if (_rules.TryGetValue(propertyName, out List<Func<string>> checkers))
                {
                    foreach (Func<string> checker in checkers)
                    {
                        string result = checker();

                        if (!string.IsNullOrEmpty(result))
                            return result;
                    }
                }

                return string.Empty;
            }
        }

        /// <summary>
        /// Unused, throws <see cref="NotSupportedException"/>
        /// </summary>
        public virtual string Error => throw new NotSupportedException();

        /// <summary>
        /// Returns whether this object has any errors
        /// </summary>
        protected bool HasErrors()
        {
            return _rules.Any(ruleColl => ruleColl.Value.Any(checker => (checker() ?? string.Empty) != string.Empty));
        }

        /// <summary>
        /// Returns whether specified property has errors
        /// </summary>
        /// <typeparam name="TProperty">Property value type</typeparam>
        /// <param name="propertyExpression">Property expression</param>
        /// <returns><code>True</code> if has; otherwise (including when property is not found), <code>False</code></returns>
        protected bool HasErrors<TProperty>(Expression<Func<TProperty>> propertyExpression)
        {
            PropertyInfo propInfo = GetPropertyInfo(propertyExpression);

            return HasErrors(propInfo.Name);
        }

        /// <summary>
        /// Returns whether specified property has errors
        /// </summary>
        /// <param name="propertyName">Property name</param>
        /// <returns><code>True</code> if has; otherwise (including when property is not found), <code>False</code></returns>
        protected bool HasErrors(string propertyName)
        {
            if (_rules.TryGetValue(propertyName, out var items))
                return items.Any(checker => (checker() ?? string.Empty) != string.Empty);

            return false;
        }

        /// <summary>
        /// Adds validation rule to provided property
        /// </summary>
        /// <typeparam name="TProperty">Property value type</typeparam>
        /// <param name="propertyExpression">Property expression</param>
        /// <param name="errorChecker">Function that checks property for errors and returns result message (<code>null</code> or <see cref="string.Empty"/> if there are no errors)</param>
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
