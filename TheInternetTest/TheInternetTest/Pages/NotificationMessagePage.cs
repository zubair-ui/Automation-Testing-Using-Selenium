using OpenQA.Selenium;
using System;

namespace TheInternetTest.Pages
{
    public class NotificationMessagePage
    {
        private IWebDriver driver;
        private string url = "https://the-internet.herokuapp.com/notification_message";

        private By notificationMessageSelector = By.Id("flash");
        private By clickHereLinkSelector = By.CssSelector("a[href='/notification_message']");
        private By footerLinkSelector = By.CssSelector("#page-footer a");

        private By githubRibbonSelector = By.CssSelector("a[href='https://github.com/tourdedave/the-internet'] img[alt='Fork me on GitHub']");

        public NotificationMessagePage(IWebDriver driver)
        {
            this.driver = driver;
            driver.Navigate().GoToUrl(url);
            Console.WriteLine($"Navigated to {url}");
        }

        public void PrintNotificationMessage()
        {
            try
            {
                var messageElement = driver.FindElement(notificationMessageSelector);
                string messageText = messageElement.Text.Trim();
                Console.WriteLine($"Notification message: {messageText}");
            }
            catch (NoSuchElementException)
            {
                Console.WriteLine("Notification message element not found.");
            }
        }

        public void ClickHereToReloadMessage()
        {
            try
            {
                var clickHereLink = driver.FindElement(clickHereLinkSelector);
                clickHereLink.Click();
                Console.WriteLine("Clicked 'Click here' link to reload notification message.");
            }
            catch (NoSuchElementException)
            {
                Console.WriteLine("'Click here' link not found.");
            }
        }

        public void VerifyFooterLink()
        {
            try
            {
                var footerLink = driver.FindElement(footerLinkSelector);
                string linkText = footerLink.Text;
                string linkHref = footerLink.GetAttribute("href");

                if (linkText.Contains("Elemental Selenium"))
                {
                    Console.WriteLine($"Found footer link: {linkText}");
                    Console.WriteLine($"Link points to: {linkHref}");
                }
                else
                {
                    Console.WriteLine("Footer link text mismatch.");
                }
            }
            catch (NoSuchElementException)
            {
                Console.WriteLine("'Powered by Elemental Selenium' link not found.");
            }
        }

        public void VerifyGithubLink()
        {
            try
            {
                var githubRibbon = driver.FindElement(githubRibbonSelector);
                var parentLink = githubRibbon.FindElement(By.XPath(".."));
                string href = parentLink.GetAttribute("href");

                if (href == "https://github.com/tourdedave/the-internet")
                {
                    Console.WriteLine("Found GitHub ribbon link.");
                    Console.WriteLine($"Link points to: {href}");
                }
                else
                {
                    Console.WriteLine("GitHub ribbon link href mismatch.");
                }
            }
            catch (NoSuchElementException)
            {
                Console.WriteLine("GitHub ribbon link not found.");
            }
        }

        public void ExecuteAllTests()
        {

            var i = 0;
            for(i=0; i<6; i++)
            {

                ClickHereToReloadMessage();
                PrintNotificationMessage();

                System.Threading.Thread.Sleep(100);
            }

            Console.WriteLine("\n-- Verifying footer link --");
            VerifyFooterLink();

            Console.WriteLine("\n-- Verifying Github link --");
            VerifyGithubLink();
        }
    }
}
