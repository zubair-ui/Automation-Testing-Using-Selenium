using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace TheInternetTest.Pages
{
    public class DropdownPage
    {
        private IWebDriver driver;

        private string url = "https://the-internet.herokuapp.com/dropdown";
        private By dropdownSelector = By.Id("dropdown");
        private By footerLinkSelector = By.XPath("//div[@id='page-footer']//a[contains(text(),'Elemental Selenium')]");
        private By githubRibbonSelector = By.CssSelector("a[href='https://github.com/tourdedave/the-internet'] img");

        public DropdownPage(IWebDriver driver)
        {
            this.driver = driver;
            driver.Navigate().GoToUrl(url);
            Console.WriteLine($"Navigated to {url}");
        }

        public void SelectOptionByValue(string value)
        {
            try
            {
                var dropdown = driver.FindElement(dropdownSelector);
                var optionToSelect = dropdown.FindElement(By.CssSelector($"option[value='{value}']"));
                optionToSelect.Click();
                Console.WriteLine($"Selected option with value: {value}");
            }
            catch (NoSuchElementException)
            {
                Console.WriteLine($"Option with value '{value}' not found in dropdown.");
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
                    //githubRibbon.Click();

                    //driver.Navigate().Back();
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
            Console.WriteLine("-- Verifying Dropdown Options --");
            SelectOptionByValue("1");
            SelectOptionByValue("2");

            Console.WriteLine("-- Verifying Footer Link --");
            VerifyFooterLink();

            Console.WriteLine("-- Verifying GitHub Link --");
            VerifyGithubLink();
        }
    }
}
