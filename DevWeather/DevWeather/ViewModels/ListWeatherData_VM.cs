using DevWeather.Models;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevWeather.ViewModels
{
    public class ListWeatherData_VM:ViewModelBase
    {
        private ObservableCollection<WeatherData_VM> listWeatherData= new ObservableCollection<WeatherData_VM>();
        private bool toggleStatus;
        public ObservableCollection<WeatherData_VM> ListWeatherData
        {
            get { return listWeatherData; }
            set
            {
                listWeatherData = value;
                RaisePropertyChanged("ListWeatherData");
            }
        }
        public void getToggleStatus(bool _status)
        {
            toggleStatus = _status;
        }
        public async void init()
        {
            var newItem = new WeatherData_VM(new WeatherData("Londra", "sfdsf"));
            newItem.ReqWeather = await requestWeather(newItem);
            listWeatherData.Add(newItem);
            newItem=new WeatherData_VM(new WeatherData("Paris", "sfdsf"));
            newItem.ReqWeather = await requestWeather(newItem);
            listWeatherData.Add(newItem);
        }
        public ListWeatherData_VM()
        {
             init();
        }

        public async Task requestWeather_item(WeatherData_VM item)
        {
            item.ReqWeather = await OpenWeatherMapProxy.GetWeather(item.Reqlocation, false);
            
        }
        public async Task<RootObject> requestWeather(WeatherData_VM item)
        {      
            item.ReqWeather =  await OpenWeatherMapProxy.GetWeather(item.Reqlocation, false);
            return item.ReqWeather;
        }
        public async Task add(string location,string units)
        {
            var newItem = new WeatherData_VM(new WeatherData(location, "celsius"));


            newItem.ReqWeather = await requestWeather(newItem);
            listWeatherData.Add(newItem);
        }
    }
}
