using SkinCareWebApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SkinCareWebApp.Controllers
{
    public class HomeController : Controller
    {
        private WeatherService _WeatherService { get; set; }

        public HomeController()
        {
            string lat = Request?.Cookies["lat"]?.Value;
            string lon = Request?.Cookies["lon"]?.Value;

            this._WeatherService = new WeatherService(lat, lon);
        }
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult UvCard()
        {
            var realTimeUvData = _WeatherService.GetRealTimeUvData();
            return View(realTimeUvData);
        }

        public ActionResult Precautions2()
        {
            return View();
        }

        public ActionResult Activities()
        {
            return View();
        }

        public ActionResult Sunburn()
        {
            return View();
        }

        public ActionResult UVHarm()
        {
            return View();
        }
    }
}