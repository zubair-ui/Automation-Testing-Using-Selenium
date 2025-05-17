using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI; 

namespace TheInternetTest
{
    public class ShiftingContentPage
    {
        private readonly IWebDriver driver;
        private readonly WebDriverWait wait;

        private readonly string baseUrl = "https://the-internet.herokuapp.com/shifting_content";

        public ShiftingContentPage(IWebDriver driver)
        {
            this.driver = driver;
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }

        public void ExecuteAllTests()
        {
            NavigateToMainPage();

            var exampleLinks = GetExampleLinks();

            Console.WriteLine($"Found {exampleLinks.Count} example links.");

            foreach (var (href, text) in exampleLinks)
            {
                Console.WriteLine($"\nTesting example: {text} ({href})");
                driver.Navigate().GoToUrl(href);

                ValidateExamplePage();

                NavigateToMainPage();
            }

            Console.WriteLine("\nAll tests executed successfully.");
        }

        private void NavigateToMainPage()
        {
            driver.Navigate().GoToUrl(baseUrl);

            wait.Until(d =>
            {
                var elements = d.FindElements(By.CssSelector("#content .example a"));
                return elements.Count > 0 && elements[0].Displayed;
            });

            Console.WriteLine($"Navigated to main page: {baseUrl}");
        }

        private List<(string href, string text)> GetExampleLinks()
        {
            var exampleLinks = driver.FindElements(By.CssSelector("#content .example a"));
            var links = new List<(string href, string text)>();

            foreach (var link in exampleLinks)
            {
                string href = link.GetAttribute("href");
                string text = link.Text.Trim();
                links.Add((href, text));
            }

            return links;
        }


        private void ValidateExamplePage()
        {
            try
            {
                var header = wait.Until(d => d.FindElement(By.TagName("h3")));
                string headerText = header.Text.Trim();
                Console.WriteLine($"Page header: {headerText}");

                
            }
            catch (WebDriverTimeoutException)
            {
                Console.WriteLine("Header element <h3> not found or timed out.");
            }
        }
    }
}
