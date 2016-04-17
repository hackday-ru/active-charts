using System.Text.RegularExpressions;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace SeleniumTracker
{
    public class SeleniumTracker
    {
        private static ChromeDriver chromeDriver;

        static SeleniumTracker()
        {
            chromeDriver = new ChromeDriver();    
        }

        public string GetDataByXPath(string url, string xPath)
        {
            chromeDriver.Navigate().GoToUrl(url);
            var elementText = chromeDriver.FindElement(By.XPath(xPath)).Text.Replace(',', '.');
            var text = Regex.Match(elementText, @"[-+]?\d*[.]?\d+").Value;

            if (elementText.Contains("-") || elementText.Contains("−"))
                text = "-" + text;
            return text;
        }
    }
}
