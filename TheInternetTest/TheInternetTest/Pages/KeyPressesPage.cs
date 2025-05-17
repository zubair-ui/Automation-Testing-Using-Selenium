using OpenQA.Selenium; 

namespace TheInternetApp.Pages
{
    public class KeyPressesPage
    {
        private IWebDriver driver;
        private By footerLinkSelector = By.CssSelector("#page-footer a"); 
        private By githubRibbonSelector = By.CssSelector("a[href='https://github.com/tourdedave/the-internet'] img[alt='Fork me on GitHub']");
        private string url = "https://the-internet.herokuapp.com/key_presses";

        // Constructor
        public KeyPressesPage(IWebDriver driver)
        {
            this.driver = driver;
            driver.Navigate().GoToUrl(url);
            Console.WriteLine($"Navigated to {url}");
        }

        // Locators
        private By inputField = By.Id("target");
        private By resultText = By.Id("result");

        // Method to send a keypress to the input field
        private void PressKey(string key)
        {
            driver.FindElement(inputField).SendKeys(key);
        }

        // Method to get the result text after keypress
        private string GetResultText()
        {
            return driver.FindElement(resultText).Text;
        }

        // Method to perform all keypress tests
        public void ExecuteAllTests()
        {
            Console.WriteLine("-- Verifying Key Press results --");
            string[] testKeys = new string[]
            {
        "A", "B", "C", "D", "E", "F", "G",  "P",  "U", "V", "W", "X", "Y", "Z","1", "2", "3", "4", "5", "6", "7", "8", "9", "0",
        " ", "[", "]", "{", "}", "(", ")", "<", ">", "@", "#", "$", "%", "^", "&", "*", "-", "_", "=", "+", "|", ";", ":", "'", "\"", ",", ".", "/",
        "~", "`", "_", "\\"
            };

            // Iterate over the array and simulate key presses
            foreach (string key in testKeys)
            {
                PressKey(key);  // Press the key
                Thread.Sleep(100);
                string result = GetResultText();  // Get the updated text/result after the key press
                Console.WriteLine($"Pressed: {key}, Page showed: {result}");  // Output the result for observation
            }


            Console.WriteLine("\n-- Verifying footer link --");
            VerifyFooterLink();

            Console.WriteLine("\n-- Verifying Github link --");
            VerifyGithubLink();
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
