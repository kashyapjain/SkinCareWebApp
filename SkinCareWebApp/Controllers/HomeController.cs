using SkinCareWebApp.Models;
using SkinCareWebApp.Services;
using System;
using System.Web;
using System.Web.Mvc;

namespace SkinCareWebApp.Controllers
{
    public class HomeController : Controller
    {
        private WeatherService WeatherService { get; set; }
        private ActionService ActionService { get; set; }

        private AssessmentService AssessmentService { get; set; }

        private double UvIndex { get; set; }
        private string MainWeather { get; set; }

        public HomeController()
        {
            string lat = System.Web.HttpContext.Current.Request.Cookies["lat"]?.Value;
            string lon = System.Web.HttpContext.Current.Request.Cookies["lon"]?.Value;
            string uvIndex = System.Web.HttpContext.Current.Request.Cookies[nameof(UvIndex)]?.Value;
            string mainWeather = System.Web.HttpContext.Current.Request.Cookies[nameof(MainWeather)]?.Value;
            
            UvIndex = uvIndex!=null ? Convert.ToDouble(uvIndex):0;
            MainWeather = mainWeather;

            this.WeatherService = new WeatherService(lat, lon);
            this.AssessmentService = new AssessmentService();
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
            try
            {
                var realTimeUvData = WeatherService.GetRealTimeUvData();

                HttpCookie uvIndexCookie = new HttpCookie(nameof(UvIndex))
                {
                    Value = realTimeUvData.uv.ToString()
                };

                System.Web.HttpContext.Current.Response.Cookies.Add(uvIndexCookie);

                return View(realTimeUvData);
            }
            catch (Exception ex)
            {
                return View();
            }
        }

        public ActionResult Precautions()
        {
            return View();
        }

        public ActionResult Activities()
        {
            return View();
        }

        public ActionResult ActionDetails(int type)
        {
            ActionService = new ActionService(UvIndex, MainWeather);
            var activities = ActionService.GetSpecificActionData(type);
            return View(activities);
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

            HttpCookie mainWeatherCookie = new HttpCookie(nameof(MainWeather))
            {
                Value = realTimeWeatherData.weather[0].main
            };

            System.Web.HttpContext.Current.Response.Cookies.Add(mainWeatherCookie);

            return View(realTimeWeatherData);
        }
    }
}