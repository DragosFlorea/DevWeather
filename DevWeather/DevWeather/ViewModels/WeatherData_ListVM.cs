using DevWeather.Models;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;

namespace DevWeather.ViewModels
{
    public class WeatherData_ListVM : ViewModelBase
    {

        private WeatherData weatherData;

        public WeatherData_ListVM(WeatherData _weather)
        {
            this.weatherData = _weather;
        }

        public string Reqlocation
        {
            get { return this.weatherData.reqLocation; }
            set { weatherData.reqLocation = value;
                RaisePropertyChanged("Reqlocation");
            }
        }
        public BitmapImage ReqIcon
        {
            get { if (weatherData.reqweather != null)
                {
                    string icon = String.Format("ms-appx:///Assets/{0}.png", weatherData.reqweather.weather[0].icon);

                    return new BitmapImage(new Uri(icon, UriKind.Absolute));
                }
                else return null;
            }
            
                    
            set
            {
                string icon = String.Format("ms-appx:///Assets/{0}.png", value);
                new BitmapImage(new Uri(icon, UriKind.Absolute));
               RaisePropertyChanged("ReqIcon");
            }
        }
        public RootObject ReqWeather
        {
            get { return this.weatherData.reqweather; }
            set
            {
                weatherData.reqweather = value;
                RaisePropertyChanged("ReqWeather");
            }
        }
        private bool _isVisible;
        public bool IsVisible
        {
            get { return _isVisible; }
            set
            {
                _isVisible = value;
                RaisePropertyChanged(nameof(IsVisible));
            }
        }
    }
}
