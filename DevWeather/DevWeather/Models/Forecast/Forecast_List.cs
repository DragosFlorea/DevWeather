using DevWeather.Models.Forecast;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DevWeather.Models.Forecast
{
    [DataContract]
    public class Forecast_List
    {
        [DataMember]
        public int dt { get; set; }
        [DataMember]
        public Forecast_Main main { get; set; }
        [DataMember]
        public List<Forecast_Weather> weather { get; set; }
        [DataMember]
        public Forecast_Clouds clouds { get; set; }
        [DataMember]
        public Forecast_Wind wind { get; set; }
        [DataMember]
        public Forecast_Sys2 sys { get; set; }
        [DataMember]
        public string dt_txt { get; set; }
        [DataMember]
        public Forecast_Rain rain { get; set; }
        [DataMember]
        public Forecast_Snow snow { get; set; }
    }
}
