using OpenQA.Selenium;
using System;

namespace TheInternetTest.Pages
{
    public class AbTestPage
    {
        private IWebDriver driver;
        private string pageUrl = "https://the-internet.herokuapp.com/abtest";

        private By headingSelector = By.CssSelector("div.example h3");
        private By footerLink = By.CssSelector("#page-footer a");
        private By githubRibbonSelector = By.CssSelector("a[href='https://github.com/tourdedave/the-internet'] img[alt='Fork me on GitHub']");

        public AbTestPage(IWebDriver webDriver)
        {
            driver = webDriver;
        }

        public void ExecuteAllTests()
        {
            NavigateToPage();
            Console.WriteLine($"Navigated to {pageUrl}");

            Console.WriteLine("\n-- Verifying Page title --");
            VerifyPageTitle();

            Console.WriteLine("\n-- Verifying existence of Heading --");
            VerifyHeadingExists();

            Console.WriteLine("\n-- Verifying footer link --");
            VerifyFooterLink();

            Console.WriteLine("\n-- Verifying Github link --");
            VerifyGithubLink();
        }

        public void NavigateToPage()
        {
            driver.Navigate().GoToUrl(pageUrl);
            Console.WriteLine($"Navigated to {pageUrl}");
        }

        public void VerifyPageTitle()
        {
            string expectedTitle = "The Internet";
            string actualTitle = driver.Title;

            if (actualTitle == expectedTitle)
                Console.WriteLine($"Page title verified: {actualTitle}");
            else
                Console.WriteLine($"Page title mismatch! Actual: {actualTitle}");
        }

        public void VerifyHeadingExists()
        {
            try
            {
                var heading = driver.FindElement(headingSelector);
                Console.WriteLine($"Found heading: {heading.Text}");
            }
            catch (NoSuchElementException)
            {
                Console.WriteLine("Heading not found on A/B Test page.");
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

        public void VerifyFooterLink()
        {
            try
            {
                var footer = driver.FindElement(footerLink);
                string linkText = footer.Text;
                string linkHref = footer.GetAttribute("href");

                if (linkText.Contains("Elemental Selenium"))
                {
                    Console.WriteLine($"Found footer link: {linkText}");
                    Console.WriteLine($"Link points to: {linkHref}");

                    footer.Click();
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
    }
}
