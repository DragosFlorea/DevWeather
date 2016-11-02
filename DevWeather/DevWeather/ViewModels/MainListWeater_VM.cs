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
    public class MainListWeater_VM: ViewModelBase
    {
        private ObservableCollection<WeatherData_VM> mainlistWeatherData = new ObservableCollection<WeatherData_VM>();
        public ObservableCollection<WeatherData_VM> MainListWeatherData
        {
            get { return mainlistWeatherData; }
            set
            {
                mainlistWeatherData = value;
                RaisePropertyChanged("MainListWeatherData");
            }
        }
        private int itemSelectedIndex;
        public int ItemSelectedIndex
        {
            get { return itemSelectedIndex; }
            set
            {
                itemSelectedIndex = value;
                RaisePropertyChanged("ItemSelectedIndex");
            }
        }
        public void SetpivotIndex(int index)
        { itemSelectedIndex=index; }

        public void init()
        {
            ViewModelLocator x = new ViewModelLocator();
            mainlistWeatherData = x.ListPageInstance.ListWeatherData;
        }
        public MainListWeater_VM()
        {
            init();
        }

        public async Task<RootObject> requestWeather(WeatherData_VM item, bool units)
        {
            item.ReqWeather = await OpenWeatherMapProxy.GetWeather(item.Reqlocation, units);
            return item.ReqWeather;
        }
    }
}
