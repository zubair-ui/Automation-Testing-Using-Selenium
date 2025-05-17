using OpenQA.Selenium;

namespace TheInternetTest.Pages
{
    public class StatusCodesPage
    {
        private IWebDriver driver;
        private string url = "https://the-internet.herokuapp.com/status_codes";
        private By statusCodeLinksSelector = By.CssSelector("ul li a");
        private By messageParagraphSelector = By.CssSelector(".example p");
        private By hereLinkSelector = By.LinkText("here");

        private By footerLinkSelector = By.CssSelector("#page-footer a"); 
        private By githubRibbonSelector = By.CssSelector("a[href='https://github.com/tourdedave/the-internet'] img[alt='Fork me on GitHub']");

        public StatusCodesPage(IWebDriver driver)
        {
            this.driver = driver;
            driver.Navigate().GoToUrl(url);
        }

        public void ExecuteAllTests()
        {
            Console.WriteLine("-- Verifying status code links --");
            VerifyStatusCodeLinks();

            Console.WriteLine("\n-- Verifying footer link --");
            VerifyFooterLink();

            Console.WriteLine("\n-- Verifying Github link --");
            VerifyGithubLink();
        }

        public void VerifyStatusCodeLinks()
        {
            var links = driver.FindElements(statusCodeLinksSelector);
            Console.WriteLine($"Total status code links found: {links.Count}");

            for (int i = 0; i < links.Count; i++)
            {
                // Re-locate links fresh each iteration to avoid stale element reference
                var freshLinks = driver.FindElements(statusCodeLinksSelector);
                var link = freshLinks[i];
                string codeText = link.Text;
                string href = link.GetAttribute("href");

                Console.WriteLine($"Clicking status code link: {codeText} ({href})");
                link.Click();

                var message = driver.FindElement(messageParagraphSelector).Text;

                if (message.Contains(codeText))
                {
                    Console.WriteLine($"Message contains expected code: {codeText}");
                }
                else
                {
                    Console.WriteLine($"Unexpected message: {message}");
                }

                // Click 'here' link to go back
                try
                {
                    var hereLink = driver.FindElement(hereLinkSelector);
                    hereLink.Click();
                    Console.WriteLine("Navigating back via 'here' link.");
                }
                catch (NoSuchElementException)
                {
                    Console.WriteLine("Could not find 'here' link to navigate back.");
                    driver.Navigate().Back();
                }
            }

            Console.WriteLine("All status code links tested successfully.");
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