using OpenQA.Selenium;
using System;
using System.Collections.Generic;

namespace TheInternetTest.Pages
{
    public class BrokenImagesPage
    {
        private IWebDriver driver;
        private string pageUrl = "https://the-internet.herokuapp.com/broken_images";
        private By footerLinkSelector = By.CssSelector("#page-footer a");
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

            Console.WriteLine("\n-- Reporting Image Summary --");
            ReportImageSummary(images.Count, brokenCount);

            Console.WriteLine("\n-- Verifying footer link --");
            VerifyFooterLink();

            Console.WriteLine("\n-- Verifying GitHub link --");
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
                var githubRibbon = driver.FindElement(githubRibbonSelector); // img element
                var parentLink = githubRibbon.FindElement(By.XPath("..")); // <a> element
                string href = parentLink.GetAttribute("href");

                if (href == "https://github.com/tourdedave/the-internet")
                {
                    Console.WriteLine("Found GitHub ribbon link.");
                    Console.WriteLine($"Link points to: {href}");

                    // Click the visible image instead of the <a>
                    githubRibbon.Click();

                    driver.Navigate().Back();
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
    }
}
