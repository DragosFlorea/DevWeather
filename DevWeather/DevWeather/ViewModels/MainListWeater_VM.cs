using DevWeather.Models;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using Microsoft.Practices.ServiceLocation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DevWeather.ViewModels
{
    public class MainListWeater_VM: ViewModelBase
    {
        private ObservableCollection<WeatherData_VM> mainlistWeatherData = new ObservableCollection<WeatherData_VM>();
        private ListWeatherData_VM ListPageInstance = ServiceLocator.Current.GetInstance<ListWeatherData_VM>();
        private INavigationService _navigationService;
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

        private LocationsToStorage locToStorage = new LocationsToStorage();

        public LocationsToStorage LocToStorage
        {
            get { return locToStorage; }
            set
            {
                locToStorage = value;
                RaisePropertyChanged("LocToStorage");
            }
        }

        private bool readFromFile = false;

        public bool ReadFromFile
        {
            get { return readFromFile; }
            set { readFromFile = value; }
        }

      

        public void setUnits(bool unit)
        {
            if (mainlistWeatherData != null && mainlistWeatherData.Count != 0)
            {
                foreach (var vm in mainlistWeatherData)
                {
                    vm.Units_1 = unit;
                }
            }
        }

        public async Task init()
        {
            if (readFromFile == false)
            {
                await locToStorage.getDataFromFile();
                readFromFile = true;
            }
            mainlistWeatherData.Clear();
                foreach (var item in locToStorage.LocationList)
                {
                    var newItem = new WeatherData_VM(new WeatherData(item));
                    newItem.ReqWeather = await requestWeather(newItem, newItem.Units_1);
                    mainlistWeatherData.Add(newItem);
                }
            
        }
        public MainListWeater_VM( INavigationService navigationService)
        {
            _navigationService = navigationService;

            this.NavToLocationsCommand = new RelayCommand(Navigate, CanNavigateToLocations);
          //  ListPageInstance.init();
        }

        public async Task<RootObject> requestWeather(WeatherData_VM item, bool units)
        {
            item.ReqWeather = await OpenWeatherMapProxy.GetWeather(item.Reqlocation, units);
            return item.ReqWeather;
        }

        public ICommand NavToLocationsCommand { get; }
        private bool CanNavigateToLocations()
        {
            return true;
        }

        private void Navigate()
        {
            _navigationService.NavigateTo("SecondPage");
        }
    }
}
