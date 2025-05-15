using OpenQA.Selenium;
using System;

namespace TheInternetTest.Pages
{
    public class HomePage
    {
        private IWebDriver driver;
        private string url = "https://the-internet.herokuapp.com/";
        private By footerLinkSelector = By.CssSelector("#page-footer a");
        private By homePageLinksSelector = By.CssSelector("ul li a");
        private By githubRibbonSelector = By.CssSelector("a[href='https://github.com/tourdedave/the-internet'] img[alt='Fork me on GitHub']");

        public HomePage(IWebDriver driver)
        {
            this.driver = driver;
            driver.Navigate().GoToUrl(url);
            Console.WriteLine($"Navigated to {url}");
        }

        public void ExecuteAllTests()
        {

            Console.WriteLine("\n-- Verifying and Clicking all links --");
            VerifyAndClickAllLinks();
            
            Console.WriteLine("\n-- Verifying footer link --");
            VerifyFooterLink();

            Console.WriteLine("\n-- Verifying Github link --");
            VerifyGithubLink();
        }
         

        public void VerifyAndClickAllLinks()
        {
            var links = driver.FindElements(homePageLinksSelector);
            Console.WriteLine($"Total links found: {links.Count}");

            for (int i = 0; i < links.Count; i++)
            {
                // Re-locate link after navigation back to avoid stale element reference
                var link = driver.FindElements(homePageLinksSelector)[i];
                string linkText = link.Text;
                string href = link.GetAttribute("href");

                if (link.Displayed && link.Enabled)
                {
                    Console.WriteLine($"Clicking link: {linkText} ({href})");
                    link.Click();
                    driver.Navigate().Back();
                }
                else
                {
                    Console.WriteLine($"Link not clickable: {linkText}");
                }
            }

            Console.WriteLine("All home page links tested successfully.");
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
                var githubRibbon = driver.FindElement(githubRibbonSelector);
                var parentLink = githubRibbon.FindElement(By.XPath("..")); // get the anchor element
                string href = parentLink.GetAttribute("href");

                if (href == "https://github.com/tourdedave/the-internet")
                {
                    Console.WriteLine("GitHub ribbon link found and verified.");
                }
                else
                {
                    Console.WriteLine($"GitHub ribbon link href mismatch: {href}");
                }
            }
            catch (NoSuchElementException)
            {
                Console.WriteLine("GitHub ribbon link not found.");
            }
        }
    }
}
