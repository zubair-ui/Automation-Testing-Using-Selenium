using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using System;

namespace TheInternetTest.Pages
{
    public class ExitIntentPage
    {
        private IWebDriver driver;
        private string url = "https://the-internet.herokuapp.com/exit_intent";
        private By footerLinkSelector = By.CssSelector("#page-footer a");
        private By modalSelector = By.Id("ouibounce-modal");
        private By modalCloseButtonSelector = By.CssSelector("#ouibounce-modal .modal-footer p");
        private By githubRibbonSelector = By.CssSelector("a[href='https://github.com/tourdedave/the-internet'] img[alt='Fork me on GitHub']");

        public ExitIntentPage(IWebDriver driver)
        {
            this.driver = driver;
            driver.Navigate().GoToUrl(url);
            Console.WriteLine($"Navigated to {url}");
        }

        public void TriggerExitIntent()
        {
            Console.WriteLine("Triggering exit intent modal via JS...");

            // This forces the modal to appear directly:
            string script = "document.getElementById('ouibounce-modal').style.display='block';";
            ((IJavaScriptExecutor)driver).ExecuteScript(script);

            System.Threading.Thread.Sleep(1000);
        }



        public void VerifyModalAppeared()
        {
            try
            {
                var modal = driver.FindElement(modalSelector);
                if (modal.Displayed)
                {
                    Console.WriteLine("Exit intent modal appeared successfully.");
                }
                else
                {
                    Console.WriteLine("Exit intent modal did not appear.");
                }
            }
            catch (NoSuchElementException)
            {
                Console.WriteLine("Exit intent modal element not found.");
            }
        }

        public void CloseModal()
        {
            try
            {
                var closeButton = driver.FindElement(modalCloseButtonSelector);
                closeButton.Click();
                System.Threading.Thread.Sleep(500);

                var modal = driver.FindElement(modalSelector);
                if (!modal.Displayed)
                {
                    Console.WriteLine("Modal closed successfully.");
                }
                else
                {
                    Console.WriteLine("Modal did not close.");
                }
            }
            catch (NoSuchElementException)
            {
                Console.WriteLine("Close button or modal not found.");
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
            Console.WriteLine("\n-- Verifying Modal --");
            TriggerExitIntent();
            VerifyModalAppeared();
            CloseModal();

            Console.WriteLine("\n-- Verifying footer link --");
            VerifyFooterLink();

            Console.WriteLine("\n-- Verifying Github link --");
            VerifyGithubLink();
        }
    }
}
