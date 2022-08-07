using Newtonsoft.Json;
using SkinCareWebApp.Models;
using SkinCareWebApp.Services;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace SkinCareWebApp.Controllers
{
    public class HomeController : Controller
    {
        private WeatherService WeatherService { get; set; }
        //private TrendsService TrendsService { get; set; }
        private ActionService ActionService { get; set; }

        public HomeController()
        {
            string lat = System.Web.HttpContext.Current.Request.Cookies["lat"]?.Value;
            string lon = System.Web.HttpContext.Current.Request.Cookies["lon"]?.Value;
            if(lat==null && lon == null)
            {
                this.WeatherService = new WeatherService("-37.815340", "144.963230");

                //this.TrendsService = new TrendsService(-37.815340, 144.963230, "MelBourne, AUS");

                return;
            }
            this.WeatherService = new WeatherService(lat, lon);

            //this.TrendsService = new TrendsService(double.Parse(lat), double.Parse(lon), "MelBourne, AUS");
            //this.ActionService = new ActionService();
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Assessment()
        {
            return View();
        }
        public ActionResult UvCard()
        {
            var realTimeUvData = WeatherService.GetRealTimeUvData();
            return View(realTimeUvData);
        }

        public ActionResult Precautions()
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
        public ActionResult Sunburn_treatment()
        {
            return View();
        }

        public ActionResult UVHarm()
        {
            return View();
        }

        public ActionResult Charts()
        {
            return View();
        }

        public JsonResult DataForChartTrends()
        {
            TrendsService trendsService = new TrendsService(-37.815340, 144.963230, "MelBourne, AUS");
            List<TrendsArchiveData> data = trendsService.dataForTrends();
            return Json(data,JsonRequestBehavior.AllowGet);
        }


        public ActionResult WeatherCard()
        {
            var realTimeWeatherData = WeatherService.GetRealTimeWeatherData();
            return View(realTimeWeatherData);
        }
    }
}