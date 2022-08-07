using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SkinCareWebApp.Models
{
    public class CustomWeatherModel
    {
        public string queryCost { get; set; }
        public double latitude { get; set; }

        public double longitude { get; set; }

        public Nullable<float> resolveAddress { get; set; }

        public string address { get; set; }

        public string timezone { get; set; }

        public Nullable<float> tzoffset { get; set; }


        public CustomWeatherData[] days { get; set; }  

        public CustomWeatherData currentConditions { get; set; }



    }

    public class CustomWeatherData
    {
        public string datetime { get; set; }
        public long datetimeEpoch { get; set; }

        public Nullable<float> temp { get; set; }

        public Nullable<float> feelslike { get; set; }

        public Nullable<float> humidity { get; set; }

        public Nullable<float> precip { get; set; }

        public Nullable<float> windspeed { get; set; }
        public Nullable<float> pressure { get; set; }
        public Nullable<float> solarradiation { get; set; }
        public Nullable<float> solarenergy { get; set; }
        public Nullable<float> uvindex { get; set; }
    }
}