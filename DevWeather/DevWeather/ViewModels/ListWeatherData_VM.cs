using DevWeather.Models;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using Microsoft.Practices.ServiceLocation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.Storage;
using Windows.Storage.Streams;

namespace DevWeather.ViewModels
{
   
    public class ListWeatherData_VM:ViewModelBase
    {
        

        private ObservableCollection<WeatherData_VM> listWeatherData= new ObservableCollection<WeatherData_VM>();
       // private LocationsToStorage locToStorage = new LocationsToStorage();
        private MainListWeater_VM MainPageInstance;
        private readonly INavigationService _navigationService;

        


        /// <summary>
        /// List which is used for binding the listbox
        /// </summary>
        public ObservableCollection<WeatherData_VM> ListWeatherData
        {
            get { return listWeatherData; }
            set
            {
                listWeatherData = value;
                RaisePropertyChanged("ListWeatherData");
                
            }
        }

        /// <summary>
        /// Binding property for selected index which is passed to MainPage
        /// </summary>
        private int itemSelectedIndex;
        public int ItemSelectedIndex
        {
            get { return itemSelectedIndex; }
            set
            {
                    itemSelectedIndex = value;
                    RaisePropertyChanged("ItemSelectedIndex");
                //this.MainPageInstance = ServiceLocator.Current.GetInstance<MainListWeater_VM>();
                MainPageInstance.SetpivotIndex(ItemSelectedIndex);
                    itemSelectedIndex = -1;
                    _navigationService.NavigateTo("FirstPage");
            }
        }
        private string location;
        public string Location
        {
            get { return location; }
            set
            {
                location = value;
                RaisePropertyChanged("Location");
            }
        }

        /// <summary>
        /// Binding property to the toggle switch
        /// </summary>
        private bool toggleStatus;
        public bool Requnits
        {
            get { return toggleStatus; }
            set
            {
                toggleStatus = value;
                MainPageInstance.setUnits( value);
                RaisePropertyChanged("Requnits");
            }
        }
        
        /// <summary>
        /// Refresh function which shall be called on unit change
        /// </summary>
        /// <returns></returns>
        public async Task GetWeatherData_again()
        {

            listWeatherData.Clear();
            foreach (var item in MainPageInstance.LocToStorage.LocationList)
            {
                var newItem = new WeatherData_VM(new WeatherData(item));
                
                newItem.ReqWeather = await requestWeather(newItem, Requnits);
                listWeatherData.Add(newItem);

            }
        }
        /// <summary>
        /// Populate ListWeatherData list
        /// </summary>
        public async Task init()
        {
            this.MainPageInstance = ServiceLocator.Current.GetInstance<MainListWeater_VM>();
            listWeatherData.Clear();
                if (MainPageInstance.LocToStorage.LocationList.Count != 0)
                    foreach (var item in MainPageInstance.LocToStorage.LocationList)
                    {
                        var newItem = new WeatherData_VM(new WeatherData(item));
                        newItem.ReqWeather = await requestWeather(newItem, Requnits);
                        listWeatherData.Add(newItem);
                    }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public ListWeatherData_VM(INavigationService navigationService)
        {          
            _navigationService = navigationService;            
            this.AddCommand = new RelayCommand(async () => await add(), CanAdd);
            
        }
        /// <summary>
        /// get weather from api
        /// </summary>
        /// <param name="item"></param>
        /// <param name="units"></param>
        /// <returns></returns>
        public async Task requestWeather_item(WeatherData_VM item, bool units)
        {
            item.ReqWeather = await OpenWeatherMapProxy.GetWeather(item.Reqlocation, units);
            
        }
        /// <summary>
        /// get weather from api
        /// </summary>
        /// <param name="item"></param>
        /// <param name="units"></param>
        /// <returns >RootObject</returns>
        public async Task<RootObject> requestWeather(WeatherData_VM item, bool units)
        {      
            item.ReqWeather =  await OpenWeatherMapProxy.GetWeather(item.Reqlocation, units);
            return item.ReqWeather;
        }
        private bool _buttCancel;
        public bool ButtCancel
        {
            get { return _buttCancel; }
            set
            {
                _buttCancel = value;
                RaisePropertyChanged("ButtCancel");
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
        /// <summary>
        /// Add new location
        /// </summary>
        /// <param name="location"></param>
        /// <param name="units"></param>
        /// <returns></returns>
        public async Task add()
        {
            WeatherData_VM newItem = new WeatherData_VM(new WeatherData(location));
            newItem.ReqWeather = new RootObject();
            newItem.ReqWeather = await requestWeather(newItem, Requnits);
            listWeatherData.Add(newItem);
            MainPageInstance.LocToStorage.LocationList.Add(location);
           await MainPageInstance.LocToStorage.SaveListofLocation(); 
        }
        
        public  ICommand AddCommand { get; }        
        private bool CanAdd()
        {
            return true;
        }

    }
}
