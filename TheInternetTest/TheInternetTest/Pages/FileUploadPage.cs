using OpenQA.Selenium; 

namespace TheInternetTest.Pages
{
    public class FileUploadPage
    {
        private IWebDriver driver;
        private string url = "https://the-internet.herokuapp.com/upload";
        private By fileInputSelector = By.Id("file-upload");
        private By uploadButtonSelector = By.Id("file-submit");
        private By uploadedFilesTextSelector = By.Id("uploaded-files");
        private By dragDropAreaSelector = By.Id("drag-drop-upload"); 
        private By githubRibbonSelector = By.CssSelector("a[href='https://github.com/tourdedave/the-internet'] img[alt='Fork me on GitHub']");
        private By footerLinkSelector = By.CssSelector("#page-footer a");

        public FileUploadPage(IWebDriver driver)
        {
            this.driver = driver;
            driver.Navigate().GoToUrl(url);
            Console.WriteLine($"Navigated to {url}");
        }

        public void ExecuteAllTests()
        {
            Console.WriteLine("\n-- Uploading via file input --");
            UploadViaFileInput();

            Console.WriteLine("\n-- Verifying footer link --");
            VerifyFooterLink();

            Console.WriteLine("\n-- Verifying Github link --");
            VerifyGithubLink();
        }

        public void UploadViaFileInput()
        {
            string filePath = Path.GetFullPath("D:\\Zubair\\study\\SQA BootCamp\\Bootcamp Project\\TheInternetTest\\TheInternetTest\\FileUploadPageDummyFile.txt"); // Adjust path as needed
            Console.WriteLine($"Uploading file: {filePath}");

            var fileInput = driver.FindElement(fileInputSelector);
            fileInput.SendKeys(filePath);

            driver.FindElement(uploadButtonSelector).Click();

            var uploadedFileName = driver.FindElement(uploadedFilesTextSelector).Text;

            if (uploadedFileName.Contains("FileUploadPageDummyFile.txt"))
                Console.WriteLine("File uploaded successfully via file input.");
            else
                Console.WriteLine("File upload via file input failed.");
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
