using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DevWeather.Models.GeoName
{
    [DataContract]
        public class Geoname
        {
        [DataMember]
        public string adminCode1 { get; set; }
        [DataMember]
        public string lng { get; set; }
        [DataMember]
        public int geonameId { get; set; }
        [DataMember]
        public string toponymName { get; set; }
        [DataMember]
        public string countryId { get; set; }
        [DataMember]
        public string fcl { get; set; }
        [DataMember]
        public int population { get; set; }
        [DataMember]
        public string countryCode { get; set; }
        [DataMember]
        public string name { get; set; }
        [DataMember]
        public string fclName { get; set; }
        [DataMember]
        public string countryName { get; set; }
        [DataMember]
        public string fcodeName { get; set; }
        [DataMember]
        public string adminName1 { get; set; }
        [DataMember]
        public string lat { get; set; }
        [DataMember]
        public string fcode { get; set; }
        }    
}
