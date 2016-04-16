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

        public ActionResult CreateChart(string chartId)
        {
	        var model = new ChartViewModel();
	        if (!string.IsNullOrEmpty(chartId))
	        {
				var data = observeService.GetObservedData(chartId);
		        model.Id = chartId;
		        model.Data = data;
	        }
			return View(model);
        }

		public ActionResult GetChart(string dataSetName)
		{
		    var data = observeService.GetObservedData(dataSetName);
			var model = new ChartViewModel{ Data = data };
			return View("Chart", model);
		}

		public ActionResult UpdateChart(string id, string data)
		{
			observeService.UpdateChart(id, data);
			return new EmptyResult();
		}

	    public ActionResult SaveChart(string data)
	    {
		    observeService.SaveChart(data, CurrentUser);
		    return null;
	    }

	    public ActionResult GetChartsPreview()
	    {
		    var charts = observeService.GetUserCharts(CurrentUser);

		    return View("ChartsPreview", charts);
	    }
    }
}