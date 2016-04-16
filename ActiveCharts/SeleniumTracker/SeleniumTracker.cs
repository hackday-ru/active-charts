using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace SeleniumTracker
{
    public class SeleniumTracker
    {
        public string GetDataByXPath(string url, string xPath)
        {
            var driver = new ChromeDriver();
            driver.Navigate().GoToUrl(url);
            var text = driver.FindElement(By.XPath(xPath)).Text;
            var element = driver.FindElementByXPath(xPath);
            return element.Text;
        }
    }
}
