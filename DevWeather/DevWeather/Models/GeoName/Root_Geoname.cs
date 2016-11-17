using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DevWeather.Models.GeoName
{
    [DataContract]
    public class Root_Geoname
    {
        [DataMember]
        public int totalResultsCount { get; set; }
        [DataMember]
        public List<Geoname> geonames { get; set; }
    }
}
