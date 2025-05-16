using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections.Generic;

namespace TheInternetTest.Pages
{
    public class HoversPage
    {
        private IWebDriver driver;
        private string url = "https://the-internet.herokuapp.com/hovers";
        private By figureSelector = By.CssSelector(".figure");
        private By figcaptionSelector = By.CssSelector(".figcaption");
        private By footerLinkSelector = By.CssSelector("#page-footer a");
        private By githubRibbonSelector = By.CssSelector("a[href='https://github.com/tourdedave/the-internet'] img[alt='Fork me on GitHub']");

        public HoversPage(IWebDriver driver)
        {
            this.driver = driver;
            driver.Navigate().GoToUrl(url);
            Console.WriteLine($"Navigated to {url}");
        }

        public void ExecuteAllTests()
        {
            Console.WriteLine("-- Starting Hover Tests --");
            HoverOverImagesAndVerifyCaptions();

            Console.WriteLine("\n-- Verifying footer link --");
            VerifyFooterLink();

            Console.WriteLine("\n-- Verifying Github link --");
            VerifyGithubLink();
        }

        public void HoverOverImagesAndVerifyCaptions()
        {
            var figures = driver.FindElements(figureSelector);
            Actions actions = new Actions(driver);

            for (int i = 0; i < figures.Count; i++)
            {
                Console.WriteLine($"Hovering over figure #{i + 1}");

                actions.MoveToElement(figures[i]).Perform();

                var caption = figures[i].FindElement(figcaptionSelector);
                if (caption.Displayed)
                {
                    var name = caption.FindElement(By.TagName("h5")).Text;
                    var profileLink = caption.FindElement(By.TagName("a")).GetAttribute("href");

                    Console.WriteLine($"Caption is visible: {name}, Link: {profileLink}");

                    if (!profileLink.Contains($"/users/{i + 1}"))
                    {
                        Console.WriteLine($"Warning: Expected profile link to contain /users/{i + 1}");
                    }
                }
                else
                {
                    Console.WriteLine($"Caption not displayed for figure #{i + 1}");
                }
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
