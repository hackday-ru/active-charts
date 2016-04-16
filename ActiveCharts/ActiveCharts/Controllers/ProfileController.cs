﻿using System;
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
		private IUserService userService;

        public ProfileController(IObserveService observeService, IUserService userService)
        {
            this.observeService = observeService;
			this.userService = userService;
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
	            model.IframeLink = GetIframeLink(chartId);
	        }
			return View(model);
        }

        private string GetIframeLink(string chartId)
        {
            var s = "<iframe src=\"{0}\" frameborder=\"0\"> </iframe>";
            var url = Url.Action("GetChart", "Profile", new {dataSetName = chartId}, this.Request.Url.Scheme);
            //http://localhost:3014/Profile/GetChart?dataSetName=7e7c2d7b-c605-47de-9ff5-ea3c2580b3e0
            var res = string.Format(s, url);
            return res;
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

	    public ActionResult GetChartsPreview(string token)
	    {
	        if (!string.IsNullOrEmpty(token))
	        {
		        var nickname = userService.GetNicknameByToken(token);
				var charts = observeService.GetUserCharts(nickname);
                return View("ChartsPreview", charts);
            }
	        else
	        {
                var charts = observeService.GetUserCharts(CurrentUser);
                return View("ChartsPreview", charts);
            }
	    }

    }
}