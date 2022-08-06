using SkinCareWebApp.Models;
using SkinCareWebApp.Services;
using System;
using System.Web.Mvc;

namespace SkinCareWebApp.Controllers
{
    public class HomeController : Controller
    {
        private WeatherService WeatherService { get; set; }
        private ActionService ActionService { get; set; }

        private AssessmentService AssessmentService { get; set; }

        public HomeController()
        {
            string lat = System.Web.HttpContext.Current.Request.Cookies["lat"]?.Value;
            string lon = System.Web.HttpContext.Current.Request.Cookies["lon"]?.Value;

            this.WeatherService = new WeatherService(lat, lon);
            this.AssessmentService = new AssessmentService();
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

        [HttpPost]
        public double AssessmentResult(string q1, string q2, string q3, string q4, string q5, int correct)
        {
            AssessmentRespons response = new AssessmentRespons()
            {
                q1 = q1,
                q2 = q2,
                q3 = q3,
                q4 = q4,
                q5 = q5,
                correct = correct
            };
            double percentage = AssessmentService.getPrecentage(Convert.ToInt32(correct));
            AssessmentService.saveResponse(response);
            return percentage;
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

        public ActionResult WeatherCard()
        {
            var realTimeWeatherData = WeatherService.GetRealTimeWeatherData();
            return View(realTimeWeatherData);
        }
    }
}