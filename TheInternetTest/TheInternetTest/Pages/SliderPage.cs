using OpenQA.Selenium;
using OpenQA.Selenium.Interactions; 

namespace TheInternetTest.Pages
{
    public class SliderPage
    {
        private IWebDriver driver;
        private string url = "https://the-internet.herokuapp.com/horizontal_slider";
        private By sliderSelector = By.CssSelector("input[type='range']");
        private By sliderValueSelector = By.Id("range");
        private By footerLinkSelector = By.CssSelector("#page-footer a");
        private By githubRibbonSelector = By.CssSelector("a[href='https://github.com/tourdedave/the-internet'] img[alt='Fork me on GitHub']");

        public SliderPage(IWebDriver driver)
        {
            this.driver = driver;
            driver.Navigate().GoToUrl(url);
            Console.WriteLine($"Navigated to {url}");
        }

        public void ExecuteAllTests()
        {
            Console.WriteLine("-- Testing Slider Movement --");
            MoveSliderToValue("4.5");
            MoveSliderToValue("5.0");
            MoveSliderToValue("3.5");
            MoveSliderToValue("1.0");
            MoveSliderToValue("2.0");

            Console.WriteLine("\n-- Verifying footer link --");
            VerifyFooterLink();

            Console.WriteLine("\n-- Verifying Github link --");
            VerifyGithubLink();
        }

        public void MoveSliderToValue(string targetValue)
        {
            var slider = driver.FindElement(sliderSelector);
            var valueLabel = driver.FindElement(sliderValueSelector);
            Actions actions = new Actions(driver);

            double currentValue = Convert.ToDouble(valueLabel.Text);
            double target = Convert.ToDouble(targetValue);

            if (currentValue == target)
            {
                Console.WriteLine($"Slider already at target value: {targetValue}");
                return;
            }

            Console.WriteLine($"Moving slider from {currentValue} to {targetValue}");

            actions.Click(slider).Perform();
            System.Threading.Thread.Sleep(100);

            while (currentValue != target)
            {
                if (currentValue < target)
                {
                    actions.SendKeys(Keys.ArrowRight).Perform();
                }
                else
                {
                    actions.SendKeys(Keys.ArrowLeft).Perform();
                }

                System.Threading.Thread.Sleep(100); // Let UI update
                currentValue = Convert.ToDouble(valueLabel.Text);
            }

            Console.WriteLine($"Final slider value: {currentValue}");
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
