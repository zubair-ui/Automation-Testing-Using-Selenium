using OpenQA.Selenium; 

namespace TheInternetTest.Pages
{
    public class ForgotPasswordPage
    {
        private readonly IWebDriver driver;
        private readonly string url = "https://the-internet.herokuapp.com/forgot_password";
        private By footerLinkSelector = By.CssSelector("#page-footer a");
        private By githubRibbonSelector = By.CssSelector("a[href='https://github.com/tourdedave/the-internet'] img[alt='Fork me on GitHub']");

        // Locators
        private readonly By emailInput = By.Id("email");
        private readonly By submitButton = By.Id("form_submit");

        public ForgotPasswordPage(IWebDriver driver)
        {
            this.driver = driver;
            driver.Navigate().GoToUrl(url);
            Console.WriteLine($"Navigated to {url}");
        }
          
        public void EnterEmail(string email)
        {
            driver.FindElement(emailInput).Clear();
            driver.FindElement(emailInput).SendKeys(email);
        }
 
        public void ClickRetrievePassword()
        {
            Console.WriteLine("Form Submitted");
            driver.FindElement(submitButton).Click();
            driver.Navigate().Back();
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

            Console.WriteLine("\n-- Entering email address --");
            Console.WriteLine("test@example.com");
            EnterEmail("test@example.com");

            Console.WriteLine("\n-- Submitting form --");
            ClickRetrievePassword();

            Console.WriteLine("\n-- Verifying footer link --");
            VerifyFooterLink();

            Console.WriteLine("\n-- Verifying Github link --");
            VerifyGithubLink();
        }
    }
}
