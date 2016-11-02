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
    [DataContract, KnownType(typeof(ViewModelBase))]
    
    public class WeatherData_VM
    {
        public ViewModelBase A { get; set; }
        private WeatherData weatherData;   
        public WeatherData_VM(WeatherData _weather)
        {
            this.weatherData = _weather;
        }
        [DataMember]
        public string Reqlocation
        {
            get { return this.weatherData.reqLocation; }
            set { weatherData.reqLocation = value;
                A.RaisePropertyChanged("Reqlocation");
            }
        }
        [DataMember]
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
                A.RaisePropertyChanged("ReqIcon");
            }
        }
        [DataMember]
        public RootObject ReqWeather
        {
            get { return this.weatherData.reqweather; }
            set
            {
                weatherData.reqweather = value;
                A.RaisePropertyChanged("ReqWeather");
            }
        }

        
        
    }
}
