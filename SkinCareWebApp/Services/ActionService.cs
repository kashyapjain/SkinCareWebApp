using SkinCareWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SkinCareWebApp.Services
{
    public class ActionService
    {
        private SkinCareDBEntities Entities = new SkinCareDBEntities();
        private List<ActionData> Data = new List<ActionData>();
        private int UvIndex { get; set; }
        private string WeatherDescription { get; set; }
        public ActionService(double uvIndex,string mainWeather)
        {
            this.UvIndex = GetExactUvIndex(uvIndex);
            this.WeatherDescription = mainWeather.ToLower();

            this.Data = GetActionData();
        }

        private int GetExactUvIndex(double uvIndex)
        {
            if (uvIndex < 2) { return 2; }
            if (uvIndex > 2 && uvIndex < 5) { return 5; }
            if (uvIndex > 5 && uvIndex < 7) { return 7; }
            if (uvIndex > 7 && uvIndex < 10) { return 10; }
            if (uvIndex > 10) { return 11; }
            return 10;
        }

        private List<ActionData> GetActionData()
        {
            var weatherQuery = Entities.ActionDatas.Where(i => i.WeatherDescription.Contains(WeatherDescription));
            var uvQuery = Entities.ActionDatas.Where(i => i.UvRange == UvIndex);

            if (!weatherQuery.Any())
            {
                weatherQuery = Entities.ActionDatas.Where(i => i.WeatherDescription.Contains("rain") ||
                                                               i.WeatherDescription.Contains("clouds"));
            }
            
            var weatherData = weatherQuery.ToList();
            var uvData = uvQuery.ToList();
            
            Data.AddRange(weatherData);
            Data.AddRange(uvData);  
            
            return Data;
        }
        public List<ActionData> GetSpecificActionData(int type)
        {
            try
            {
                var query = Data.Where(i => i.Description.Type == type);

                if (query.Any())
                {
                    List<ActionData> data = query.ToList();

                    Random rand = new Random();

                    var shuffledData = data.OrderBy(_ => rand.Next()).ToList();

                    return shuffledData;
                }

                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}