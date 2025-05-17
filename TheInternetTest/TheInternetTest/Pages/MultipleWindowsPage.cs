using OpenQA.Selenium; 

namespace TheInternetTest.Pages
{
    public class MultipleWindowsPage
    {
        private IWebDriver driver;
        private string url = "https://the-internet.herokuapp.com/windows";
        private By githubRibbonSelector = By.CssSelector("a[href='https://github.com/tourdedave/the-internet'] img[alt='Fork me on GitHub']");
        private By footerLinkSelector = By.CssSelector("#page-footer a");
        private By headingSelector = By.CssSelector("div.example h3");
        private By clickHereLinkSelector = By.LinkText("Click Here");

        public MultipleWindowsPage(IWebDriver driver)
        {
            this.driver = driver;
            driver.Navigate().GoToUrl(url);
            Console.WriteLine($"Navigated to {url}");
        }

        public void ExecuteAllTests()
        {
            Console.WriteLine("-- Verifying heading --");
            VerifyPageHeading();

            Console.WriteLine("\n-- Verifying new window --");
            OpenNewWindowAndVerify();

            Console.WriteLine("\n-- Verifying footer link --");
            VerifyFooterLink();

            Console.WriteLine("\n-- Verifying Github link --");
            VerifyGithubLink();
        }

        public void VerifyPageHeading()
        {
            var heading = driver.FindElement(headingSelector);
            string headingText = heading.Text;
            Console.WriteLine($"Page heading: {headingText}");
            if (headingText != "Opening a new window")
            {
                throw new Exception("Page heading text mismatch.");
            }
            else
            {
                Console.WriteLine("Page heading verified.");
            }
        }

        public void OpenNewWindowAndVerify()
        {
            string originalWindow = driver.CurrentWindowHandle;
            Console.WriteLine($"Original window handle: {originalWindow}");

            driver.FindElement(clickHereLinkSelector).Click();
            Console.WriteLine("Clicked 'Click Here' link to open new window.");

            // Wait for new window - simple sleep or explicit wait could be used here
            System.Threading.Thread.Sleep(1000);

            var windowHandles = driver.WindowHandles;
            Console.WriteLine($"Number of open windows: {windowHandles.Count}");

            foreach (string handle in windowHandles)
            {
                if (handle != originalWindow)
                {
                    driver.SwitchTo().Window(handle);
                    Console.WriteLine($"Switched to new window: {handle}");
                    string newWindowTitle = driver.Title;
                    Console.WriteLine($"New window title: {newWindowTitle}");

                    // The new window page shows heading "New Window"
                    var newWindowHeading = driver.FindElement(By.CssSelector("div.example h3")).Text;
                    Console.WriteLine($"New window heading: {newWindowHeading}");
                    if (newWindowHeading != "New Window")
                    {
                        throw new Exception("New window heading text mismatch.");
                    }
                    else
                    {
                        Console.WriteLine("New window heading verified.");
                    }

                    // Close new window and switch back
                    driver.Close();
                    Console.WriteLine("Closed new window.");
                }
            }

            driver.SwitchTo().Window(originalWindow);
            Console.WriteLine("Switched back to original window.");
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
