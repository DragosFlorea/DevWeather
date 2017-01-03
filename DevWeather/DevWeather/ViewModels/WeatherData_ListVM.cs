using DevWeather.Models;
using DevWeather.Models.GeoName;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Microsoft.Practices.ServiceLocation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml.Media.Imaging;

namespace DevWeather.ViewModels
{
    public class WeatherData_ListVM : ViewModelBase
    {

        private WeatherData weatherData;
       private ListWeatherData_VM ListPageInstance = ServiceLocator.Current.GetInstance<ListWeatherData_VM>();

        public WeatherData_ListVM(WeatherData _weather)
        {
            this.weatherData = _weather;
            this.RemoveCommand = new RelayCommand<object>((obj) => RemoveLocation(obj), CanRemove);
        }

        public ICommand RemoveCommand { get; }
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

        private bool CanRemove(object b)
        {
            return true;
        }

        public void RemoveLocation(object x)
        {
            ListPageInstance.removeItem((string)x);
            
           
        }
    }
}
