using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows;


namespace PL
{

   
    

        public class NotIDToBoolConverter : IValueConverter
        {
            //convert from source property type to target property type
            public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            {
                int intValue = (int)value;
                if (intValue == 0)
                {
                    return false; //Visibility.Collapsed;
                }
                else
                {
                    return true;
                }
            }
            //convert from target property type to source property type
            public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            {
                throw new NotImplementedException();
            }
        }

    [ValueConversion(typeof(int), typeof(Visibility))]
    public class ZeroToHidden : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
            => (int)value == 0 ? Visibility.Hidden : Visibility.Visible;

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
            => throw new NotImplementedException();
    }

    public class FalseToHidden : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
            => (bool)value == false ? Visibility.Hidden : Visibility.Visible;

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
            => throw new NotImplementedException();
    }


    [ValueConversion(typeof(bool), typeof(Visibility))]
    public class ZeroToVisible : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
            => (int)value == 0 ? Visibility.Visible : Visibility.Hidden;

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
            => throw new NotImplementedException();
    }
}

