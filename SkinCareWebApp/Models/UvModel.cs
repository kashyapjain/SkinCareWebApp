using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SkinCareWebApp.Models
{
    public class ApiResult
    {
        public UvModel result { get; set; }
    }
    public class UvModel
    {
        public double uv { get; set; }
        public string uv_time { get; set; }
        public long queryCost { get; set; }
        public double latitude { get; set; }
        public double longitude { get; set; }
        public string address { get; set; }
        public SunPosition sun_position { get; set; }
        private CurrentConditions _currentConditions { get; set; }
        public CurrentConditions currentConditions 
        { 
            get
            {
                return _currentConditions;
            }
            set 
            { 
                if(uv == 0){uv = value.uvindex;}
                if (uv_time == null){uv_time = value.datetime;}

                _currentConditions = value;
            } 
        }
    }
    public class CurrentConditions
    {
        public string datetime { get; set; }
        public double temp { get; set; }
        public double humidity { get; set; }
        public double precip { get; set; }
        public double windspeed { get; set; }
        public double pressure { get; set; }
        public long uvindex { get; set; }
    }

    public class SunPosition
    {
        public double azimuth { get; set; }
        public double altitude { get; set; }
    }
}