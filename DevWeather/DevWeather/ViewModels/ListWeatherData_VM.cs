using DevWeather.Converters;
using DevWeather.Models;
using DevWeather.Models.GeoName;
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
using Windows.UI.Xaml.Controls;

namespace DevWeather.ViewModels
{
   
    public class ListWeatherData_VM:ViewModelBase
    {


        private LocationsToStorage locToStorage = new LocationsToStorage();
        private MainListWeater_VM MainPageInstance;
        private readonly INavigationService _navigationService;
        private ObservableCollection<Geoname> geonameDataList;
        private Geoname geonameData;
        private Root_Geoname geonameRootData;
        private ObservableCollection<string> cities = new ObservableCollection<string>();
        /// <summary>
        /// PROPERTIES OF LISTWEATHERDATA_VM
        /// </summary>
        
        /// <summary>
        /// List which is used for binding the listbox
        /// </summary>
        private ObservableCollection<WeatherData_ListVM> listWeatherData= new ObservableCollection<WeatherData_ListVM>();
        public ObservableCollection<WeatherData_ListVM> ListWeatherData
        {
            get { return listWeatherData; }
            set
            {
                listWeatherData = value;
                RaisePropertyChanged("ListWeatherData");
                
            }
        }
        private ObservableCollection<WeatherData_ListVM> geoLocationList = new ObservableCollection<WeatherData_ListVM>();
        public ObservableCollection<WeatherData_ListVM> GeoLocationList
        {
            get { return geoLocationList; }
            set
            {
                geoLocationList = value;
                RaisePropertyChanged("GeoLocationList");

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
                
                _navigationService.NavigateTo("FirstPage");
               MainPageInstance.SetpivotIndex(ItemSelectedIndex);

            }
        }
        /// <summary>
        /// Binding to the Location
        /// </summary>
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

        /// <summary>
        /// Units property binding
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
        /// Set visibility of bullet of delete
        /// </summary>
        private bool _isVisible;
        public bool IsVisible
        {
            get { return _isVisible; }
            set
            {
                _isVisible = value;
                setVisibility(value);
                RaisePropertyChanged(nameof(IsVisible));
            }
        }

        public ObservableCollection<Geoname> GeonameList
        {
            get { return geonameDataList; }
            set
            {
                geonameDataList = value;
                RaisePropertyChanged("GeonameList");
            }
        }
        private string autoSuggestBoxText;
        public string AutoSuggestBoxText
        {
            get { return autoSuggestBoxText; }
            set
            {
                autoSuggestBoxText = value;
                RaisePropertyChanged(nameof(AutoSuggestBoxText));
            }
        }
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
                GeonameList = GeonameRootData.geonames;
                RaisePropertyChanged("GeonameRootData");
            }
        }
        public ObservableCollection<string> Cities
        {
            get { return cities; }
            set
            {
                cities = value;
                RaisePropertyChanged("Cities");
            }
        }

        /// <summary>
        /// FUNCTIONS
        /// </summary>
        public void setVisibility(bool Visibility)
        {
            if (ListWeatherData != null && ListWeatherData.Count != 0)
            {
                foreach (var vm in ListWeatherData)
                {
                    vm.IsVisible = IsVisible;
                }
            }
        }
        /// <summary>
        /// Refresh function which shall be called on unit change
        /// </summary>
        /// <returns></returns>
        public async Task Update()
        {

            listWeatherData.Clear();
            WeatherData_ListVM newItem = new WeatherData_ListVM(new WeatherData());

            var location = await LocationManager.GetPosition();
            var ListWeatherElement = await ProcessData.setGeolocation(location.Coordinate.Latitude, location.Coordinate.Longitude,  "List", Requnits);
            listWeatherData.Add((WeatherData_ListVM)ListWeatherElement);
            if(ProcessData.LocToStorage.LocationList!=null)
            { 
                foreach (var item in ProcessData.LocToStorage.LocationList)
                {
                    newItem = new WeatherData_ListVM(new WeatherData(item));               
                    newItem.ReqWeather = await requestWeather(newItem, Requnits);
                    listWeatherData.Add(newItem);
                }
            }
        }
        private object _listgeolocation = new object();
        public object LIST_Geolocation
        {
            get { return _listgeolocation; }
            set { _listgeolocation = value; }
        }
        private bool firstuse = false;
        /// <summary>
        /// Populate ListWeatherData list
        /// </summary>
        public async Task init()
        {
            itemSelectedIndex = -1;
            if (!firstuse)
            {
                listWeatherData.Clear();
                this.MainPageInstance = ServiceLocator.Current.GetInstance<MainListWeater_VM>();
                WeatherData_ListVM newItem = new WeatherData_ListVM(new WeatherData());
                listWeatherData.Add((WeatherData_ListVM)LIST_Geolocation);

                if (ProcessData.LocToStorage.LocationList != null)
                    foreach (var item in ProcessData.LocToStorage.LocationList)
                    {
                        newItem = new WeatherData_ListVM(new WeatherData(item));
                        newItem.ReqWeather = await requestWeather(newItem, Requnits);
                        listWeatherData.Add(newItem);
                    }
            }
        }
     
        private bool readFromFile = false;

        public bool ReadFromFile
        {
            get { return readFromFile; }
            set { readFromFile = value; }
        }

        /// <summary>
        /// get weather from api
        /// </summary>
        /// <param name="item"></param>
        /// <param name="units"></param>
        /// <returns></returns>
        public async Task requestWeather_item(WeatherData_ListVM item, bool units)
        {
            item.ReqWeather = await OpenWeatherMapProxy.GetWeather(item.Reqlocation, units);

        }
        /// <summary>
        /// get weather from api
        /// </summary>
        /// <param name="item"></param>
        /// <param name="units"></param>
        /// <returns >RootObject</returns>
        public async Task<RootObject> requestWeather(WeatherData_ListVM item, bool units)
        {
            item.ReqWeather = await OpenWeatherMapProxy.GetWeather(item.Reqlocation, units);
            return item.ReqWeather;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="city"></param>
        /// <returns></returns>
        public async Task<Root_Geoname> requestCity(string city)
        {
            GeonameRootData = await LocationManager.GetCity(city);
            return GeonameRootData;
        }
        public async void removeItem(string location)
        {
            foreach (var item in ListWeatherData)
            {
                if (item.Reqlocation == location)
                {
                    ListWeatherData.Remove(item);
                    ProcessData.LocToStorage.LocationList.Remove(location);
                    await ProcessData.LocToStorage.SaveListofLocation();
                    break;
                }
            }
        }

        /// <summary>
        /// Constructor ListWeatherData_VM
        /// </summary>
        public ListWeatherData_VM(INavigationService navigationService)
        {          
            _navigationService = navigationService;            
            this.AddCommand = new RelayCommand(async () => await add(), CanAdd);
            this.TextChangedCommand = new RelayCommand(async () => await TextChange(), CanTextChange);
            this.QuerySubmittedCommand = new RelayCommand<object>(async (s) => await QuerySubmitted(s), CanSuggestionChosen);
            this.SuggestionChosenCommand = new RelayCommand<object>( (s) =>  SuggestionChosen(s), CanQuerySubmitted);
            this.ToggleSwitchCommand = new RelayCommand(async () => await ToggleSwitch(), CanToggleSwitch);            
        }
        /// <summary>
        /// Constructors
        /// </summary>





        /// <summary>
        /// Start - Defines of all commands
        /// </summary>
        public ICommand AddCommand { get; }
        public ICommand TextChangedCommand { get; }
        public ICommand QuerySubmittedCommand { get; }
        public ICommand SuggestionChosenCommand { get; }
        public ICommand ToggleSwitchCommand { get; }
        
        /// <summary>
        /// End - Defines of all commands
        /// </summary>
        /// <returns></returns>


        /// <summary>
        /// CanAdd from AddCommand
        /// </summary>
        /// <returns></returns>
        private bool CanAdd()
        {
            return true;
        }
        /// <summary>
        /// CanTextChange from TextChangeCommand
        /// </summary>
        /// <returns></returns>
        private bool CanTextChange()
        {
            return true;
        }
        /// <summary>
        /// CanQuerySubmitted from QuerySubmittedCommand
        /// </summary>
        /// <param name="b"></param>
        /// <returns></returns>
        private bool CanQuerySubmitted(object b)
        {
            return true;
        }
        /// <summary>
        /// CanSuggestionChosen from SuggestionChosenCommand
        /// </summary>
        /// <param name="b"></param>
        /// <returns></returns>
        private bool CanSuggestionChosen(object b)
        {
            return true;
        }
        /// <summary>
        /// CanToggleSwitch from ToggleSwitchCommand
        /// </summary>
        /// <returns></returns>
        private bool CanToggleSwitch()
        {
            return true;
        }
       


        /// <summary>
        /// Add new location
        /// </summary>
        /// <param name="location"></param>
        /// <param name="units"></param>
        /// <returns></returns>
        public async Task add()
        {
            //WeatherData_ListVM newItem = new WeatherData_ListVM(new WeatherData(location));
            //newItem.ReqWeather = new RootObject();
            //newItem.ReqWeather = await requestWeather(newItem, Requnits);
            //listWeatherData.Add(newItem);
            if (ProcessData.LocToStorage.LocationList != null)
                ProcessData.LocToStorage.LocationList.Add(location);
            else
            { 
                ProcessData.LocToStorage.LocationList = new ObservableCollection<string>();
                ProcessData.LocToStorage.LocationList.Add(location);
            }
            await ProcessData.LocToStorage.SaveListofLocation();
            await Update();
            await MainPageInstance.Updatedata();
        }
        public async Task TextChange()
        {

            Cities.Clear();
            var suggestions = GeonameRootData = await requestCity((AutoSuggestBoxText));
            foreach (var item in suggestions.geonames)
            {
                Cities.Add(item.name);
            }           
        }
        public async Task QuerySubmitted(object args)
        {
            if (((AutoSuggestBoxQuerySubmittedEventArgs)args).ChosenSuggestion != null )
            {
                //User selected an item, take an action
                Location= ((AutoSuggestBoxQuerySubmittedEventArgs)args).QueryText;
               // await add();
            }
            else if (!string.IsNullOrEmpty(((AutoSuggestBoxQuerySubmittedEventArgs)args).QueryText))
            {
                //Do a fuzzy search based on the text
                Cities.Clear();
                var suggestions = GeonameRootData = await requestCity((AutoSuggestBoxText));
                foreach (var item in suggestions.geonames)
                {
                    Cities.Add(item.name);
                }
            }
        }
        public void SuggestionChosen(object args)
        {
            var control = ((AutoSuggestBoxSuggestionChosenEventArgs)args).SelectedItem;

            //Don't autocomplete the TextBox when we are showing "no results"
            if (control != null)
            {
                AutoSuggestBoxText = (string) control;
            }
        }
        public async Task ToggleSwitch()
        {
            await MainPageInstance.Updatedata();
            await Update();
            IsVisible = false;
        }
      
    }
}