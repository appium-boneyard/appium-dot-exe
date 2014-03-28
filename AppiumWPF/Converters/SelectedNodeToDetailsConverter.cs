using Appium.ViewModels;
using System;
using System.Windows.Data;

namespace Appium.Converters
{
    /// <summary>
    /// Converts the UIAutomatorNodeVM to a displayable string
    /// </summary>
    public class SelectedNodeToDetailsConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var retVal = string.Empty;
            var node = value as UIAutomatorNodeVM;

            if (null != node)
            {
                retVal = node.GetDetails();
            }
            return retVal;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
