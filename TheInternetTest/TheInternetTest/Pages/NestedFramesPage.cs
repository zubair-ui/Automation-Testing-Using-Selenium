using OpenQA.Selenium;
using System;

namespace TheInternetTest.Pages
{
    public class NestedFramesPage
    {
        private IWebDriver driver;
        private string url = "https://the-internet.herokuapp.com/nested_frames";
         
        // Frame names
        private string frameTop = "frame-top";
        private string frameLeft = "frame-left";
        private string frameMiddle = "frame-middle";
        private string frameRight = "frame-right";
        private string frameBottom = "frame-bottom";

        // Selectors inside frames
        private By bodySelector = By.TagName("body");
        private By middleContentSelector = By.Id("content");

        public NestedFramesPage(IWebDriver driver)
        {
            this.driver = driver;
            driver.Navigate().GoToUrl(url);
            Console.WriteLine($"Navigated to {url}");
        }

        public void ExecuteAllTests()
        {
            Console.WriteLine("-- Verifying Frames --"); 
            SwitchToTopLeftFrameAndPrintText();
            SwitchToTopMiddleFrameAndPrintText();
            SwitchToTopRightFrameAndPrintText();
            SwitchToBottomFrameAndPrintText();

            driver.SwitchTo().DefaultContent();
            Console.WriteLine("Switched back to main content after all frame tests.");
             
        }

        public void SwitchToTopLeftFrameAndPrintText()
        {
            driver.SwitchTo().Frame(frameTop);
            driver.SwitchTo().Frame(frameLeft);

            string leftText = driver.FindElement(bodySelector).Text;
            Console.WriteLine($"Top Left frame text: {leftText}");

            driver.SwitchTo().DefaultContent();
        }

        public void SwitchToTopMiddleFrameAndPrintText()
        {
            driver.SwitchTo().Frame(frameTop);
            driver.SwitchTo().Frame(frameMiddle);

            string middleText = driver.FindElement(middleContentSelector).Text;
            Console.WriteLine($"Top Middle frame text: {middleText}");

            driver.SwitchTo().DefaultContent();
        }

        public void SwitchToTopRightFrameAndPrintText()
        {
            driver.SwitchTo().Frame(frameTop);
            driver.SwitchTo().Frame(frameRight);

            string rightText = driver.FindElement(bodySelector).Text;
            Console.WriteLine($"Top Right frame text: {rightText}");

            driver.SwitchTo().DefaultContent();
        }

        public void SwitchToBottomFrameAndPrintText()
        {
            driver.SwitchTo().Frame(frameBottom);

            string bottomText = driver.FindElement(bodySelector).Text;
            Console.WriteLine($"Bottom frame text: {bottomText}");

            driver.SwitchTo().DefaultContent();
        }
 
         
    }
}
