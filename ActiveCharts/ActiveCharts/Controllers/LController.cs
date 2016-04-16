using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ActiveCharts.Services;

namespace ActiveCharts.Controllers
{
    public class LController : Controller
    {
        private IRenderService renderService;

        public LController(IRenderService renderService)
        {
            this.renderService = renderService;
        }
        public ActionResult P(string id)
        {
            var data = renderService.GetPngData(id, Url.Action("GetChart", "Profile", new { dataSetName = id }, this.Request.Url.Scheme));
            return File(data, "image/png");
        }
    }
}