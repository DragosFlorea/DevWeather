using DevWeather.Models;
using DevWeather.Models.Forecast;
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
using Windows.UI.Xaml.Media.Imaging;

namespace DevWeather.ViewModels
{
    public class MainListWeater_VM: ViewModelBase
    {
        private ObservableCollection<WeatherData_MainVM> mainlistWeatherData = new ObservableCollection<WeatherData_MainVM>();
        private ListWeatherData_VM ListPageInstance = ServiceLocator.Current.GetInstance<ListWeatherData_VM>();
        private INavigationService _navigationService;
        
        public ObservableCollection<WeatherData_MainVM> MainListWeatherData
        {
            get { return mainlistWeatherData; }
            set
            {
                mainlistWeatherData = value;
                RaisePropertyChanged("MainListWeatherData");
            }
        }
        private int _itemSelectedIndex;
        public int ItemSelectedIndex
        {
            get { return _itemSelectedIndex; }
            set
            {
                _itemSelectedIndex = value;
                RaisePropertyChanged("ItemSelectedIndex");
            }
        }
        public void SetpivotIndex(int index)
        { _itemSelectedIndex = index; }

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
            if (MainListWeatherData != null && MainListWeatherData.Count != 0)
            {
                foreach (var vm in MainListWeatherData)
                {
                    vm.Units_1 = unit;
                }
            }
        }
        private object _geolocation = new object();
        public object Geolocation
        {
            get { return _geolocation; }
            set { _geolocation = value; }
        }

        public async Task Updatedata()
        {
            WeatherData_MainVM newItem = new WeatherData_MainVM(new WeatherData());
            MainListWeatherData.Clear();
            var location = await LocationManager.GetPosition();
            Geolocation = await ProcessData.setGeolocation(location.Coordinate.Latitude, location.Coordinate.Longitude,  "Main", ListPageInstance.Requnits);           
            MainListWeatherData.Add((WeatherData_MainVM)Geolocation);
            ReadFromFile = await ProcessData.init();
            if (ReadFromFile)
            {
                foreach (var item in ProcessData.LocToStorage.LocationList)
                {
                    newItem = new WeatherData_MainVM(new WeatherData(item));
                    newItem = (WeatherData_MainVM)await ProcessData.GetWeatherAndForecast(item, 0, 0, ListPageInstance.Requnits);
                    MainListWeatherData.Add(newItem);
                }
                setUnits(ListPageInstance.Requnits);
            }
        }
        private bool firstuse = false;
        public async Task FirstInit()
        {
            if (!firstuse)
            {
                firstuse = true;
                await LocationsToStorage.CreateFileLocation();
                WeatherData_MainVM newItem = new WeatherData_MainVM(new WeatherData());
                MainListWeatherData.Clear();
                var location = await LocationManager.GetPosition();
                this.Geolocation = await ProcessData.setGeolocation(location.Coordinate.Latitude, location.Coordinate.Longitude, "Main", ListPageInstance.Requnits);
                ListPageInstance.LIST_Geolocation = await ProcessData.setGeolocation(location.Coordinate.Latitude, location.Coordinate.Longitude, "Main", ListPageInstance.Requnits);
                MainListWeatherData.Add((WeatherData_MainVM)Geolocation);
                ReadFromFile = await ProcessData.init();
                if (ReadFromFile)
                {
                    foreach (var item in ProcessData.LocToStorage.LocationList)
                    {
                        newItem = new WeatherData_MainVM(new WeatherData(item));
                        newItem = (WeatherData_MainVM)await ProcessData.GetWeatherAndForecast(item, 0, 0, ListPageInstance.Requnits);
                        MainListWeatherData.Add(newItem);
                    }
                    setUnits(ListPageInstance.Requnits);
                }            
            }
           
        }
        
        public MainListWeater_VM( INavigationService navigationService)
        {
            _navigationService = navigationService;
            this.NavToLocationsCommand = new RelayCommand(Navigate, CanNavigateToLocations);
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
