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
            var path = Server.MapPath("~/Content/Data") + "\\" + Guid.NewGuid() + ".png";

            var data = renderService.GetPngData(id, Url.Action("GetChart", "Profile", new { dataSetName = id }, this.Request.Url.Scheme), path);
            return File(data, "image/png");
        }

        public ActionResult S(string id)
        {
            var path = Server.MapPath("~/Content/Data") + "\\" + Guid.NewGuid() + ".svg";

            var data = renderService.GetSvgData(id, Url.Action("GetChart", "Profile", new { dataSetName = id }, this.Request.Url.Scheme), path);
            return File(data, "image/svg");
        }
    }
}