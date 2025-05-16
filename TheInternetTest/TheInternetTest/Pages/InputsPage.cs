using OpenQA.Selenium;
using OpenQA.Selenium.Interactions; 

namespace TheInternetTest.Pages
{
    public class InputsPage
    {
        private IWebDriver driver;
        private string url = "https://the-internet.herokuapp.com/inputs";
        private By numberInputSelector = By.CssSelector("input[type='number']");
        private By footerLinkSelector = By.CssSelector("#page-footer a");
        private By githubRibbonSelector = By.CssSelector("a[href='https://github.com/tourdedave/the-internet'] img[alt='Fork me on GitHub']");

        public InputsPage(IWebDriver driver)
        {
            this.driver = driver;
            driver.Navigate().GoToUrl(url);
            Console.WriteLine($"Navigated to {url}");
        }

        public void ExecuteAllTests()
        {
            Console.WriteLine("-- Testing Valid Number Input --");
            TestValidNumberInput();

            Console.WriteLine("\n-- Testing Up Arrow Key Functionality --");
            TestUpArrowKeyFunctionality();

            Console.WriteLine("\n-- Testing Down Arrow Key Functionality --");
            TestDownArrowKeyFunctionality();

            Console.WriteLine("\n-- Testing Invalid Number Input --");
            TestInvalidInput();

            Console.WriteLine("\n-- Verifying footer link --");
            VerifyFooterLink();

            Console.WriteLine("\n-- Verifying Github link --");
            VerifyGithubLink();
        }

        public void TestValidNumberInput()
        {
            var input = driver.FindElement(numberInputSelector);
            input.Clear();
            input.SendKeys("12345");
            Console.WriteLine($"Typed 12345. Current value: {input.GetAttribute("value")}");
        }

        public void TestUpArrowKeyFunctionality()
        {
            var input = driver.FindElement(numberInputSelector);
            

            input.Clear();
            input.SendKeys("5");

            Console.WriteLine($"Before arrow key usage, value is: {input.GetAttribute("value")}");

            Actions actions = new Actions(driver);
            actions.SendKeys(Keys.ArrowUp).Perform();
            actions.SendKeys(Keys.ArrowUp).Perform();
            actions.SendKeys(Keys.ArrowUp).Perform();

            Console.WriteLine($"After arrow key usage, value is: {input.GetAttribute("value")}");
        }

        public void TestDownArrowKeyFunctionality()
        {
            var input = driver.FindElement(numberInputSelector);


            input.Clear();
            input.SendKeys("5");

            Console.WriteLine($"Before arrow key usage, value is: {input.GetAttribute("value")}");

            Actions actions = new Actions(driver);
            actions.SendKeys(Keys.ArrowDown).Perform();
            actions.SendKeys(Keys.ArrowDown).Perform();
            actions.SendKeys(Keys.ArrowDown).Perform();

            Console.WriteLine($"After arrow key usage, value is: {input.GetAttribute("value")}");
        }

        public void TestInvalidInput()
        {
            var input = driver.FindElement(numberInputSelector);
            input.Clear();
            input.SendKeys("abc");
            Thread.Sleep(500);
            string value = input.GetAttribute("value");

            if (string.IsNullOrEmpty(value))
            {
                Console.WriteLine("Invalid non-numeric input 'abc' not accepted.");
            }
            else
            {
                Console.WriteLine($"Non-numeric input was accepted. Current value: {value}");
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
    }
}
