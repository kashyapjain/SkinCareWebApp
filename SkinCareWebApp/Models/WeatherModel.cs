using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SkinCareWebApp.Models
{
    public class WeatherModel
    {
        public List<Weather> weather { get; set; }
        public Main main { get; set; }
        public Wind wind { get; set; }
        public string name { get; set; }
    }
    public class Main
    {
        public double temp { get; set; }
        public int pressure { get; set; }
        public int humidity { get; set; }
    }

    public class Weather
    {
        public string main { get; set; }
        public string description { get; set; }
        public string icon { get; set; }
    }

    public class Wind
    {
        public double speed { get; set; }
    }


}