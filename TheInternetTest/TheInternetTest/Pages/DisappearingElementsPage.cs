using OpenQA.Selenium;

namespace TheInternetTest.Pages
{
    public class DisappearingElementsPage
    {
        private IWebDriver driver;
        private string url = "https://the-internet.herokuapp.com/disappearing_elements";
        private By menuItemsSelector = By.CssSelector("ul li a");
        private By footerLinkSelector = By.CssSelector("#page-footer a");
        private By githubRibbonSelector = By.CssSelector("a[href='https://github.com/tourdedave/the-internet'] img[alt='Fork me on GitHub']");

        public DisappearingElementsPage(IWebDriver driver)
        {
            this.driver = driver;
            driver.Navigate().GoToUrl(url);
            Console.WriteLine($"Navigated to {url}");
        }

        public IList<IWebElement> GetMenuItems()
        {
            return driver.FindElements(menuItemsSelector);
        }
         

        public void ClickMenuItemIfExists(string menuText)
        {
            var menuItems = GetMenuItems();
            var item = menuItems.FirstOrDefault(e => e.Text.Equals(menuText, StringComparison.OrdinalIgnoreCase));

            if (item != null)
            {
                Console.WriteLine($"Clicking menu item: {menuText}");
                item.Click();
                driver.Navigate().Back();
            }
            else
            {
                Console.WriteLine($"Menu item '{menuText}' not found.");
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
                    githubRibbon.Click();

                    driver.Navigate().Back();
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

            Console.WriteLine("\n-- Clicking 'Home' menu item if available --");
            ClickMenuItemIfExists("Home");

            Console.WriteLine("\n-- Clicking 'About' menu item if available --");
            ClickMenuItemIfExists("About");

            Console.WriteLine("\n-- Clicking 'Contact Us' menu item if available --");
            ClickMenuItemIfExists("Contact Us");

            Console.WriteLine("\n-- Clicking 'Portfolio' menu item if available --");
            ClickMenuItemIfExists("Portfolio");

            Console.WriteLine("\n-- Verifying footer link --");
            VerifyFooterLink();

            Console.WriteLine("\n-- Verifying GitHub link --");
            VerifyGithubLink();
        }
    }
}
