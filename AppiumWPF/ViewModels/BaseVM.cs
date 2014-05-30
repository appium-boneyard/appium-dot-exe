using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;

namespace Appium.ViewModels
{
    /// <summary>
    /// Base View Model class
    /// </summary>
    public class BaseVM : INotifyPropertyChanged
    {
        /// <summary>Dictionary of all names with their property changed event args </summary>
        private readonly Dictionary<string, PropertyChangedEventArgs> eventArgsCache = new Dictionary<string, PropertyChangedEventArgs>();

        #region INotifyPropertyChanged
        /// <summary>Subscribe to the Property Changed event</summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>Fire the Property changed event</summary>
        /// <param name="propertyName">string of the property changed</param>
        protected void FirePropertyChanged(string propertyName)
        {
            PropertyChangedEventArgs args;
            if (!eventArgsCache.TryGetValue(propertyName, out args))
            {
                args = new PropertyChangedEventArgs(propertyName);
                eventArgsCache.Add(propertyName, args);
            }

            FirePropertyChanged(args);
        }

        /// <summary>Fire the Property changed event</summary>
        /// <param name="args">Property changed arg</param>
        protected void FirePropertyChanged(PropertyChangedEventArgs args)
        {
            if (!eventArgsCache.ContainsKey(args.PropertyName))
            {
                eventArgsCache.Add(args.PropertyName, args);
            }

            if (null != PropertyChanged)
            {
                PropertyChanged(this, args);
            }
        }

        /// <summary>Fire the Property changed event</summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="expression">linq expression used to find the property name</param>
        protected void FirePropertyChanged<T>(Expression<Func<T>> expression)
        {
            var propertyName = GetPropertyName(expression);
            FirePropertyChanged(propertyName);
        }

        /// <summary>Use Expression to get the member name</summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="expression"></param>
        /// <returns>property name</returns>
        protected string GetPropertyName<T>(Expression<Func<T>> expression)
        {
            // Similar to http://stackoverflow.com/questions/3778598/get-string-property-name-from-expression but we only care to get the name
            var propertyName = string.Empty;
            if (null != expression && expression.Body is MemberExpression)
            {
                propertyName = (expression.Body as MemberExpression).Member.Name;
            }
            return propertyName;
        }
        #endregion INotifyPropertyChanged
    }
}
