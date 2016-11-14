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

namespace DevWeather.Models
{
    [DataContract]
    public class LocationsToStorage
    {

        private ObservableCollection<string> locationList = new ObservableCollection<string>();
        [DataMember]
        public ObservableCollection<string> LocationList
        {
            get { return locationList; }
            set
            {
                locationList = value;            
            }
        }

        public async Task SaveListofLocation()
        {
            StorageFile userListofLocations = await ApplicationData.Current.LocalFolder.CreateFileAsync("userListofLocation", CreationCollisionOption.ReplaceExisting);
            IRandomAccessStream raStream = await userListofLocations.OpenAsync(FileAccessMode.ReadWrite);
            using (IOutputStream outStream = raStream.GetOutputStreamAt(0))
            {
                DataContractSerializer serializer = new DataContractSerializer(typeof(ObservableCollection<string>));
                serializer.WriteObject(outStream.AsStreamForWrite(), locationList);
                await outStream.FlushAsync();
                outStream.Dispose();
                raStream.Dispose();
            }
        }
        private async Task<ObservableCollection<string>> readListofLocation()
        {
            var Serializer = new DataContractSerializer(typeof(ObservableCollection<string>));
            using (var stream = await ApplicationData.Current.LocalFolder.OpenStreamForReadAsync("userListofLocation"))
            {
                var list = (ObservableCollection<string>)Serializer.ReadObject(stream);
                return list;
            }

        }

        public async Task getDataFromFile()
        {
            locationList = await readListofLocation();
        }
    
    }
}
