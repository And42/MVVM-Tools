using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using MVVM_Tools.Code.Providers;

namespace MVVM_Tools.Code.Classes
{
    /// <summary>
    /// Base class for property changes notifications
    /// </summary>
    public abstract class BindableBase : INotifyPropertyChanged
    {
        /// <summary>
        /// Sets the value of a specified property if it is different from the current one (checking using <see cref="EqualityComparer{T}.Default"/>)
        /// </summary>
        /// <typeparam name="T">Value type</typeparam>
        /// <param name="storage">Property's backing field</param>
        /// <param name="value">New value</param>
        /// <param name="propertyName">Property name</param>
        /// <returns><code>True</code> if <see cref="storage"/> value is changed; otherwise, <code>False</code></returns>
        protected bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(storage, value))
                return false;

            storage = value;
            OnPropertyChanged(propertyName);

            return true;
        }

        /// <summary>
        /// Sets the value of a specified property if it is different from the current one (checking using <see cref="object.ReferenceEquals"/>)
        /// </summary>
        /// <typeparam name="T">Value type</typeparam>
        /// <param name="storage">Property's backing field</param>
        /// <param name="value">New value</param>
        /// <param name="propertyName">Property name</param>
        /// <returns><code>True</code> if <see cref="storage"/> value is changed; otherwise, <code>False</code></returns>
        protected bool SetPropertyRef<T>(ref T storage, T value, [CallerMemberName] string propertyName = null) where T : class
        {
            if (ReferenceEquals(storage, value))
                return false;

            storage = value;
            OnPropertyChanged(propertyName);

            return true;
        }

        /// <summary>
        /// Creates <see cref="Property{TPropertyType}"/> and tunnels <see cref="Property{TPropertyType}"/>.PropertyChanged to the current object
        /// </summary>
        /// <param name="targetPropertyName">Property name to notify</param>
        /// <param name="initialValue">Initial property value</param>
        /// <typeparam name="TPropertyType">Property value type</typeparam>
        protected Property<TPropertyType> BindProperty<TPropertyType>(
            string targetPropertyName, TPropertyType initialValue = default)
        {
            var provider = new Property<TPropertyType>(initialValue);

            provider.PropertyChanged += (sender, args) => OnPropertyChanged(targetPropertyName);

            return provider;
        }

        /// <summary>
        /// Creates <see cref="Property{TPropertyType}"/> and tunnels <see cref="Property{TPropertyType}"/>.PropertyChanged to the source object
        /// </summary>
        /// <param name="source">Source container to bind to</param>
        /// <param name="propertyExpression">Property expression</param>
        /// <param name="initialValue">Initial property value</param>
        /// <typeparam name="TSource">Source type</typeparam>
        /// <typeparam name="TPropertyType">Property value type</typeparam>
        protected void BindProperty<TSource, TPropertyType>(
            TSource source,
            Expression<Func<TSource, Property<TPropertyType>>> propertyExpression,
            TPropertyType initialValue = default) where TSource : BindableBase
        {
            PropertyInfo property = GetPropertyInfo(propertyExpression);

            var provider = new Property<TPropertyType>(initialValue);

            provider.PropertyChanged += (sender, args) => source.OnPropertyChanged(property.Name);

            property.SetValue(source, provider, null);
        }

        /// <summary>
        /// Creates <see cref="Property{TPropertyType}"/> and tunnels <see cref="Property{TPropertyType}"/>.PropertyChanged to the source object
        /// </summary>
        /// <param name="propertyExpression">Property expression</param>
        /// <param name="initialValue">Initial property value</param>
        /// <typeparam name="TPropertyType">Property value type</typeparam>
        protected void BindProperty<TPropertyType>(
            Expression<Func<Property<TPropertyType>>> propertyExpression,
            TPropertyType initialValue = default)
        {
            PropertyInfo property = GetPropertyInfo(propertyExpression);

            var provider = new Property<TPropertyType>(initialValue);

            provider.PropertyChanged += (sender, args) => OnPropertyChanged(property.Name);

            property.SetValue(this, provider, null);
        }


        /// <summary>
        /// Creates <see cref="PropertyRef{TPropertyType}"/> and tunnels <see cref="PropertyRef{TPropertyType}"/>.PropertyChanged to the current object
        /// </summary>
        /// <param name="targetPropertyName">Property name to notify</param>
        /// <param name="initialValue">Initial property value</param>
        /// <typeparam name="TPropertyType">Property value type</typeparam>
        protected PropertyRef<TPropertyType> BindPropertyRef<TPropertyType>(
            string targetPropertyName, TPropertyType initialValue = default) where TPropertyType : class
        {
            var provider = new PropertyRef<TPropertyType>(initialValue);

            provider.PropertyChanged += (sender, args) => OnPropertyChanged(targetPropertyName);

            return provider;
        }

        /// <summary>
        /// Creates <see cref="PropertyRef{TPropertyType}"/> and tunnels <see cref="PropertyRef{TPropertyType}"/>.PropertyChanged to the source object
        /// </summary>
        /// <param name="source">Source container to bind to</param>
        /// <param name="propertyExpression">Property expression</param>
        /// <param name="initialValue">Initial property value</param>
        /// <typeparam name="TSource">Source type</typeparam>
        /// <typeparam name="TPropertyType">Property value type</typeparam>
        protected void BindPropertyRef<TSource, TPropertyType>(
            TSource source,
            Expression<Func<TSource, PropertyRef<TPropertyType>>> propertyExpression,
            TPropertyType initialValue = default) where TSource : BindableBase where TPropertyType : class
        {
            PropertyInfo property = GetPropertyInfo(propertyExpression);

            var provider = new PropertyRef<TPropertyType>(initialValue);

            provider.PropertyChanged += (sender, args) => source.OnPropertyChanged(property.Name);

            property.SetValue(source, provider, null);
        }

        /// <summary>
        /// Creates <see cref="PropertyRef{TPropertyType}"/> and tunnels <see cref="PropertyRef{TPropertyType}"/>.PropertyChanged to the source object
        /// </summary>
        /// <param name="propertyExpression">Property expression</param>
        /// <param name="initialValue">Initial property value</param>
        /// <typeparam name="TPropertyType">Property value type</typeparam>
        protected void BindPropertyRef<TPropertyType>(
            Expression<Func<PropertyRef<TPropertyType>>> propertyExpression,
            TPropertyType initialValue = default) where TPropertyType : class
        {
            PropertyInfo property = GetPropertyInfo(propertyExpression);

            var provider = new PropertyRef<TPropertyType>(initialValue);

            provider.PropertyChanged += (sender, args) => OnPropertyChanged(property.Name);

            property.SetValue(this, provider, null);
        }

        /// <summary>
        /// Occurs on property changes
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Used to call <see cref="PropertyChanged"/> event on a specified property
        /// </summary>
        /// <param name="propertyName">Property to use while raising <see cref="PropertyChanged"/> event</param>
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private static PropertyInfo GetPropertyInfo<TProperty>(Expression<Func<TProperty>> propertyLambda)
        {
            MemberExpression member = propertyLambda.Body as MemberExpression;
            if (member == null)
                throw new ArgumentException(
                    $"Expression '{propertyLambda}' refers to a method, not a property.");

            PropertyInfo propInfo = member.Member as PropertyInfo;
            if (propInfo == null)
                throw new ArgumentException(
                    $"Expression '{propertyLambda}' refers to a field, not a property.");

            return propInfo;
        }

        private static PropertyInfo GetPropertyInfo<TSource, TProperty>(Expression<Func<TSource, TProperty>> propertyLambda)
        {
            Type type = typeof(TSource);

            MemberExpression member = propertyLambda.Body as MemberExpression;
            if (member == null)
                throw new ArgumentException(
                    $"Expression '{propertyLambda}' refers to a method, not a property.");

            PropertyInfo propInfo = member.Member as PropertyInfo;
            if (propInfo == null)
                throw new ArgumentException(
                    $"Expression '{propertyLambda}' refers to a field, not a property.");

            if (type != propInfo.ReflectedType && !type.IsSubclassOf(propInfo.ReflectedType))
                throw new ArgumentException(
                    $"Expression '{propertyLambda}' refers to a property that is not from type {type}.");

            return propInfo;
        }
    }
}
