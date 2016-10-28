using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevWeather.Models
{
    public class WeatherData
    {
        //public RootObject rootObject { get; set; }
        public string reqLocation { get; set; }
        public string requnits { get; set; }
        public RootObject reqweather { get; set; }

        public WeatherData(string _reqLocation, string _requnits/*, RootObject _reqweather*/)
        {
            reqLocation = _reqLocation;
            requnits = _requnits;
            //reqweather = _reqweather;
        }
    }
}
