using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DevWeather.Models
{
    public class WeatherData
    {
        //public RootObject rootObject { get; set; }
        public string reqLocation { get; set; }
        public bool requnits { get; set; }
        public RootObject reqweather { get; set; }

        public WeatherData(string _reqLocation)
        {
            reqLocation = _reqLocation;
        }

            public WeatherData() {}
    }
}
