using OpenQA.Selenium;
using System;

namespace TheInternetTest.Pages
{
    public class BasicAuthPage
    {
        private IWebDriver driver; 
        private By footerLinkSelector = By.CssSelector("#page-footer a");
        private By githubRibbonSelector = By.CssSelector("a[href='https://github.com/tourdedave/the-internet'] img[alt='Fork me on GitHub']");

        public BasicAuthPage(IWebDriver webDriver)
        {
            driver = webDriver;
        }

        public void ExecuteAllTests()
        {
            Console.WriteLine("\n-- Verifying Negative test cases --");
            NegativeAuthTest("wronguser", "wrongpass");
            NegativeAuthTest("", "");
            NegativeAuthTest("wronguser", "admin");
            NegativeAuthTest("admin", "wrongpass");
            NegativeAuthTest("", "wrongpass");
            NegativeAuthTest("wronguser", "");
            NegativeAuthTest("admin", "");
            NegativeAuthTest("", "admin");

            Console.WriteLine("\n-- Verifying Positive test case --");
            PositiveAuthTest("admin", "admin");

            Console.WriteLine("\n-- Verifying footer link --");
            VerifyFooterLink();

            Console.WriteLine("\n-- Verifying Github link --");
            VerifyGithubLink();
        }

        public void PositiveAuthTest(string username, string password)
        {
            string authUrl = $"https://{username}:{password}@the-internet.herokuapp.com/basic_auth";
            driver.Navigate().GoToUrl(authUrl);

            try
            {
                var message = driver.FindElement(By.CssSelector("div.example p")).Text;

                if (message.Contains("Congratulations!"))
                {
                    Console.WriteLine($"Positive Test Passed with credentials {username}:{password}");
                    Console.WriteLine($"Successful signup link: {driver.Url}");
                                        
                }
                else
                {
                    Console.WriteLine($"Positive Test Failed: Unexpected content with {username}:{password}");
                }
            }
            catch (NoSuchElementException)
            {
                Console.WriteLine($"Positive Test Failed: Element not found with {username}:{password}");
            }
        }

        public void NegativeAuthTest(string username, string password)
        {
            string authUrl = $"https://{username}:{password}@the-internet.herokuapp.com/basic_auth";
            driver.Navigate().GoToUrl(authUrl);

            bool isUnauthorized = false;

            try
            {
                var message = driver.FindElement(By.CssSelector("div.example p")).Text;
                if (message.Contains("Congratulations!"))
                {
                    Console.WriteLine($"Negative Test Failed: Unexpected success with {username}:{password}");
                }
                else
                {
                    Console.WriteLine($"Negative Test Passed: No success message with {username}:{password}");
                    isUnauthorized = true;
                }
            }
            catch (NoSuchElementException)
            {
                Console.WriteLine($"Negative Test Passed: Page blocked or element missing for {username}:{password}");
                isUnauthorized = true;
            }

            if (isUnauthorized)
            {
                Console.WriteLine($"Test result: Unauthorized access attempt correctly handled for {username}:{password}");
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
    }
}
