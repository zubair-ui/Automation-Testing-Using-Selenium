using OpenQA.Selenium;
using System;
using System.Collections.Generic;

namespace TheInternetTest.Pages
{
    public class CheckboxesPage
    {
        private IWebDriver driver;
        private string url = "https://the-internet.herokuapp.com/checkboxes";
        private By checkboxSelector = By.CssSelector("#checkboxes input[type='checkbox']");
        private By footerLinkSelector = By.CssSelector("#page-footer a");
        private By githubRibbonSelector = By.CssSelector("a[href='https://github.com/tourdedave/the-internet'] img[alt='Fork me on GitHub']");

        public CheckboxesPage(IWebDriver driver)
        {
            this.driver = driver;
            driver.Navigate().GoToUrl(url);
            Console.WriteLine($"Navigated to {url}");
        }

        public IList<IWebElement> GetCheckboxes()
        {
            return driver.FindElements(checkboxSelector);
        }

        public void PrintCheckboxStates()
        {
            var checkboxes = GetCheckboxes();
            Console.WriteLine($"Total checkboxes found: {checkboxes.Count}");
            for (int i = 0; i < checkboxes.Count; i++)
            {
                Console.WriteLine($"Checkbox {i + 1} is {(checkboxes[i].Selected ? "checked" : "unchecked")}");
            }
        }

        public void ToggleCheckbox(int index)
        {
            var checkboxes = GetCheckboxes();
            if (index < 0 || index >= checkboxes.Count)
            {
                Console.WriteLine($"Invalid checkbox index: {index}");
                return;
            }

            checkboxes[index].Click();
            Console.WriteLine($"Toggled checkbox at index {index + 1}");
        }

        public void VerifyAndToggleAllCheckboxes()
        {
            var checkboxes = GetCheckboxes();
            Console.WriteLine("Initial checkbox states:");
            PrintCheckboxStates();

            for (int i = 0; i < checkboxes.Count; i++)
            {
                ToggleCheckbox(i);
            }

            Console.WriteLine("Checkbox states after toggling:");
            PrintCheckboxStates();
        }

        public void VerifyAndToggleAllCheckboxesIfUnchecked()
        {
            var checkboxes = GetCheckboxes();
            Console.WriteLine("Initial checkbox states:");
            PrintCheckboxStates();

            for (int i = 0; i < checkboxes.Count; i++)
            {
                if (!(checkboxes[i].Selected))
                {
                    ToggleCheckbox(i);
                }
            }

            Console.WriteLine("Checkbox states after toggling unchecked checkboxes:");
            PrintCheckboxStates();
        }

        public void VerifyAndUntoggleAllCheckboxesIfChecked()
        {
            var checkboxes = GetCheckboxes();
            Console.WriteLine("Initial checkbox states:");
            PrintCheckboxStates();

            for (int i = 0; i < checkboxes.Count; i++)
            {
                if (checkboxes[i].Selected)  // Fix here: untoggle only if checked
                {
                    ToggleCheckbox(i);
                }
            }

            Console.WriteLine("Checkbox states after untoggling checked checkboxes:");
            PrintCheckboxStates();
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
                var githubRibbon = driver.FindElement(githubRibbonSelector);
                var parentLink = githubRibbon.FindElement(By.XPath("..")); // get the anchor element
                string href = parentLink.GetAttribute("href");

                if (href == "https://github.com/tourdedave/the-internet")
                {
                    Console.WriteLine("GitHub ribbon link found and verified.");
                }
                else
                {
                    Console.WriteLine($"GitHub ribbon link href mismatch: {href}");
                }
            }
            catch (NoSuchElementException)
            {
                Console.WriteLine("GitHub ribbon link not found.");
            }
        }

        // New method to execute all tests in sequence
        public void ExecuteAllTests()
        { 

            Console.WriteLine("\n-- Printing initial checkbox states --");
            PrintCheckboxStates();

            Console.WriteLine("\n-- Verify and toggle all checkboxes --");
            VerifyAndToggleAllCheckboxes();

            Console.WriteLine("\n-- Verify and toggle all unchecked checkboxes --");
            VerifyAndToggleAllCheckboxesIfUnchecked();

            Console.WriteLine("\n-- Verify and untoggle all checked checkboxes --");
            VerifyAndUntoggleAllCheckboxesIfChecked();

            Console.WriteLine("\n-- Verifying footer link --");
            VerifyFooterLink();

            Console.WriteLine("\n-- Verifying Github link --");
            VerifyGithubLink();
        }
    }
}
