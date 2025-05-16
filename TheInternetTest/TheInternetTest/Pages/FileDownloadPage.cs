using OpenQA.Selenium; 

namespace TheInternetTest.Pages
{
    public class FileDownloadPage
    {
        private IWebDriver driver;
        private string url = "https://the-internet.herokuapp.com/download";
        private By fileLinksSelector = By.CssSelector("#content a");
        private By footerLinkSelector = By.CssSelector("#page-footer a");
        private By githubRibbonSelector = By.CssSelector("a[href='https://github.com/tourdedave/the-internet'] img[alt='Fork me on GitHub']");

        public FileDownloadPage(IWebDriver driver)
        {
            this.driver = driver;
            driver.Navigate().GoToUrl(url);
            Console.WriteLine($"Navigated to {url}");
        }

        public void ExecuteAllTests()
        {
            Console.WriteLine("\n-- Verifying and Downloading all file links --");
            VerifyAndDownloadAllFiles();

            Console.WriteLine("\n-- Verifying footer link --");
            VerifyFooterLink();

            Console.WriteLine("\n-- Verifying Github link --");
            VerifyGithubLink();
        }

        public void VerifyAndDownloadAllFiles()
        {
            var fileLinks = driver.FindElements(fileLinksSelector);
            Console.WriteLine($"Total file download links found: {fileLinks.Count}");

            if (fileLinks.Count == 0)
            {
                Console.WriteLine("No download links found on the page.");
                return;
            }

            for (int i = 0; i < fileLinks.Count; i++)
            {
                // Re-locate links each time to avoid stale reference
                var link = driver.FindElements(fileLinksSelector)[i];
                string fileName = link.Text;
                string fileUrl = link.GetAttribute("href");

                if (link.Displayed && link.Enabled)
                {
                    Console.WriteLine($"Downloading file: {fileName} ({fileUrl})");
                    link.Click();
                }
                else
                {
                    Console.WriteLine($"File link not clickable: {fileName}");
                }
            }

            Console.WriteLine("All file download links clicked successfully.");
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
