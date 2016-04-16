using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ActiveCharts.ViewModels;

namespace ActiveCharts.Controllers
{
    public class ProfileController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CreateChart(int chartId, string chartData, int? chartWidth, int? chartHeight)
        {
            ViewBag.ChartData = chartData;
            ViewBag.ChartWidth = chartWidth;
            ViewBag.ChartHeight = chartHeight;
            return View(chartId);
        }

		public ActionResult GetChart(string dataSetName)
		{
			var model = new ChartViewModel{ Data = "dfdf fdsf fdsd\n33 33 43\n34 546 2" };
			return View("Chart", model);
		}
    }
}