using System;
using System.Windows;
using System.Windows.Data;

namespace Appium.Converters
{
    /// <summary>
    /// Converts a string to a visibility state
    /// </summary>
    internal class StringToVisibilityConverter : IValueConverter
    {
        /// <summary>
        /// Converts a string to a visibility state
        /// </summary>
        /// <param name="value">string object</param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns>Visibility.Visible if string is not null or empty, else Visibility.Collapsed</returns>
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return string.IsNullOrWhiteSpace(value as string) ? Visibility.Collapsed : Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
