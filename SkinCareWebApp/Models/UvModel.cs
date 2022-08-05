using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SkinCareWebApp.Models
{
    public class UvModel
    {
        public double uv { get; set; }
        public string uv_time { get; set; }
        public SunPosition sun_position { get; set; }
    }

    public class SunPosition
    {
        public double azimuth { get; set; }
        public double altitude { get; set; }
    }
}