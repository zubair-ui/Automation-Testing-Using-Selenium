using OpenQA.Selenium; 

namespace TheInternetTest.Pages
{
    public class RedirectionPage
    {
        private IWebDriver driver;
        private string url = "https://the-internet.herokuapp.com/redirector";

        private By redirectLinkSelector = By.Id("redirect");
        private By statusCodesHeaderSelector = By.CssSelector(".example h3");
        private By responseTextSelector = By.CssSelector(".example p");
        private By footerLinkSelector = By.CssSelector("#page-footer a");
        private By githubRibbonSelector = By.CssSelector("a[href='https://github.com/tourdedave/the-internet'] img[alt='Fork me on GitHub']");

        public RedirectionPage(IWebDriver driver)
        {
            this.driver = driver;
            driver.Navigate().GoToUrl(url);
            Console.WriteLine($"Navigated to {url}");
        }

        public void ExecuteAllTests()
        {
            Console.WriteLine("\n-- Clicking Redirect Link --");
            ClickRedirectLink();

            Console.WriteLine("\n-- Verifying Status Codes Page --");
            VerifyStatusCodesPage();

            Console.WriteLine("\n-- Clicking Status Code 200 Link --");
            ClickStatusCodeLink("200");

            Console.WriteLine("\n-- Clicking Status Code 301 Link --");
            ClickStatusCodeLink("301");

            Console.WriteLine("\n-- Clicking Status Code 404 Link --");
            ClickStatusCodeLink("404");

            Console.WriteLine("\n-- Clicking Status Code 500 Link --");
            ClickStatusCodeLink("500");

            Console.WriteLine("\n-- Verifying Response Text --");
            VerifyResponseText();

            Console.WriteLine("\n-- Verifying footer link --");
            VerifyFooterLink();

            Console.WriteLine("\n-- Verifying Github link --");
            VerifyGithubLink();
        }

        public void ClickRedirectLink()
        {
            var redirectLink = driver.FindElement(redirectLinkSelector);
            redirectLink.Click();
            Console.WriteLine("Clicked on Redirect link.");
        }

        public void VerifyStatusCodesPage()
        {
            var header = driver.FindElement(statusCodesHeaderSelector).Text;
            if (header.Contains("Status Codes"))
            {
                Console.WriteLine($"Landed on Status Codes page with header: {header}");
            }
            else
            {
                Console.WriteLine("Failed to navigate to Status Codes page.");
            }
        }

        public void ClickStatusCodeLink(string code)
        {
            var statusCodeLink = driver.FindElement(By.LinkText(code));
            statusCodeLink.Click();
            Console.WriteLine($"Clicked on Status Code {code} link.");
            driver.Navigate().Back();
        }

        public void VerifyResponseText()
        {
            var responseText = driver.FindElement(responseTextSelector).Text;
            Console.WriteLine("Response page content:");
            Console.WriteLine(responseText);
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
