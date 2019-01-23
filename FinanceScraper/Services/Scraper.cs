using FinanceScraper.Models;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinanceScraper.Services
{
    public class Scraper
    {
        public List<Stock> Scrape()
        {
            var options = new ChromeOptions();
            options.AddArguments("--headless");
            options.AddArguments("--disable-gpu");
            options.AddArguments("--window-size=1980,1080");

            IWebDriver chromeDriver = new ChromeDriver("C:\\Users\\klync\\Source\\Repos\\FinanceScraper", options);
            // fixing timing issues
            //WebDriverWait wait = new WebDriverWait(chromeDriver, TimeSpan.FromSeconds(5));

            chromeDriver.Navigate().GoToUrl("https://login.yahoo.com");
            chromeDriver.Manage().Window.Maximize();

            chromeDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);

            chromeDriver.FindElement(By.Id("login-username")).SendKeys("" + Keys.Enter);
            chromeDriver.FindElement(By.Id("login-passwd")).SendKeys("" + Keys.Enter);

            chromeDriver.Url = "https://finance.yahoo.com/portfolio/p_0/view/v1";

            //var popup = chromeDriver.FindElement(By.XPath("//dialog[@id = '__dialog']/section/button"));
            //popup.Click();

            IWebElement list = chromeDriver.FindElement(By.TagName("tbody"));
            System.Collections.ObjectModel.ReadOnlyCollection<IWebElement> stocks = list.FindElements(By.TagName("tr"));
            int count;
            count = stocks.Count();

            List<Stock> stockList = new List<Stock>();
            for (int i = 1; i <= count; i++)
            {
                var symbol = chromeDriver.FindElement(By.XPath("//*[@id=\"main\"]/section/section[2]/div[2]/table/tbody/tr[" + i + "]/td[1]/span/a")).GetAttribute("innerText");
                var lastPrice = chromeDriver.FindElement(By.XPath("//*[@id=\"main\"]/section/section[2]/div[2]/table/tbody/tr[" + i + "]/td[2]/span")).GetAttribute("innerText");
                var change = chromeDriver.FindElement(By.XPath("//*[@id=\"main\"]/section/section[2]/div[2]/table/tbody/tr[" + i + "]/td[3]/span")).GetAttribute("innerText");
                var pChange = chromeDriver.FindElement(By.XPath("//*[@id=\"main\"]/section/section[2]/div[2]/table/tbody/tr[" + i + "]/td[4]/span")).GetAttribute("innerText");
                var currency = chromeDriver.FindElement(By.XPath("//*[@id=\"main\"]/section/section[2]/div[2]/table/tbody/tr[" + i + "]/td[5]")).GetAttribute("innerText");
                var volume = chromeDriver.FindElement(By.XPath("//*[@id=\"main\"]/section/section[2]/div[2]/table/tbody/tr[" + i + "]/td[7]/span")).GetAttribute("innerText");
                var aVolume = chromeDriver.FindElement(By.XPath("//*[@id=\"main\"]/section/section[2]/div[2]/table/tbody/tr[" + i + "]/td[9]")).GetAttribute("innerText");
                var marketCap = chromeDriver.FindElement(By.XPath("//*[@id=\"main\"]/section/section[2]/div[2]/table/tbody/tr[" + i + "]/td[13]/span")).GetAttribute("innerText");


                Stock stock = new Stock();
                Console.WriteLine(symbol);
                stock.Symbol = symbol;
                Console.WriteLine(lastPrice);
                stock.LastPrice = lastPrice;
                Console.WriteLine(change);
                stock.Change = change;
                Console.WriteLine(pChange);
                stock.PChange = pChange;
                Console.WriteLine(currency);
                stock.Currency = currency;
                Console.WriteLine(volume);
                stock.Volume = volume;
                Console.WriteLine(aVolume);
                stock.AVolume = aVolume;
                Console.WriteLine(marketCap);
                stock.MarketCap = marketCap;

                Console.WriteLine(stock);
                stockList.Add(stock);

            }
            return stockList;
        }
    }
}