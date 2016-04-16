using System;

namespace DataTracker
{
    class Program
    {
        static void Main(string[] args)
        {
            var dataTracker = new DataTracker();
            dataTracker.GetDataByXPath("http://www.cbr.ru/", "//*[@id=\"widget_exchange\"]/div/table/tbody/tr[2]/td[2]/text()");
            //"//*[@id="widget_exchange"]/div/table/tbody/tr[2]/td[2]/text()"
            Console.ReadLine();
        }
    }
}
