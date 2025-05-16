using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI; 

namespace TheInternetTest.Pages
{
    public class DynamicLoadingExample1Page
    {
        private readonly IWebDriver driver;
        private readonly WebDriverWait wait;

        private readonly By startButton = By.CssSelector("#start button");
        private readonly By loadingIndicator = By.Id("loading");
        private readonly By finishText = By.CssSelector("#finish h4");

        public DynamicLoadingExample1Page(IWebDriver driver)
        {
            this.driver = driver;
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            Console.WriteLine("On Example 1 page");
        }

        public void StartLoading()
        {
            wait.Until(d => d.FindElement(startButton)).Click();
            Console.WriteLine("Clicked Start button");

            // Wait for loading indicator to appear and then disappear
            wait.Until(d => d.FindElement(loadingIndicator).Displayed);
            wait.Until(d => !d.FindElement(loadingIndicator).Displayed);

            // Wait for finish text to appear
            wait.Until(d => d.FindElement(finishText).Displayed);

            string text = driver.FindElement(finishText).Text;
            Console.WriteLine($"Finish text: {text}");
        }
    }
}
