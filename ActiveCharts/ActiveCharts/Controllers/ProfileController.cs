using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ActiveCharts.Services.Interfaces;
using ActiveCharts.ViewModels;

namespace ActiveCharts.Controllers
{
    public class ProfileController : BaseController
    {
        private IObserveService observeService;

        public ProfileController(IObserveService observeService)
        {
            this.observeService = observeService;
        }
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
		    var data = observeService.GetObservedData(dataSetName);
			var model = new ChartViewModel{ Data = data };
			return View("Chart", model);
		}
    }
}