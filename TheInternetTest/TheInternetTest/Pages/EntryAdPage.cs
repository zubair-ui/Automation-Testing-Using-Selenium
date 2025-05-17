using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace TheInternetTest.Pages
{
    public class EntryAdPage
    {
        private readonly IWebDriver driver;
        private readonly WebDriverWait wait;

        private readonly string url = "https://the-internet.herokuapp.com/entry_ad";

        // Selectors
        private readonly By modalSelector = By.Id("modal");
        private readonly By modalTitleSelector = By.CssSelector("#modal .modal-title h3");
        private readonly By modalBodySelector = By.CssSelector("#modal .modal-body p");
        private readonly By closeButtonSelector = By.CssSelector("#modal .modal-footer p");
        private readonly By restartAdLinkSelector = By.Id("restart-ad");

        private readonly By footerLinkSelector = By.CssSelector("#page-footer a[target='_blank']");
        private readonly By githubRibbonSelector = By.CssSelector("a[href='https://github.com/tourdedave/the-internet'] img");

        public EntryAdPage(IWebDriver driver)
        {
            this.driver = driver;
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            driver.Navigate().GoToUrl(url);
            Console.WriteLine($"Navigated to {url}");
        }

        public void VerifyAndCloseModal()
        {
            try
            {
                wait.Until(drv => drv.FindElement(modalSelector)); // ensure it's in DOM

                var modal = driver.FindElement(modalSelector);

                // Now wait for it to be displayed (as it may still be hidden)
                wait.Until(drv => modal.Displayed);

                var title = driver.FindElement(modalTitleSelector).Text;
                var bodyText = driver.FindElement(modalBodySelector).Text;

                Console.WriteLine($"Modal Title: {title}");
                Console.WriteLine($"Modal Body: {bodyText}");

                driver.FindElement(closeButtonSelector).Click();
                Console.WriteLine("Modal closed successfully.");
            }
            catch (WebDriverTimeoutException)
            {
                Console.WriteLine("Modal not displayed within timeout.");
            }
            catch (NoSuchElementException)
            {
                Console.WriteLine("Modal not found.");
            }
        }


        public void RestartAdAndVerifyModalAppears()
        {
            try
            {
                driver.FindElement(restartAdLinkSelector).Click();
                Console.WriteLine("Clicked 'restart ad' link.");

                wait.Until(drv => drv.FindElement(modalSelector));
                var modal = driver.FindElement(modalSelector);

                wait.Until(drv => modal.Displayed);

                Console.WriteLine("Modal appeared after restart.");
                Thread.Sleep(100);
                driver.FindElement(closeButtonSelector).Click();
                Console.WriteLine("Modal closed after restart.");
            }
            catch (WebDriverTimeoutException)
            {
                Console.WriteLine("Modal did not appear after restart.");
            }
            catch (NoSuchElementException)
            {
                Console.WriteLine("Restart ad link or modal not found.");
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

        // Method to safely close modal if it appears
        public void CloseModalIfPresent()
        {
            try
            {
                var modal = driver.FindElement(modalSelector);
                if (modal.Displayed)
                {
                    driver.FindElement(closeButtonSelector).Click();
                    Console.WriteLine("Modal closed.");
                }
            }
            catch (NoSuchElementException)
            {
                // Modal not present, no action needed
            }
        }


        public void ExecuteAll()
        {
            Console.WriteLine("-- Verifying and Closing Modal --");
            VerifyAndCloseModal();

            Console.WriteLine("\n-- Restarting Ad and Verifying Modal Again --");
            RestartAdAndVerifyModalAppears();
            CloseModalIfPresent();

            Console.WriteLine("\n-- Verifying Footer link --");
            VerifyFooterLink();

            Console.WriteLine("\n-- Verifying Github Ribbon --");
            VerifyGithubLink();
        }
    }
}
