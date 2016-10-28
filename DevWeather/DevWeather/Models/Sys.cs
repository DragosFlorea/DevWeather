using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DevWeather.Models
{
    [DataContract]
    public class Sys
    {
        [DataMember]
        public double message { get; set; }
        [DataMember]
        public string country { get; set; }
        [DataMember]
        public int sunrise { get; set; }
        [DataMember]
        public int sunset { get; set; }
    }
}
