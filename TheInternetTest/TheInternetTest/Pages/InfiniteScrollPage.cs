using OpenQA.Selenium; 
using OpenQA.Selenium.Support.UI; 

namespace TheInternetTest.Pages
{
    public class InfiniteScrollPage
    {
        private IWebDriver driver;
        private string url = "https://the-internet.herokuapp.com/infinite_scroll";
        private By paragraphSelector = By.CssSelector(".jscroll-added");
        private int scrollsToPerform = 3;
        private By footerLinkSelector = By.CssSelector("#page-footer a");
        private By githubRibbonSelector = By.CssSelector("a[href='https://github.com/tourdedave/the-internet'] img[alt='Fork me on GitHub']");

        public InfiniteScrollPage(IWebDriver driver)
        {
            this.driver = driver;
            driver.Navigate().GoToUrl(url);
            Console.WriteLine($"Navigated to {url}");
        }

        public void ExecuteAllTests()
        {
            Console.WriteLine("-- Starting Infinite Scroll Tests --");
            ScrollAndVerifyContentLoading();

            Console.WriteLine("\n-- Verifying footer link --");
            VerifyFooterLink();

            Console.WriteLine("\n-- Verifying Github link --");
            VerifyGithubLink();
        }

        public void ScrollAndVerifyContentLoading()
        {
            int initialParagraphs = driver.FindElements(paragraphSelector).Count;
            Console.WriteLine($"Initial paragraphs: {initialParagraphs}");

            for (int i = 0; i < scrollsToPerform; i++)
            {
                ScrollToBottom();
                WaitForNewParagraphs(initialParagraphs + i + 1);
            }

            int finalParagraphs = driver.FindElements(paragraphSelector).Count;
            Console.WriteLine($"Final paragraphs after scrolling: {finalParagraphs}");

            if (finalParagraphs > initialParagraphs)
            {
                Console.WriteLine("Infinite scroll content loaded successfully.");
            }
            else
            {
                Console.WriteLine("Infinite scroll failed to load additional content.");
            }
        }

        private void ScrollToBottom()
        {
            var js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("window.scrollTo(0, document.body.scrollHeight);");
            Console.WriteLine("Scrolled to bottom.");
        }

        private void WaitForNewParagraphs(int expectedCount)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(2));
            try
            {
                wait.Until(d => d.FindElements(paragraphSelector).Count >= expectedCount);
                Console.WriteLine($"Loaded at least {expectedCount} paragraphs.");
            }
            catch (WebDriverTimeoutException)
            {
                ScrollToBottom();
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
