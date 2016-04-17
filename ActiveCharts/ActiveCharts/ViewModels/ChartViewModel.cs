using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ActiveCharts.ViewModels
{
	public class ChartViewModel
	{
		public string Id { get; set; }
		public string Data { get; set; }
        public string PngLink { get; set; }
        public string IframeLink { get; set; }
        public string SvgLink { get; set; }
        public string Url { get; set; }
	}
}