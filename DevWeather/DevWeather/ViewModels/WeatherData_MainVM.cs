﻿using DevWeather.Models;
using DevWeather.Models.Forecast;
using DevWeather.Models.GeoName;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;

namespace DevWeather.ViewModels
{
    public class WeatherData_MainVM: ViewModelBase
    {

        private WeatherData weatherData;

        public WeatherData_MainVM(WeatherData _weather)
        {
            this.weatherData = _weather;
        }

        public string Reqlocation
        {
            get { return this.weatherData.reqLocation; }
            set
            {
                weatherData.reqLocation = value;
                RaisePropertyChanged("Reqlocation");
            }
        }
        public BitmapImage ReqIcon
        {
            get
            {
                if (weatherData.reqweather != null)
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
        private Forecast_Root forecastData;
        public Forecast_Root ReqForecast
        {
            get { return this.forecastData; }
            set
            {
                forecastData = value;
                RaisePropertyChanged("ReqForecast");
            }
        }
        private Geoname geonameData;
        private Root_Geoname geonameRootData;
        public Geoname GeonameData
        {
            get { return geonameData; }
            set
            {
                geonameData = value;
                RaisePropertyChanged("GeonameData");
            }
        }
        public Root_Geoname GeonameRootData
        {
            get { return geonameRootData; }
            set
            {
                geonameRootData = value;
                //  GeonameList = GeonameRootData.geonames;
                RaisePropertyChanged("GeonameRootData");
            }
        }
        private bool unit;
        public bool Units_1
        {
            get { return unit; }
            set
            {
                unit = value;
                RaisePropertyChanged("Units_1");
            }
        }
    }
}
