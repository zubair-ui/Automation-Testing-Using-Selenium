using OpenQA.Selenium;
using System;
using System.Collections.Generic;

namespace TheInternetTest.Pages
{
    public class BrokenImagesPage
    {
        private IWebDriver driver;
        private string pageUrl = "https://the-internet.herokuapp.com/broken_images";
        private By githubRibbonSelector = By.CssSelector("a[href='https://github.com/tourdedave/the-internet'] img[alt='Fork me on GitHub']");

        public BrokenImagesPage(IWebDriver webDriver)
        {
            driver = webDriver;
            driver.Navigate().GoToUrl(pageUrl);
            Console.WriteLine($"Navigated to {pageUrl}");
        }

        public void ExecuteAllTests()
        { 

            var images = GetAllImages();
            int brokenCount = CheckBrokenImages(images);
            ReportImageSummary(images.Count, brokenCount);
            ValidateFooterLink();
            VerifyGithubLink();
        }


        private IReadOnlyCollection<IWebElement> GetAllImages()
        {
            var images = driver.FindElements(By.CssSelector(".example img"));
            Console.WriteLine($"Total images found: {images.Count}");
            return images;
        }


        private int CheckBrokenImages(IReadOnlyCollection<IWebElement> images)
        {
            int brokenCount = 0;

            foreach (var image in images)
            {
                var result = (long)((IJavaScriptExecutor)driver)
                    .ExecuteScript("return arguments[0].naturalWidth;", image);

                if (result == 0)
                {
                    Console.WriteLine($"Broken image found: {image.GetAttribute("src")}");
                    brokenCount++;
                }
            }

            return brokenCount;
        }

        private void ReportImageSummary(int totalImages, int brokenImages)
        {
            Console.WriteLine($"Total broken images: {brokenImages}");
            Console.WriteLine($"Total working images: {totalImages - brokenImages}");
        }

        private void ValidateFooterLink()
        {
            var footerLink = driver.FindElement(By.CssSelector("#page-footer a"));
            string linkText = footerLink.Text;
            string linkHref = footerLink.GetAttribute("href");

            if (linkText.Contains("Elemental Selenium"))
            {
                Console.WriteLine("Found footer link: " + linkText);
                Console.WriteLine("Link points to: " + linkHref);
                footerLink.Click();
                Console.WriteLine("Clicked footer link successfully.");
            }
            else
            {
                Console.WriteLine("Footer link text mismatch.");
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
    }
}
