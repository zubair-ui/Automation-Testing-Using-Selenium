using OpenQA.Selenium;
using System;

public class LargeAndDeepDomPage
{
    private readonly IWebDriver driver;

    // Locator for the large table
    private By largeTableLocator = By.Id("large-table");
    private string url = "https://the-internet.herokuapp.com/large";
    private By footerLinkSelector = By.CssSelector("#page-footer a");
    private By githubRibbonSelector = By.CssSelector("a[href='https://github.com/tourdedave/the-internet'] img[alt='Fork me on GitHub']");

    public LargeAndDeepDomPage(IWebDriver driver)
    {
        this.driver = driver;
        driver.Navigate().GoToUrl(url);
        Console.WriteLine($"Navigated to {url}");
    }

    public void ExecuteAllTests()
    {
        Console.WriteLine("-- Checking Large table --");
        CheckLargeTablePresence();
        PrintTableSizeInfo();

        Console.WriteLine("\n-- Verifying footer link --");
        VerifyFooterLink();

        Console.WriteLine("\n-- Verifying Github link --");
        VerifyGithubLink();

    }

    private void CheckLargeTablePresence()
    {
        bool isTablePresent = driver.FindElements(largeTableLocator).Count > 0;

        Console.WriteLine(isTablePresent ? "Large table is present." : "Large table is NOT present.");
    }

    private void PrintTableSizeInfo()
    {
        var table = driver.FindElement(largeTableLocator);
        var rows = table.FindElements(By.TagName("tr"));
        int rowCount = rows.Count;

        // Assuming first row has all columns
        int colCount = 0;
        if (rowCount > 0)
        {
            colCount = rows[0].FindElements(By.TagName("td")).Count;
            if (colCount == 0) // maybe th headers
                colCount = rows[0].FindElements(By.TagName("th")).Count;
        }

        Console.WriteLine($"Table size - Rows: {rowCount}, Columns: {colCount}");
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
