﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DevWeather.Models
{
    [DataContract]
    public class RootObject
    {
        [DataMember]
        public Coord coord { get; set; }
        [DataMember]
        public List<Weather> weather { get; set; }
        [DataMember]
        public string @base { get; set; }
        [DataMember]
        public Main main { get; set; }
        [DataMember]
        public Wind wind { get; set; }
        [DataMember]
        public Clouds clouds { get; set; }
        [DataMember]
        public int dt { get; set; }
        [DataMember]
        public Sys sys { get; set; }
        [DataMember]
        public int id { get; set; }
        [DataMember]
        public string name { get; set; }
        [DataMember]
        public int cod { get; set; }
    }
}
