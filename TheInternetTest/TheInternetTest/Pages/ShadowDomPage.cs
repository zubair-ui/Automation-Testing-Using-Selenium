using OpenQA.Selenium; 

namespace TheInternetTest.Pages
{
    public class ShadowDomPage
    {
        private IWebDriver driver;
        private string url = "https://the-internet.herokuapp.com/shadowdom";

        private By headerSelector = By.TagName("h1");
        private By footerLinkSelector = By.CssSelector("#page-footer a");
        private By githubRibbonSelector = By.CssSelector("a[href='https://github.com/tourdedave/the-internet'] img[alt='Fork me on GitHub']");

        public ShadowDomPage(IWebDriver driver)
        {
            this.driver = driver;
            driver.Navigate().GoToUrl(url);
            Console.WriteLine($"Navigated to {url}");
        }

        public void ExecuteAllTests()
        {
            Console.WriteLine("\n-- Verifying Page Header --");
            VerifyPageHeader();

            Console.WriteLine("\n-- Verifying Shadow DOM Content --");
            GetShadowDomContent();

            Console.WriteLine("\n-- Verifying footer link --");
            VerifyFooterLink();

            Console.WriteLine("\n-- Verifying Github link --");
            VerifyGithubLink();
        }

        public void VerifyPageHeader()
        {
            var header = driver.FindElement(headerSelector).Text;
            if (header == "Simple template")
            {
                Console.WriteLine($"Page header is correct: {header}");
            }
            else
            {
                Console.WriteLine($"Unexpected page header: {header}");
            }
        }

        public void GetShadowDomContent()
        {
            try
            {
                // Get number of my-paragraph elements
                var count = (long)((IJavaScriptExecutor)driver).ExecuteScript(
                    "return document.querySelectorAll('my-paragraph').length;"
                );
                Console.WriteLine($"Found {count} 'my-paragraph' elements.");

                // Fetch text content from the first slot inside shadow DOM
                string slotText = (string)((IJavaScriptExecutor)driver).ExecuteScript(@"
                    const paragraph = document.querySelector('my-paragraph');
                    const shadowRoot = paragraph.shadowRoot;
                    const slot = shadowRoot.querySelector('slot');
                    return slot.assignedNodes().map(n => n.textContent.trim()).join(', ');
                ");

                Console.WriteLine($"Shadow DOM slot content: {slotText}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error accessing Shadow DOM: {ex.Message}");
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
