using SkinCareWebApp.Services;
using System.Web.Mvc;

namespace SkinCareWebApp.Controllers
{
    public class HomeController : Controller
    {
        private WeatherService _WeatherService { get; set; }

        public HomeController()
        {
            string lat = System.Web.HttpContext.Current.Request.Cookies["lat"]?.Value;
            string lon = System.Web.HttpContext.Current.Request.Cookies["lon"]?.Value;

            this._WeatherService = new WeatherService(lat, lon);
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
            var realTimeUvData = _WeatherService.GetRealTimeUvData();
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
    }
}