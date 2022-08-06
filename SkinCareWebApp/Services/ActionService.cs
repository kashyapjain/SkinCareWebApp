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
        private ActionData[] Data { get; set; }
        private int UvIndex { get; set; }
        private string WeatherDescription { get; set; }
        public ActionService(int uvIndex,string weatherDescription)
        {
            this.UvIndex = uvIndex;
            this.WeatherDescription = weatherDescription.ToLower();

            this.Data = GetActionData();
        }

        private ActionData[] GetActionData()
        {
            var weatherQuery = Entities.ActionDatas.Where(i => i.WeatherDescription.Contains(WeatherDescription));
            var uvQuery = Entities.ActionDatas.Where(i => i.UvRange.Equals(UvIndex));

            if (!weatherQuery.Any())
            {
                weatherQuery = Entities.ActionDatas.Where(i => i.WeatherDescription.Contains("rain") ||
                                                               i.WeatherDescription.Contains("clouds"));
            }

            var data = weatherQuery.ToArray().Concat(uvQuery.ToArray());
            return Data.ToArray();
        }

        public ActionData[] GetPrecautions()
        {
            return GetSpecificActionData(1);
        }

        public ActionData[] GetActivities()
        {
            return GetSpecificActionData(2);
        }

        private ActionData[] GetSpecificActionData(int type)
        {
            var query = Data.Where(i => i.Description.Type == type);

            if (query.Any())
            {
                return query.ToArray();
            }

            return null;
        }
    }
}