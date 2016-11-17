using DevWeather.Models.GeoName;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;

namespace DevWeather
{
    public class LocationManager
    {
        public async Task<Geoposition> GetPosition()
        {
            Geoposition pos = null ;
            try
            {
                // Request permission to access location
                var accessStatus = await Geolocator.RequestAccessAsync();

                switch (accessStatus)
                {
                    case GeolocationAccessStatus.Allowed:

                        // If DesiredAccuracy or DesiredAccuracyInMeters are not set (or value is 0), DesiredAccuracy.Default is used.
                        Geolocator geolocator = new Geolocator { DesiredAccuracyInMeters = 0 };
                        // Carry out the operation
                        pos = await geolocator.GetGeopositionAsync();
                        break;

                    case GeolocationAccessStatus.Denied:
                        pos = null;
                        break;

                    case GeolocationAccessStatus.Unspecified:
                        pos = null;
                        break;
                }
                
            }
            catch { }
            return pos;
        }

        public async static Task<Root_Geoname> GetCity(string city)
        {
            var token = "Dregosh";
            var httpClient = new HttpClient();
            HttpResponseMessage response  = null;
            var url = String.Format("http://api.geonames.org/searchJSON?q={0}&maxRows=3&username={1}", city, token);
            response = await httpClient.GetAsync(url);
            var result = await response.Content.ReadAsStringAsync();
            var serializer = new DataContractJsonSerializer(typeof(Root_Geoname));
            var ms = new MemoryStream(Encoding.UTF8.GetBytes(result));
            var data = (Root_Geoname)serializer.ReadObject(ms);

            return data;
        }
    }
}
