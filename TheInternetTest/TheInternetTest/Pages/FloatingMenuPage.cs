using OpenQA.Selenium; 

namespace TheInternetTest.Pages
{
    public class FloatingMenuPage
    {
        private readonly IWebDriver driver;
        private By footerLinkSelector = By.CssSelector("#page-footer a");
        private readonly string url = "https://the-internet.herokuapp.com/floating_menu"; 
        private By githubRibbonSelector = By.CssSelector("a[href='https://github.com/tourdedave/the-internet'] img[alt='Fork me on GitHub']");


        // Locators for menu links
        private readonly By menuLinks = By.CssSelector("#menu a");

        public FloatingMenuPage(IWebDriver driver)
        {
            this.driver = driver;
            driver.Navigate().GoToUrl(url);
            Console.WriteLine($"Navigated to {url}");
        }


        public bool IsFloatingMenuDisplayed()
        {
            return driver.FindElement(By.Id("menu")).Displayed;
        }

        public List<string> GetMenuLinkTexts()
        {
            var elements = driver.FindElements(menuLinks);
            List<string> linkTexts = new List<string>();

            foreach (var element in elements)
            {
                linkTexts.Add(element.Text);
            }

            return linkTexts;
        }

        public void ClickAllLinks()
        {
            var elements = driver.FindElements(menuLinks);

            foreach (var element in elements)
            {
                try
                {
                    Console.WriteLine($"Clicking link: {element.Text}");
                    element.Click();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Failed to click {element.Text}: {ex.Message}");
                }
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

        public void ExecuteAllTests()
        {
            

            Console.WriteLine("\n-- Checking if floating menu is displayed --");
            bool isDisplayed = IsFloatingMenuDisplayed();
            Console.WriteLine($"Floating Menu displayed: {isDisplayed}");

            if (isDisplayed)
            {
                Console.WriteLine("\n-- Getting all menu link texts --");
                var linkTexts = GetMenuLinkTexts();
                Console.WriteLine("Menu Links:");
                foreach (var text in linkTexts)
                {
                    Console.WriteLine($" - {text}");
                }

                Console.WriteLine("\n-- Clicking all menu links --");
                ClickAllLinks();
            }

            Console.WriteLine("\n-- Verifying footer link --");
            VerifyFooterLink();

            Console.WriteLine("\n-- Verifying Github link --");
            VerifyGithubLink();
        }
    }
}
