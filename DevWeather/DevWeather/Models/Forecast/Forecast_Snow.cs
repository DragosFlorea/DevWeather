using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DevWeather.Models.Forecast
{
    [DataContract]
  public  class Forecast_Snow
    {
        [DataMember]
        public double __invalid_name__3h { get; set; }
    }
}
