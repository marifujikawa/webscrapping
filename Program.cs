using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;

namespace webscrapping
{
    class Program
    {

        const string ChromeDriverPath = "C:/desenvolvimento/webscrapping/";
        const string XPathName = "//*[@id='ember40']//div[2]//div[2]//div[1]//div[1]//h1";
        const string XPathTitle = "//*[@id='ember40']//div[2]//div[2]//div[1]//div[2]";
        const string XPathAbout = "//*[@id='ember182']//div";
        const string ClassNameExpandAbout = "inline-show-more-text__button";
        const string BaseUrl = "https://www.linkedin.com";
        const string LoginUrl = "/checkpoint/rm/sign-in-another-account?fromSignIn=true&trk=guest_homepage-basic_nav-header-signin";
        private static string ProfileUrl { get; set; }

        private static string UserName { get; set; }
        private static string Password { get; set; }

        static IWebDriver driver;
        static void Main(string[] args)
        {
            ProfileUrl = $"/in/{args[0]}";
            UserName = args[1];
            Password = args[2];

            try
            {
                startBrowser();
                login();
                getInformations();
                //closeBrowser();
            }
            catch (System.Exception)
            {
                //closeBrowser();
                throw;
            }

        }
        private static void startBrowser()
        {
            Console.WriteLine("startBrowser");
            driver = new ChromeDriver(ChromeDriverPath);
            driver.Url = BaseUrl + LoginUrl;


        }
        private static void closeBrowser()
        {
            driver.Close();
        }
        private static void login()
        {
            Console.WriteLine("login");
            IWebElement usernameElement = driver.FindElement(By.Id("username"));
            usernameElement.SendKeys(UserName);

            IWebElement passwordElement = driver.FindElement(By.Id("password"));
            passwordElement.SendKeys(Password);
            passwordElement.Submit();
        }

        private static void getInformations()
        {
            driver.Url = BaseUrl + ProfileUrl;
            getName();
            getTitle();
            getAbout();
        }
        private static void getName()
        {
            Console.WriteLine("getName");
            IWebElement nameElement = driver.FindElement(By.XPath(XPathName));
            scrollTo(nameElement);
            Console.WriteLine(nameElement.Text);
        }
        private static void getTitle()
        {
            Console.WriteLine("getTitle");
            IWebElement titleElement = driver.FindElement(By.XPath(XPathTitle));
            scrollTo(titleElement);
            Console.WriteLine(titleElement.Text);
        }
        private static void getAbout()
        {
            Console.WriteLine("getAbout");
            IWebElement expandElement = driver.FindElement(By.ClassName(ClassNameExpandAbout));
            scrollTo(expandElement);
            expandElement.Click();

            IWebElement aboutElement = driver.FindElement(By.XPath(XPathAbout));
            Console.WriteLine(aboutElement.Text);
        }

        private static void scrollTo(IWebElement expandElement)
        {
            Actions actions = new Actions(driver);
            actions.MoveToElement(expandElement);
            actions.Perform();
        }
    }
}
