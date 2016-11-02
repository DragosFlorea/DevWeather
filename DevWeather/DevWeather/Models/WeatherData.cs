using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DevWeather.Models
{
    [DataContract]
    public class WeatherData
    {
        //public RootObject rootObject { get; set; }
        [DataMember]
        public string reqLocation { get; set; }
        [DataMember]
        public bool requnits { get; set; }
        [DataMember]
        public RootObject reqweather { get; set; }

        public WeatherData(string _reqLocation)
        {
            reqLocation = _reqLocation;
        }
    }
}
