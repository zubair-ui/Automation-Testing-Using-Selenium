using OpenQA.Selenium;
using OpenQA.Selenium.Interactions; 

namespace TheInternetTest.Pages
{
    public class JQueryUIMenuPage
    {
        private IWebDriver driver;
        private string url = "https://the-internet.herokuapp.com/jqueryui/menu";
        private Actions actions;
        private By footerLinkSelector = By.CssSelector("#page-footer a");
        private By githubRibbonSelector = By.CssSelector("a[href='https://github.com/tourdedave/the-internet'] img[alt='Fork me on GitHub']");

        public JQueryUIMenuPage(IWebDriver driver)
        {
            this.driver = driver;
            actions = new Actions(driver);
            driver.Navigate().GoToUrl(url);
            Console.WriteLine($"Navigated to {url}");
        }

        public void ExecuteAllTests()
        {
            Console.WriteLine("-- Validating Hover and Downloading files --");
            HoverAndClickDownload("PDF");
            driver.Navigate().Refresh();
            HoverAndClickDownload("CSV");
            driver.Navigate().Refresh();
            HoverAndClickDownload("Excel");

            Console.WriteLine("\n-- Testing Back to JQueryUi link --"); 
            TestBackToJQueryUI();

            Console.WriteLine("\n-- Validating Official JQuery Menu Page --");
            ValidateOfficialJQueryMenuPage();

            Console.WriteLine("\n-- Verifying footer link --");
            VerifyFooterLink();

            Console.WriteLine("\n-- Verifying Github link --");
            VerifyGithubLink();
        }

        private void HoverAndClickDownload(string option)
        {
            try
            {
                // Hover over 'Enabled'
                var enabledMenu = driver.FindElement(By.XPath("//a[text()='Enabled']"));
                actions.MoveToElement(enabledMenu).Perform();
                Thread.Sleep(700);

                // Hover over 'Downloads'
                var downloads = driver.FindElement(By.XPath("//a[text()='Downloads']"));
                actions.MoveToElement(downloads).Perform();
                Thread.Sleep(700);

                // Wait for the specific option to become visible
                var targetOption = driver.FindElement(By.XPath($"//a[text()='{option}']"));

                // Check if visible and enabled before clicking
                if (targetOption.Displayed && targetOption.Enabled)
                {
                    string href = targetOption.GetAttribute("href");
                    actions.MoveToElement(targetOption).Click().Perform();
                    Console.WriteLine($"Clicked '{option}' and navigated to: {href}");
                    Thread.Sleep(1000);
                    Console.WriteLine($"Current URL: {driver.Url}");
                }
                else
                {
                    Console.WriteLine($"{option} is not visible or interactable.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception during hover-click for '{option}': {ex.Message}");
            }
        }


        private void TestBackToJQueryUI()
        {
            try
            {
                // Ensure we're back to the correct page after download actions.
                driver.Navigate().GoToUrl(url);
                Thread.Sleep(1000);  // Ensure page load

                bool isBackLinkVisible = false;

                // Trigger hover over the "Enabled" menu item to open the submenus
                var enabledMenu = driver.FindElement(By.XPath("//a[text()='Enabled']"));
                actions.MoveToElement(enabledMenu).Perform();
                Thread.Sleep(500);  // Wait for menu to expand

                // Trigger hover over the "Downloads" menu item to open the submenus
                var downloadsMenu = driver.FindElement(By.XPath("//a[text()='Downloads']"));
                actions.MoveToElement(downloadsMenu).Perform();
                Thread.Sleep(500);  // Wait for submenu to appear

                // Now try to find the "Back to JQuery UI" link inside the visible menu
                for (int i = 0; i < 5; i++)
                {
                    try
                    {
                        var backLink = driver.FindElement(By.LinkText("Back to JQuery UI"));

                        // Ensure the element is visible, enabled, and interactable
                        if (backLink.Displayed && backLink.Enabled && backLink.Size.Height > 0 && backLink.Size.Width > 0)
                        {
                            // Scroll into view to make sure it's interactable
                            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", backLink);
                            backLink.Click();
                            isBackLinkVisible = true;
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Back to JQuery UI link is not interactable.");
                        }
                    }
                    catch (NoSuchElementException)
                    {
                        Console.WriteLine("Back to JQuery UI link not found, retrying...");
                        Thread.Sleep(500); // Wait for half a second before retrying
                    }
                }

                if (!isBackLinkVisible)
                {
                    Console.WriteLine("Back to JQuery UI link not found or not clickable after retries.");
                    return;
                }

                // Wait a bit to ensure the navigation is complete
                Thread.Sleep(1000);

                // Check if the URL matches the expected result
                string expectedUrl = "https://the-internet.herokuapp.com/jqueryui";
                if (driver.Url == expectedUrl)
                {
                    Console.WriteLine("Back to JQuery UI link works correctly.");
                }
                else
                {
                    Console.WriteLine($"Failed to go back. Current URL: {driver.Url}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Test Exception: {ex.Message}");
            }
        }




        private void ValidateOfficialJQueryMenuPage()
        {
            try
            {
                // Navigate directly to the page where we can find the official jQuery UI link
                driver.Navigate().GoToUrl("https://the-internet.herokuapp.com/jqueryui/menu");
                Thread.Sleep(1000);

                // Ensure the menu page is loaded and the doc link is visible
                var docLink = driver.FindElement(By.CssSelector("a[href='http://api.jqueryui.com/menu/']"));

                // Retrieve the link's href attribute to verify the target URL
                string officialLinkHref = docLink.GetAttribute("href");

                if (officialLinkHref.Contains("jqueryui.com/menu"))
                {
                    Console.WriteLine($"Official jQuery UI Menu page link is correct: {officialLinkHref}");
                }
                else
                {
                    Console.WriteLine($"❌ Unexpected link for official jQuery UI Menu page: {officialLinkHref}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception validating official jQuery UI page: {ex.Message}");
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
