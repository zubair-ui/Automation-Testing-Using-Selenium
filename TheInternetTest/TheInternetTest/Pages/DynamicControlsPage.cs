using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;

namespace TheInternetTest.Pages
{
    public class DynamicControlsPage
    {
        private readonly IWebDriver driver;
        private readonly WebDriverWait wait;

        private readonly string url = "https://the-internet.herokuapp.com/dynamic_controls";

        // Selectors
        private readonly By checkboxButton = By.CssSelector("#checkbox-example button");
        private readonly By checkbox = By.Id("checkbox"); // the dynamically added/removed checkbox input
        private readonly By checkboxMessage = By.CssSelector("#checkbox-example #message");

        private readonly By inputButton = By.CssSelector("#input-example button");
        private readonly By inputField = By.CssSelector("#input-example input[type='text']");
        private readonly By inputMessage = By.CssSelector("#input-example #message");

        // The loader divs with id='loading' appear multiple times and may stay visible, so we will just check presence, not disappearance
        private readonly By loaders = By.CssSelector("div#loading");

        // New selectors for footer and GitHub ribbon
        private readonly By footerLinkSelector = By.CssSelector("#page-footer a[href='http://elementalselenium.com/']");
        private readonly By githubRibbonSelector = By.CssSelector("a[href='https://github.com/tourdedave/the-internet'] img");

        public DynamicControlsPage(IWebDriver driver)
        {
            this.driver = driver;
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            driver.Navigate().GoToUrl(url);
            Console.WriteLine($"Navigated to {url}");
        }

        public (bool, string, string) ClickCheckboxButton()
        {
            var button = wait.Until(drv => drv.FindElement(checkboxButton));
            string beforeText = button.Text;

            button.Click();
            Console.WriteLine($"Clicked Checkbox button (was '{beforeText}')");

            // Wait fixed 3.5 seconds to allow the JS toggle & messages to appear (because the page uses setTimeout 3 sec)
            System.Threading.Thread.Sleep(3500);

            bool checkboxPresent = IsElementPresent(checkbox);
            string afterButtonText = button.Text;

            string messageText = GetElementTextSafe(checkboxMessage);

            Console.WriteLine($"Checkbox present: {checkboxPresent}");
            Console.WriteLine($"Button text now: {afterButtonText}");
            Console.WriteLine($"Message text: {messageText}");

            return (checkboxPresent, afterButtonText, messageText);
        }

        public (bool, string, string) ClickInputButton()
        {
            var button = wait.Until(drv => drv.FindElement(inputButton));
            string beforeText = button.Text;

            button.Click();
            Console.WriteLine($"Clicked Input button (was '{beforeText}')");

            // Wait fixed 3.5 seconds to allow the JS toggle & messages to appear
            System.Threading.Thread.Sleep(3500);

            bool inputEnabled = false;
            try
            {
                var input = driver.FindElement(inputField);
                inputEnabled = input.Enabled;
            }
            catch (NoSuchElementException)
            {
                inputEnabled = false;
            }

            string afterButtonText = button.Text;
            string messageText = GetElementTextSafe(inputMessage);

            Console.WriteLine($"Input enabled: {inputEnabled}");
            Console.WriteLine($"Button text now: {afterButtonText}");
            Console.WriteLine($"Message text: {messageText}");

            return (inputEnabled, afterButtonText, messageText);
        }

        public bool IsCheckboxPresent() => IsElementPresent(checkbox);

        public bool IsInputEnabled()
        {
            try
            {
                return driver.FindElement(inputField).Enabled;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        private string GetElementTextSafe(By by)
        {
            try
            {
                return driver.FindElement(by).Text;
            }
            catch (NoSuchElementException)
            {
                return string.Empty;
            }
        }

        private bool IsElementPresent(By by)
        {
            try
            {
                var elems = driver.FindElements(by);
                return elems.Count > 0;
            }
            catch (NoSuchElementException)
            {
                return false;
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

            var (checkboxPresent, checkboxButtonText, checkboxMessage) = ClickCheckboxButton();
            Console.WriteLine($"After checkbox toggle: Present={checkboxPresent}, Button='{checkboxButtonText}', Message='{checkboxMessage}'");

            var (inputEnabled, inputButtonText, inputMessage) = ClickInputButton();
            Console.WriteLine($"After input toggle: Enabled={inputEnabled}, Button='{inputButtonText}', Message='{inputMessage}'");

            Console.WriteLine("\n-- Verifying footer link --");
            VerifyFooterLink();

            Console.WriteLine("\n-- Verifying Github link --");
            VerifyGithubLink();

        }

    }
}
