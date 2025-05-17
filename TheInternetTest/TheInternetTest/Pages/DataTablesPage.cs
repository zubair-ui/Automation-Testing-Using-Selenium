using OpenQA.Selenium; 

namespace TheInternetTest.Pages
{
    public class DataTablesPage
    {
        private IWebDriver driver;
        private string url = "https://the-internet.herokuapp.com/tables";
        private By footerLinkSelector = By.CssSelector("#page-footer a"); 
        private By githubRibbonSelector = By.CssSelector("a[href='https://github.com/tourdedave/the-internet'] img[alt='Fork me on GitHub']");

        // Constructor
        public DataTablesPage(IWebDriver driver)
        {
            this.driver = driver; 
            driver.Navigate().GoToUrl(url);
        }

        // Locators and Elements
        public IWebElement Table1 => driver.FindElement(By.Id("table1"));
        public IWebElement Table2 => driver.FindElement(By.Id("table2"));

        public IList<IWebElement> Table1Rows => driver.FindElements(By.CssSelector("#table1 tbody tr"));
        public IList<IWebElement> Table2Rows => driver.FindElements(By.CssSelector("#table2 tbody tr"));

        // Get text from a specific cell in a given table
        public string GetCellText(IWebElement table, int rowIndex, int colIndex)
        {
            var rows = table.FindElements(By.CssSelector("tbody tr"));
            var cells = rows[rowIndex].FindElements(By.TagName("td"));
            return cells[colIndex].Text;
        }

        // Click 'Edit' link in a specific row of a given table
        public void ClickEditLink(IWebElement table, int rowIndex)
        {
            var rows = table.FindElements(By.CssSelector("tbody tr"));
            rows[rowIndex].FindElement(By.LinkText("edit")).Click();
            driver.Navigate().Back();
        }

        // Click 'Delete' link in a specific row of a given table
        public void ClickDeleteLink(IWebElement table, int rowIndex)
        {
            var rows = table.FindElements(By.CssSelector("tbody tr"));
            rows[rowIndex].FindElement(By.LinkText("delete")).Click();
            driver.Navigate().Back();
        }

        // Get total number of rows in a table
        public int GetRowCount(IWebElement table)
        {
            return table.FindElements(By.CssSelector("tbody tr")).Count;
        }

        // Execute all tests sequentially
        public void ExecuteAllTests()
        { 

            Console.WriteLine("-- Verifying Table 1 Row Count --");
            Console.WriteLine($"Table 1 has {GetRowCount(Table1)} rows.");

            Console.WriteLine("\n-- Verifying Table 2 Row Count --");
            Console.WriteLine($"Table 2 has {GetRowCount(Table2)} rows.");

            Console.WriteLine("\n-- Reading first cell value from Table 1 --");
            string cellValue1 = GetCellText(Table1, 0, 0);
            Console.WriteLine($"First cell in Table 1: {cellValue1}");

            Console.WriteLine("\n-- Reading first cell value from Table 2 --");
            string cellValue2 = GetCellText(Table2, 0, 0);
            Console.WriteLine($"First cell in Table 2: {cellValue2}");

            Console.WriteLine("\n-- Clicking Edit link in first row of Table 1 --");
            ClickEditLink(Table1, 0);

            Console.WriteLine("\n-- Clicking Delete link in second row of Table 2 --");
            ClickDeleteLink(Table2, 1);

            Console.WriteLine("\n-- Verifying footer link --");
            VerifyFooterLink();

            Console.WriteLine("\n-- Verifying Github link --");
            VerifyGithubLink();

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
