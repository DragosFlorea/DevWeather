using DevWeather.Models;
using DevWeather.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;

namespace DevWeather
{
    public class ProcessData
    {
        private static LocationsToStorage locToStorage = new LocationsToStorage();

        public static LocationsToStorage LocToStorage
        {
            get { return locToStorage; }
            set { locToStorage = value; }
        }


        public static async Task<bool> init()
        {
            await locToStorage.getDataFromFile();
            if (locToStorage.LocationList != null)
                return true;
            else
                return false;
        }

        public static async Task<object> setGeolocation(double lat, double lng, string type, bool unit)
        {
            string icon;
            WeatherData_MainVM item_mainVm = new WeatherData_MainVM(new WeatherData()); 
            WeatherData_ListVM item_listvm = new WeatherData_ListVM(new WeatherData());
            switch (type)
            {
                case "Main":                                     
                    item_mainVm.ReqWeather = await OpenWeatherMapProxy.GetWeather_byCoord(lat, lng, unit);
                    item_mainVm.Reqlocation = item_mainVm.ReqWeather.name;
                    item_mainVm.Units_1 = unit;
                    icon = String.Format("ms-appx:///Assets/{0}.png", item_mainVm.ReqWeather.weather[0].icon);
                    item_mainVm.ReqIcon = new BitmapImage(new Uri(icon, UriKind.Absolute));
                    item_mainVm.ReqForecast = await OpenWeatherMapProxy.GetForecast(lat, lng, unit);
                    break;
                case "List":                   
                    item_listvm.ReqWeather = await OpenWeatherMapProxy.GetWeather_byCoord(lat, lng, unit);
                    item_listvm.Reqlocation = item_listvm.ReqWeather.name;
                     icon = String.Format("ms-appx:///Assets/{0}.png", item_listvm.ReqWeather.weather[0].icon);
                    item_listvm.ReqIcon = new BitmapImage(new Uri(icon, UriKind.Absolute));
                    
                    //((WeatherData_ListVM)item).Units_1 = unit;
                    break;
                default:
                    //null case not covered
                    break;
            }

            if (type == "Main")
                return item_mainVm;
            else
                return item_listvm;
        }

        public static async Task<object> GetWeatherAndForecast(string location, double lat, double lng, bool unit)
        {
            string icon;
            WeatherData_MainVM item_mainVm = new WeatherData_MainVM(new WeatherData());
                    item_mainVm.ReqWeather = await OpenWeatherMapProxy.GetWeather(location, unit);
                    item_mainVm.Reqlocation = item_mainVm.ReqWeather.name;
                    item_mainVm.Units_1 = unit;
                    icon = String.Format("ms-appx:///Assets/{0}.png", item_mainVm.ReqWeather.weather[0].icon);
                    item_mainVm.ReqIcon = new BitmapImage(new Uri(icon, UriKind.Absolute));
                    item_mainVm.ReqForecast = await OpenWeatherMapProxy.GetForecast(item_mainVm.ReqWeather.coord.lat, item_mainVm.ReqWeather.coord.lon, unit);

                return item_mainVm;

        }

    }
}
