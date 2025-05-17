using OpenQA.Selenium;
using System;

namespace TheInternetTest.Pages
{
    public class FormAuthenticationPage
    {
        private IWebDriver driver;
        private string url = "https://the-internet.herokuapp.com/login";

        // Locators
        private By usernameInput = By.Id("username");
        private By passwordInput = By.Id("password");
        private By loginButton = By.CssSelector("button[type='submit']");
        private By flashMessage = By.Id("flash");
        private By logoutButton = By.CssSelector("a.button.secondary.radius");
        private By footerLinkSelector = By.CssSelector("#page-footer a"); 
        private By githubRibbonSelector = By.CssSelector("a[href='https://github.com/tourdedave/the-internet'] img[alt='Fork me on GitHub']");


        public FormAuthenticationPage(IWebDriver webDriver)
        {
            driver = webDriver;
            driver.Navigate().GoToUrl(url);
            Console.WriteLine($"Navigated to {url}");
        }

        public void ExecuteAllTests()
        {
            Console.WriteLine("\n-- Running Negative Login Tests --");
            NegativeLoginTest("", "");                  // empty username, empty password
            NegativeLoginTest("wrongUser", "wrongPass"); // wrong credentials
            NegativeLoginTest("tomsmith", "wrongPass");  // correct user, wrong password
            NegativeLoginTest("", "SuperSecretPassword!");// empty username, correct password

            Console.WriteLine("\n-- Running Positive Login Test --");
            PositiveLoginTest("tomsmith", "SuperSecretPassword!");

            Console.WriteLine("\n-- Running Logout Test --");
            LogoutTest();
             

            Console.WriteLine("\n-- Verifying footer link --");
            VerifyFooterLink();

            Console.WriteLine("\n-- Verifying Github link --");
            VerifyGithubLink();
        }

        public void PositiveLoginTest(string username, string password)
        {
            
            driver.FindElement(usernameInput).Clear();
            driver.FindElement(usernameInput).SendKeys(username);

            driver.FindElement(passwordInput).Clear();
            driver.FindElement(passwordInput).SendKeys(password);

            driver.FindElement(loginButton).Click();

            try
            {
                var flashText = driver.FindElement(flashMessage).Text;

                if (flashText.Contains("You logged into a secure area!"))
                {
                    Console.WriteLine("Positive login test passed.");
                }
                else
                {
                    Console.WriteLine("Positive login test failed: Unexpected flash message.");
                }
            }
            catch (NoSuchElementException)
            {
                Console.WriteLine("Positive login test failed: Flash message not found.");
            }
        }

        public void NegativeLoginTest(string username, string password)
        {
            driver.Navigate().GoToUrl(url);  // reload login page

            driver.FindElement(usernameInput).Clear();
            driver.FindElement(usernameInput).SendKeys(username);

            driver.FindElement(passwordInput).Clear();
            driver.FindElement(passwordInput).SendKeys(password);

            driver.FindElement(loginButton).Click();

            try
            {
                var flashText = driver.FindElement(flashMessage).Text;

                if (flashText.Contains("Your username is invalid!") || flashText.Contains("Your password is invalid!"))
                {
                    Console.WriteLine($"Negative login test passed for username='{username}' password='{password}'.");
                }
                else
                {
                    Console.WriteLine($"Negative login test failed for username='{username}' password='{password}': Unexpected flash message.");
                }
            }
            catch (NoSuchElementException)
            {
                Console.WriteLine($"Negative login test failed for username='{username}' password='{password}': Flash message not found.");
            }
        }


        public void LogoutTest()
        {
            try
            {
                driver.FindElement(logoutButton).Click();

                var flashText = driver.FindElement(flashMessage).Text;

                if (flashText.Contains("You logged out of the secure area!"))
                {
                    Console.WriteLine("Logout test passed.");
                }
                else
                {
                    Console.WriteLine("Logout test failed: Unexpected flash message.");
                }
            }
            catch (NoSuchElementException)
            {
                Console.WriteLine("Logout test failed: Logout button or flash message not found.");
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
