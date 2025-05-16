using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using System;

namespace TheInternetTest.Pages
{
    public class DragAndDropPage
    {
        private IWebDriver driver;
        private string url = "https://the-internet.herokuapp.com/drag_and_drop";
        private By columnASelector = By.Id("column-a");
        private By columnBSelector = By.Id("column-b");
        private By footerLinkSelector = By.CssSelector("#page-footer a");
        private By githubRibbonSelector = By.CssSelector("a[href='https://github.com/tourdedave/the-internet'] img[alt='Fork me on GitHub']");

        public DragAndDropPage(IWebDriver driver)
        {
            this.driver = driver;
            driver.Navigate().GoToUrl(url);
            Console.WriteLine($"Navigated to {url}");
        }

        public void DragAtoB()
        {
            try
            {
                var columnA = driver.FindElement(columnASelector);
                var columnB = driver.FindElement(columnBSelector);

                Actions action = new Actions(driver);
                action.DragAndDrop(columnA, columnB).Perform();

                Console.WriteLine("Performed drag and drop: A → B.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Drag and drop failed: {ex.Message}");
            }
        }

        public void VerifyDragResult()
        {
            var columnAHeader = driver.FindElement(columnASelector).Text;
            var columnBHeader = driver.FindElement(columnBSelector).Text;

            Console.WriteLine($"Column A now has: {columnAHeader}");
            Console.WriteLine($"Column B now has: {columnBHeader}");
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
            Console.WriteLine("\n-- Performing drag and drop --");
            DragAtoB();

            Console.WriteLine("\n-- Verifying drag and drop result --");
            VerifyDragResult();

            Console.WriteLine("\n-- Verifying footer link --");
            VerifyFooterLink();

            Console.WriteLine("\n-- Verifying GitHub link --");
            VerifyGithubLink();
        }
    }
}
