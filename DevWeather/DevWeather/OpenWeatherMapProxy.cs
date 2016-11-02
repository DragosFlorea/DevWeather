using DevWeather.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace DevWeather
{


    //https://channel9.msdn.com/Series/Windows-10-development-for-absolute-beginners/UWP-058-UWP-Weather-Setup-and-Working-with-the-Weather-API
    public class OpenWeatherMapProxy
    {
        public const string metric = "metric";
        public const string imperial = "imperial";
        private static string token = "0a08a218caa65034f65bba26714aec11";
        public async static Task<RootObject> GetWeather(/*double lat, double lon,*/ string city, bool units)
        {
            var http = new HttpClient();
            HttpResponseMessage response = null;
            //var url =String.Format("http://api.openweathermap.org/data/2.5/weather?lat={0}&lon={1},gb&appid={2}&units={3}",lat,lon,token,units);
            var url = String.Format("http://api.openweathermap.org/data/2.5/weather?q={0},gb&appid={1}&units={2}", city, token, convertBoolToString(units));
            response = await http.GetAsync(url);

           
            var result = await response.Content.ReadAsStringAsync();
            var serializer = new DataContractJsonSerializer(typeof(RootObject));

            var ms = new MemoryStream(Encoding.UTF8.GetBytes(result));
            var data = (RootObject)serializer.ReadObject(ms);

            return data;
        }

        private static string convertBoolToString(bool status)
        {
            if (status) return imperial;
            else return metric;
        }
        private string getUrl(System.Net.HttpStatusCode responseType, string city,string units)
        {
            string url="";
            switch(responseType)
            {
                case System.Net.HttpStatusCode.Unauthorized: url = String.Format("http://api.openweathermap.org/data/2.5/weather?q={0},gb&appid={1}aec11&units={2}", city, token, units); break;
            }
            return url;
        }
    }
    

    

   
    

   
    

    
}
