using OpenQA.Selenium;
using System;

namespace TheInternetTest.Pages
{
    public class ChallengingDomPage
    {
        private IWebDriver driver;
        private string url = "https://the-internet.herokuapp.com/challenging_dom";
        private By githubRibbonSelector = By.CssSelector("a[href='https://github.com/tourdedave/the-internet'] img[alt='Fork me on GitHub']");

        // Selector for the 3 buttons on the top right, located by position not text/class
        private By domButtons = By.CssSelector("div.large-2.columns a.button");

        // Selector for table rows
        private By tableRows = By.CssSelector("table tbody tr");

        // Footer link selector
        private By footerLinkSelector = By.CssSelector("#page-footer a");

        public ChallengingDomPage(IWebDriver driver)
        {
            this.driver = driver;
            driver.Navigate().GoToUrl(url);
            Console.WriteLine($"Navigated to {url}");
        }


        public void ClickButtonByIndex(int index)
        {
            var buttons = driver.FindElements(domButtons);
            if (buttons.Count > index)
            {
                buttons[index].Click();
                Console.WriteLine($"Clicked button at index {index}.");
            }
            else
            {
                Console.WriteLine($"Button index {index} not found.");
            }
        }

        public void PrintTableRows()
        {
            var rows = driver.FindElements(tableRows);
            Console.WriteLine($"Total table rows found: {rows.Count}");
            for (int i = 0; i < rows.Count; i++)
            {
                Console.WriteLine($"Row {i + 1}: {rows[i].Text}");
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

            // Click the three buttons by index (order is stable)
            Console.WriteLine("\n-- Clicking Buttons by Index --");
            ClickButtonByIndex(0);
            ClickButtonByIndex(1);
            ClickButtonByIndex(2);

            // Print table rows text
            Console.WriteLine("\n-- Printing table rows --");
            PrintTableRows();

            Console.WriteLine("\n-- Verifying footer link --");
            VerifyFooterLink();

            Console.WriteLine("\n-- Verifying Github link --");
            VerifyGithubLink();
        }
    }
}

