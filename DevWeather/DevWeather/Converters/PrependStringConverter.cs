using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace DevWeather.Converters
{
  public class PrependStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if((string)parameter=="Max")
            return "Max Temp"+value.ToString() + "°";
           else if ((string)parameter == "Min")
                return "Min Temp" + value.ToString() + "°";
            else
                return value.ToString() + "°";
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            // implement for two-way convertion
            throw new NotImplementedException();
        }
    }
}
