using System.IO;
using System.Web.Mvc;
using NReco.ImageGenerator;

namespace ActiveCharts.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}