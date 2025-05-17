using OpenQA.Selenium; 

namespace TheInternetTest.Pages
{
    public class JavaScriptAlertsPage
    {
        private readonly IWebDriver driver;
        private readonly string pageUrl = "https://the-internet.herokuapp.com/javascript_alerts";
        private By footerLinkSelector = By.CssSelector("#page-footer a");
        private By githubRibbonSelector = By.CssSelector("a[href='https://github.com/tourdedave/the-internet'] img[alt='Fork me on GitHub']");

        // Locators
        private By jsAlertButton = By.XPath("//button[text()='Click for JS Alert']");
        private By jsConfirmButton = By.XPath("//button[text()='Click for JS Confirm']");
        private By jsPromptButton = By.XPath("//button[text()='Click for JS Prompt']");
        private By resultText = By.Id("result");

        public JavaScriptAlertsPage(IWebDriver driver)
        {
            this.driver = driver;
            driver.Navigate().GoToUrl(pageUrl);
            Console.WriteLine("Navigated to JavaScript Alerts page.");
        }

        public void ExecuteAllTests()
        {
            Console.WriteLine("\n-- Testing Javascript Alert --");
            TestJsAlert();

            Console.WriteLine("\n-- Testing Javascript Confirm --");
            TestJsConfirm();

            Console.WriteLine("\n-- Testing Javascript Prompt --");
            TestJsPrompt();

            Console.WriteLine("\n-- Verifying footer link --");
            VerifyFooterLink();

            Console.WriteLine("\n-- Verifying Github link --");
            VerifyGithubLink();
        }

        private void TestJsAlert()
        {
            Console.WriteLine("Running JS Alert test...");
            driver.FindElement(jsAlertButton).Click();

            IAlert alert = driver.SwitchTo().Alert();
            Console.WriteLine("Alert text: " + alert.Text);
            alert.Accept();

            string result = driver.FindElement(resultText).Text;
            Console.WriteLine("Result after JS Alert: " + result);
        }

        private void TestJsConfirm()
        {
            Console.WriteLine("Running JS Confirm test...");
            driver.FindElement(jsConfirmButton).Click();

            IAlert confirmAlert = driver.SwitchTo().Alert();
            Console.WriteLine("Confirm text: " + confirmAlert.Text);
            confirmAlert.Dismiss(); // simulate 'Cancel'

            string result = driver.FindElement(resultText).Text;
            Console.WriteLine("Result after JS Confirm: " + result);
        }

        private void TestJsPrompt()
        {
            Console.WriteLine("Running JS Prompt test...");

            // With data
            driver.FindElement(jsPromptButton).Click();
            IAlert promptAlert = driver.SwitchTo().Alert();
            Console.WriteLine("Prompt text: " + promptAlert.Text);

            string inputText = "Selenium Test";
            promptAlert.SendKeys(inputText);
            promptAlert.Accept();

            string result = driver.FindElement(resultText).Text;
            Console.WriteLine("Result after entering text and Accept: " + result);

            // Without data
            driver.FindElement(jsPromptButton).Click();
            promptAlert = driver.SwitchTo().Alert();
            Console.WriteLine("Prompt text: " + promptAlert.Text);

            promptAlert.Accept();
            result = driver.FindElement(resultText).Text;
            Console.WriteLine("Result after no text and Accept: " + result);

            // Press Cancel
            driver.FindElement(jsPromptButton).Click();
            promptAlert = driver.SwitchTo().Alert();
            Console.WriteLine("Prompt text: " + promptAlert.Text);

            promptAlert.Dismiss();
            result = driver.FindElement(resultText).Text;
            Console.WriteLine("Result after pressing Cancel: " + result);
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
