using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI; 

namespace TheInternetTest.Pages
{
    public class DynamicLoadingPage
    {
        private readonly IWebDriver driver;
        private readonly WebDriverWait wait;

        private readonly string url = "https://the-internet.herokuapp.com/dynamic_loading";

        // Selectors
        private readonly By example1Link = By.LinkText("Example 1: Element on page that is hidden");
        private readonly By example2Link = By.LinkText("Example 2: Element rendered after the fact");

        private readonly By footerLinkSelector = By.CssSelector("#page-footer a[target='_blank']");
        private readonly By githubRibbonSelector = By.CssSelector("a[href='https://github.com/tourdedave/the-internet'] img");

        public DynamicLoadingPage(IWebDriver driver)
        {
            this.driver = driver;
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            driver.Navigate().GoToUrl(url);
            Console.WriteLine($"Navigated to {url}");
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

        public void ClickExample1()
        {
            var link = wait.Until(drv => drv.FindElement(example1Link));
            Console.WriteLine($"Clicking: {link.Text}");
            link.Click();

            var example1Page = new DynamicLoadingExample1Page(driver);
            example1Page.StartLoading();

            driver.Navigate().Back();
        }

        public void ClickExample2()
        {
            driver.Navigate().GoToUrl(url);
            var link = wait.Until(drv => drv.FindElement(example2Link));
            Console.WriteLine($"Clicking: {link.Text}");
            link.Click();

            var example2Page = new DynamicLoadingExample2Page(driver);
            example2Page.StartLoading();

            driver.Navigate().Back();
        }

         
        public void ExecuteAllTests()
        {

            // Click example links and return back
            ClickExample1();

            ClickExample2();

            Console.WriteLine("\n-- Verifying footer link --");
            VerifyFooterLink();

            Console.WriteLine("\n-- Verifying Github link --");
            VerifyGithubLink();

        }
    }
}
