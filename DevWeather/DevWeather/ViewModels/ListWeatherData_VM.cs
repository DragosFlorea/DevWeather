using DevWeather.Models;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Streams;

namespace DevWeather.ViewModels
{
   
    public class ListWeatherData_VM:ViewModelBase
    {
        public ViewModelBase A { get; set; }

        private ObservableCollection<WeatherData_VM> listWeatherData= new ObservableCollection<WeatherData_VM>();
        private bool toggleStatus;
        private string location;
        private int itemSelectedIndex;
        
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
        public int ItemSelectedIndex
        {
            get { return itemSelectedIndex; }
            set
            {
                itemSelectedIndex = value;
                RaisePropertyChanged("ItemSelectedIndex");
            }
        }
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
        public bool Requnits
        {
            get { return toggleStatus; }
            set
            {
                toggleStatus = value;
                RaisePropertyChanged("Requnits");
            }
        }     
        /// <summary>
        /// Refresh function which shall be called on unit change
        /// </summary>
        /// <returns></returns>
        public async Task GetWeatherData_again()
        {
            foreach (var item in ListWeatherData)
            {
                item.ReqWeather = await requestWeather(item, Requnits);
                RaisePropertyChanged("ListWeatherData");
            }
        }

        /// <summary>
        /// Populate ListWeatherData list
        /// </summary>
        public async void init()
        {

 //           listWeatherData = await readListofLocation();
            //var newItem = new WeatherData_VM(new WeatherData("Londra"));
            //newItem.ReqWeather = await requestWeather(newItem, Requnits);
            //listWeatherData.Add(newItem);
            //newItem=new WeatherData_VM(new WeatherData("Paris"));
            //newItem.ReqWeather = await requestWeather(newItem, Requnits);
            //listWeatherData.Add(newItem);
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public ListWeatherData_VM()
        {
             init();
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

        /// <summary>
        /// Add new location
        /// </summary>
        /// <param name="location"></param>
        /// <param name="units"></param>
        /// <returns></returns>
        public async Task add()
        {
            var newItem = new WeatherData_VM(new WeatherData(location));
            newItem.ReqWeather = new RootObject();
            newItem.ReqWeather = await requestWeather(newItem, Requnits);
            listWeatherData.Add(newItem);
            
        }


        public async Task SaveListofLocation()
        {
            StorageFile userListofLocations = await ApplicationData.Current.LocalFolder.CreateFileAsync("userListofLocation", CreationCollisionOption.ReplaceExisting);
            IRandomAccessStream raStream = await userListofLocations.OpenAsync(FileAccessMode.ReadWrite);
            using (IOutputStream outStream = raStream.GetOutputStreamAt(0))
            {
                DataContractSerializer serializer = new DataContractSerializer(typeof(ObservableCollection<WeatherData_VM>));
                serializer.WriteObject(outStream.AsStreamForWrite(), listWeatherData);
                await outStream.FlushAsync();
                outStream.Dispose();   
                raStream.Dispose();
            }
        }
        private async Task<ObservableCollection<WeatherData_VM>> readListofLocation()
        {
            var Serializer = new DataContractSerializer(typeof(ObservableCollection<WeatherData_VM>));
            using (var stream = await ApplicationData.Current.LocalFolder.OpenStreamForReadAsync("userListofLocation"))
            {
                var list = (ObservableCollection<WeatherData_VM>)Serializer.ReadObject(stream);
                return list;
            }
            
        }
    }
}
