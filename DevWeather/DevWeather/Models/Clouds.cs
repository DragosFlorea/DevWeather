using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DevWeather.Models
{
    [DataContract]
    public class Clouds
    {
        [DataMember]
        public int all { get; set; }
    }
}
