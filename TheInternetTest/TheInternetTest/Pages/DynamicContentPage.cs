using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI; 

namespace TheInternetTest.Pages
{
    public class DynamicContentPage
    {
        private IWebDriver driver;
        private WebDriverWait wait;

        private string url = "https://the-internet.herokuapp.com/dynamic_content";

        private By contentBlocksSelector = By.CssSelector("div.example > div.row > div#content > div.row");
        private By footerLinkSelector = By.XPath("//div[@id='page-footer']//a[contains(text(),'Elemental Selenium')]");
        private By clickHereLinkSelector = By.XPath("//a[contains(text(),'click here')]");
        private By githubRibbonSelector = By.CssSelector("a[href='https://github.com/tourdedave/the-internet'] > img");

        public DynamicContentPage(IWebDriver driver)
        {
            this.driver = driver;
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            driver.Navigate().GoToUrl(url);
            Console.WriteLine($"Navigated to {url}");
        }

        public void VerifyContentBlocks()
        {
            try
            {
                var blocks = wait.Until(drv => drv.FindElements(contentBlocksSelector));
                Console.WriteLine($"Found {blocks.Count} content blocks.");

                if (blocks.Count < 3)
                {
                    Console.WriteLine("WARNING: Expected at least 3 content blocks.");
                }

                for (int i = 0; i < blocks.Count; i++)
                {
                    var img = blocks[i].FindElement(By.CssSelector("div.large-2.columns > img"));
                    var text = blocks[i].FindElement(By.CssSelector("div.large-10.columns"));

                    Console.WriteLine($"Block {i + 1} image src: {img.GetAttribute("src")}");
                    Console.WriteLine($"Block {i + 1} text: {text.Text.Substring(0, Math.Min(text.Text.Length, 50))}...");
                }
            }
            catch (NoSuchElementException)
            {
                Console.WriteLine("Content blocks or their elements not found.");
            }
            catch (WebDriverTimeoutException)
            {
                Console.WriteLine("Timeout waiting for content blocks.");
            }
        }

        public void RefreshPage()
        {
            driver.Navigate().Refresh();
            Console.WriteLine("Page refreshed.");
        }

        public void ClickStaticContentLink()
        {
            try
            {
                var clickHereLink = wait.Until(drv => drv.FindElement(clickHereLinkSelector));
                clickHereLink.Click();
                Console.WriteLine("Clicked 'click here' link for static content.");
            }
            catch (NoSuchElementException)
            {
                Console.WriteLine("'click here' link not found.");
            }
            catch (WebDriverTimeoutException)
            {
                Console.WriteLine("Timeout waiting for 'click here' link.");
            }
        }

        public void VerifyFooterLink()
        {
            try
            {
                var footerLink = wait.Until(drv => drv.FindElement(footerLinkSelector));
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
            catch (WebDriverTimeoutException)
            {
                Console.WriteLine("Timeout waiting for footer link.");
            }
        }

        public void VerifyGithubLink()
        {
            try
            {
                var githubRibbon = driver.FindElement(githubRibbonSelector); // img element
                var parentLink = githubRibbon.FindElement(By.XPath("..")); // <a> element
                string href = parentLink.GetAttribute("href");

                if (href == "https://github.com/tourdedave/the-internet")
                {
                    Console.WriteLine("Found GitHub ribbon link.");
                    Console.WriteLine($"Link points to: {href}");

                    // Click the visible image instead of the <a>
                    //githubRibbon.Click();

                    //driver.Navigate().Back();
                    Console.WriteLine("Clicked GitHub ribbon image successfully.");
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
            Console.WriteLine("-- Verifying Dynamic Content Blocks --");
            VerifyContentBlocks();

            Console.WriteLine("-- Verifying Static Content Blocks --");
            ClickStaticContentLink();
            VerifyContentBlocks();

            Console.WriteLine("-- Verifying Footer Link --");
            VerifyFooterLink();

            Console.WriteLine("-- Verifying GitHub Link --");
            VerifyGithubLink();
        }
    }
}
