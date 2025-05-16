using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using System;

namespace TheInternetTest.Pages
{
    public class ContextMenuPage
    {
        private IWebDriver driver;
        private string url = "https://the-internet.herokuapp.com/context_menu";
        private By hotSpotSelector = By.Id("hot-spot");
        private By footerLinkSelector = By.CssSelector("#page-footer a");
        private By githubRibbonSelector = By.CssSelector("a[href='https://github.com/tourdedave/the-internet'] img[alt='Fork me on GitHub']");

        public ContextMenuPage(IWebDriver driver)
        {
            this.driver = driver;
            driver.Navigate().GoToUrl(url);
            Console.WriteLine($"Navigated to {url}");
        }
         

        public void RightClickOnHotSpot()
        {
            var hotSpot = driver.FindElement(hotSpotSelector);
            Actions action = new Actions(driver);
            action.ContextClick(hotSpot).Perform();
            Console.WriteLine("Performed right-click (context click) on hot spot.");
        }

        public string GetAlertTextAndAccept()
        {
            try
            {
                IAlert alert = driver.SwitchTo().Alert();
                string alertText = alert.Text;
                Console.WriteLine($"Alert text: {alertText}");
                alert.Accept();
                Console.WriteLine("Alert accepted.");
                return alertText;
            }
            catch (NoAlertPresentException)
            {
                Console.WriteLine("No alert present.");
                return null;
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
                    footerLink.Click();
                    Console.WriteLine("Clicked footer link successfully.");
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
                var githubRibbon = driver.FindElement(githubRibbonSelector); // img element
                var parentLink = githubRibbon.FindElement(By.XPath("..")); // <a> element
                string href = parentLink.GetAttribute("href");

                if (href == "https://github.com/tourdedave/the-internet")
                {
                    Console.WriteLine("Found GitHub ribbon link.");
                    Console.WriteLine($"Link points to: {href}");

                    // Click the visible image instead of the <a>
                    githubRibbon.Click();

                    driver.Navigate().Back();
                    Console.WriteLine("Clicked GitHub ribbon image successfully.");
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
            Console.WriteLine("\n-- Right Clicking on Hotspot --");
            RightClickOnHotSpot();
            var alertText = GetAlertTextAndAccept();
            if (alertText == "You selected a context menu")
            {
                Console.WriteLine("Alert text is correct.");
            }
            else
            {
                Console.WriteLine("Alert text is incorrect or alert missing.");
            }

            Console.WriteLine("\n-- Verifying footer link --");
            VerifyFooterLink();

            Console.WriteLine("\n-- Verifying Github link --");
            VerifyGithubLink();
        }
    }
}
