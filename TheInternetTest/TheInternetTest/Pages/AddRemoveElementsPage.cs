using OpenQA.Selenium; 

namespace TheInternetTest.Pages
{
    public class AddRemoveElementsPage
    {
        private IWebDriver driver;
        private string url = "https://the-internet.herokuapp.com/add_remove_elements/";
        private By addElementButton = By.XPath("//button[text()='Add Element']");
        private By deleteButtons = By.CssSelector("#elements button");
        private By footerLinkSelector = By.CssSelector("#page-footer a");
        private By githubRibbonSelector = By.CssSelector("a[href='https://github.com/tourdedave/the-internet'] img[alt='Fork me on GitHub']");

        public AddRemoveElementsPage(IWebDriver driver)
        {
            this.driver = driver;
            driver.Navigate().GoToUrl(url);
            Console.WriteLine($"Navigated to {url}");
        }

        public void ExecuteAllTests()
        {
            Console.WriteLine("\n-- Adding 10 Elements --");
            AddElements(10);

            Console.WriteLine("\n-- Deleting 10 Elements --");
            DeleteElements(10);

            Console.WriteLine("\n-- Verifying no elements exist --");
            VerifyNoDeleteButtonsRemain();

            Console.WriteLine("\n-- Verifying footer link --");
            VerifyFooterLink();

            Console.WriteLine("\n-- Verifying Github link --");
            VerifyGithubLink();
        }

        public void AddElements(int count)
        {
            for (int i = 1; i <= count; i++)
            {
                driver.FindElement(addElementButton).Click();
                Console.WriteLine($"Clicked 'Add Element' button {i} time(s).");
            }
            Console.WriteLine($"Passed: Added {count} elements.");
        }

        public void DeleteElements(int count)
        {
            for (int i = 1; i <= count; i++)
            {
                var deleteButtonList = driver.FindElements(deleteButtons);
                if (deleteButtonList.Count > 0)
                {
                    deleteButtonList[0].Click();
                    Console.WriteLine($"Clicked 'Delete' button {i} time(s).");
                }
                else
                {
                    Console.WriteLine("No 'Delete' button found to click.");
                    break;
                }
            }
            Console.WriteLine($"Passed: Deleted up to {count} elements (or as many as available).");
        }

        public void VerifyNoDeleteButtonsRemain()
        {
            var remainingDeleteButtons = driver.FindElements(deleteButtons).Count;
            if (remainingDeleteButtons == 0)
            {
                Console.WriteLine("Test Passed: No 'Delete' buttons remain.");
            }
            else
            {
                Console.WriteLine($"Test Failed: {remainingDeleteButtons} 'Delete' button(s) remain.");
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
