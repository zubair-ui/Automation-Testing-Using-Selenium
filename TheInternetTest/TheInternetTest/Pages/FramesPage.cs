using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;

namespace TheInternetTest.Pages
{
    public class FramesPage
    {
        private IWebDriver driver;
        private WebDriverWait wait;
        private string framesUrl = "https://the-internet.herokuapp.com/frames";
        private By footerLinkSelector = By.CssSelector("#page-footer a");
        private By githubRibbonSelector = By.CssSelector("a[href='https://github.com/tourdedave/the-internet'] img[alt='Fork me on GitHub']");


        public FramesPage(IWebDriver driver)
        {
            this.driver = driver;
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            driver.Navigate().GoToUrl(framesUrl);
            Console.WriteLine($"Navigated to {framesUrl}");
        }

        public void ExecuteAllTests()
        {
            Console.WriteLine("\n-- Running Frame tests --");
            RunFramesTests();

            Console.WriteLine("\n-- Verifying footer link --");
            VerifyFooterLink();

            Console.WriteLine("\n-- Verifying Github link --");
            VerifyGithubLink();
        }

        public void RunFramesTests()
        {
            

            

            try
            {
                var nestedFramesLink = wait.Until(d => d.FindElement(By.LinkText("Nested Frames")));
                nestedFramesLink.Click();

                Console.WriteLine("Navigated to Nested Frames page.");
                TestNestedFrames();

                driver.Navigate().Back();
                Console.WriteLine("Returned to Frames landing page.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error testing Nested Frames: " + ex.Message);
            }

            // Test iFrame
            try
            {
                var iFrameLink = wait.Until(d => d.FindElement(By.LinkText("iFrame")));
                iFrameLink.Click();

                Console.WriteLine("Navigated to iFrame page.");
                TestIFrame();

                driver.Navigate().Back();
                Console.WriteLine("Returned to Frames landing page.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error testing iFrame: " + ex.Message);
            }

            Console.WriteLine("Frames tests finished.");
        }

        private void TestNestedFrames()
        {
            // Switch to top frame first
            driver.SwitchTo().Frame("frame-top");

            // Switch to left frame inside top frame
            driver.SwitchTo().Frame("frame-left");
            string leftText = driver.FindElement(By.TagName("body")).Text.Trim();
            Console.WriteLine("Nested Frames - Left frame text: " + leftText);
            if (string.IsNullOrEmpty(leftText))
                throw new Exception("Left frame text is empty!");

            // Back to top frame
            driver.SwitchTo().ParentFrame();

            // Switch to middle frame inside top frame
            driver.SwitchTo().Frame("frame-middle");
            string middleText = driver.FindElement(By.Id("content")).Text.Trim();
            Console.WriteLine("Nested Frames - Middle frame text: " + middleText);
            if (string.IsNullOrEmpty(middleText))
                throw new Exception("Middle frame text is empty!");

            // Return to default content
            driver.SwitchTo().DefaultContent();
        }

        private void TestIFrame()
        {
            // Switch to iframe by ID
            driver.SwitchTo().Frame("mce_0_ifr");

            var editableArea = driver.FindElement(By.Id("tinymce"));
            string testText = "Hello from Selenium!";

            // Clear the editable area and input text using JS (better for rich text editors)
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("arguments[0].innerHTML = '';", editableArea);
            js.ExecuteScript($"arguments[0].innerText = '{testText}';", editableArea);

            string enteredText = editableArea.Text.Trim();
            Console.WriteLine("iFrame editable area text: " + enteredText);

            if (enteredText != testText)
                throw new Exception("iFrame text input did not match expected text.");

            // Switch back to default content after finishing test
            driver.SwitchTo().DefaultContent();

            Console.WriteLine("iFrame test passed.");
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
