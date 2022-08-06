using Newtonsoft.Json;
using SkinCareWebApp.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;

namespace SkinCareWebApp.Services
{
    public class WeatherService
    {

        private string _Lat { get; set; }
        private string _Lon { get; set; }

        public WeatherService(string _Lat,string _Lon)
        {
            this._Lon = _Lon;
            this._Lat = _Lat;
        }
        public UvModel[] GetHistoricalUvData()
        {
            return null;
        }
        public WeatherModel GetRealTimeWeatherData()
        {
            if (_Lat == null && _Lon == null)
            {
                return GetDefaultWeatherData();
            }
            string url = GetRealTimeWeatherUrl();
            string responseString = GetWeatherResponseString(url);

            WeatherModel weatherData = JsonConvert.DeserializeObject<WeatherModel>(responseString);

            return weatherData;
        }

        private string GetWeatherResponseString(string url)
        {
            var httpRequest = (HttpWebRequest)WebRequest.Create(url);


            var httpResponse = (HttpWebResponse)httpRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                return result;
            }
        }

        private string GetRealTimeWeatherUrl()
        {
            string url = "https://api.openweathermap.org/data/2.5/weather";
            string token = "61c0675eae4c51328acf4d4c0be72cf6";

            return url + "?lat="+_Lat+"&lon="+_Lon+"&appid=" + token;
        }

        private WeatherModel GetDefaultWeatherData()
        {
            return new WeatherModel
            {
                main = new Main
                {
                    humidity = 70,
                    pressure = 1003,
                    temp =  304.91
                },
                name = "Jagraon",
                weather = new List<Weather>
                {
                  new Weather{main = "Clouds",description="overcast clouds",icon="04d"}
                },
                wind = new Wind { speed = 4.83}
            };
        }

        public UvModel GetRealTimeUvData()
        {
            try
            {
                if (_Lat == null && _Lon == null)
                {
                    return GetDefaultUvData();
                }
                string url = GetRealTimeUvUrl();
                string responseString = GetUvResponseString(url);

                UvModel uvData = JsonConvert.DeserializeObject<UvModel>(responseString);

                return uvData;
            }catch(Exception ex)
            {
                return GetDefaultUvData();
            }
            
        }
        private UvModel GetDefaultUvData()
        {
            return new UvModel
            {
                uv = 4.6,
                uv_time = "2022-08-05T01:43:05.975Z",

                sun_position = new SunPosition
                {
                    altitude = -0.012250989414255129,
                    azimuth = -1.912874131004239
                } 
            };
        }
      
        private string GetUvResponseString(string url)
        {
            var httpRequest = (HttpWebRequest)WebRequest.Create(url);

            httpRequest.Headers["x-access-token"] = "4be36784f0954deb6b344e044962a4db";


            var httpResponse = (HttpWebResponse)httpRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                return result;
            }
        }
        private string GetRealTimeUvUrl()
        {
            string url = "https://api.openuv.io/api/v1/uv";
            return url + "?lat=" + _Lat + "&lng=" + _Lon;
        }
    }
}