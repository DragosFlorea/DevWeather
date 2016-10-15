using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;

namespace DevWeather
{
    public class LocationManager
    {
        public async static Task<Geoposition> GetPosition()
        {
            var accesStatus= await Geolocator.RequestAccessAsync();
            if (accesStatus != GeolocationAccessStatus.Allowed) throw new Exception();

            var geolocator = new Geolocator { DesiredAccuracyInMeters = 0 };
            var position = await geolocator.GetGeopositionAsync();
            return position;
        }
    }
}
