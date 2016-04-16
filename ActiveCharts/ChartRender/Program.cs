using System;
using System.IO;
using NReco.ImageGenerator;
using NReco.PhantomJS;

namespace ChartRender
{
    static class Program
    {
        public static void Main(string[] args)
        {
            var phantomJS = new PhantomJS();
            phantomJS.Run(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "rasterize.js"),
                    new[] { "http://localhost:3014/Profile/GetChart?dataSetName=7e7c2d7b-c605-47de-9ff5-ea3c2580b3e0", "result1.png" });
            //var htmlToImageConv = new NReco.ImageGenerator.HtmlToImageConverter();
            //htmlToImageConv.GenerateImageFromFile("http://localhost:3014/Profile/GetChart?dataSetName=7e7c2d7b-c605-47de-9ff5-ea3c2580b3e0", null, "result.png");
        }
    }
}
