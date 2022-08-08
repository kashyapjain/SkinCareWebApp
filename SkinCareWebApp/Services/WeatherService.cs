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
        public WeatherModel GetRealTimeWeatherData()
        {
            try
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
            catch (Exception ex)
            {
                return GetDefaultWeatherData();
            }
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
                
                if(uvData.uv == 0)
                {
                    ApiResult apiResult = JsonConvert.DeserializeObject<ApiResult>(responseString);
                    return apiResult.result;
                }

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
            try
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
            catch(Exception ex)
            {
                url = GetVisualCrossingUrl();

                var httpRequest = (HttpWebRequest)WebRequest.Create(url);

                var httpResponse = (HttpWebResponse)httpRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    return result;
                }
            }
        }

        private string GetVisualCrossingUrl()
        {
            string url = "https://weather.visualcrossing.com/VisualCrossingWebServices/rest/services/timeline/";
            string elements = "datetime%2CdatetimeEpoch%2Cname%2Caddress%2CresolvedAddress%2Ctemp%2Cfeelslike%2Chumidity%2Cprecip%2Cwindspeed%2Cpressure%2Csolarradiation%2Csolarenergy%2Cuvindex&include=current%2Cdays";
            string key = "EBY8VHU22SUAUN9GCRBUU42EF";
            return url + _Lat + "%2C" + _Lon + "/today?unitGroup=metric&elements=" + elements + "&key=" + key + "&contentType=json";
        }

        private string GetRealTimeUvUrl()
        {
            string url = "https://api.openuv.io/api/v1/uv";
            return url + "?lat=" + _Lat + "&lng=" + _Lon;
        }
    }
}