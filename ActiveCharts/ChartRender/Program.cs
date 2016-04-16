using System;
using NReco.ImageGenerator;
using NReco.PhantomJS;

namespace ChartRender
{
    static class Program
    {
        public static void Main(string[] args)
        {
            var htmlToImageConv = new NReco.ImageGenerator.HtmlToImageConverter();
            htmlToImageConv.GenerateImageFromFile("http://google.com", null, "result.png");
        }
    }
}
