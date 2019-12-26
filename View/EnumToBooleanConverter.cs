using System;
using System.Windows;
using System.Windows.Data;
using System.Globalization;

namespace cat {

    /// <summary>
    /// Enum To Boolean Value Cross Converter
    /// 
    /// For Enum <> RadioButton
    /// </summary>
    public class EnumToBooleanConverter : IValueConverter {
        /// <summary>
        /// Convert Enum Select To Bool
        /// </summary>
        /// <param name="value">Enum Type</param>
        /// <param name="targetType"></param>
        /// <param name="parameter">Enum Value</param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            var parameterString = parameter as string;
            if (null == parameterString)
                return DependencyProperty.UnsetValue;

            if (!Enum.IsDefined(value.GetType(), value))
                return DependencyProperty.UnsetValue;

            var parameterValue = Enum.Parse(value.GetType(), parameterString);
            return parameterValue.Equals(value);
        }

        /// <summary>
        /// Convert Bool To Enum Value
        /// </summary>
        /// <param name="value">Bool Value</param>
        /// <param name="targetType">Enum Type</param>
        /// <param name="parameter">Enum Name</param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            var parameterString = parameter as string;
            if (null == parameterString)
                return DependencyProperty.UnsetValue;

            if (true.Equals(value))
                return Enum.Parse(targetType, parameterString);
            else
                return DependencyProperty.UnsetValue;
        }
    }
}
