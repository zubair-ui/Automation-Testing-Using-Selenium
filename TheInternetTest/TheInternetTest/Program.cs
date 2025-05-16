using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using TheInternetTest.Pages;

namespace TheInternetTest
{
    class Program
    {
        static void Main(string[] args)
        {
             
            IWebDriver driver = new ChromeDriver();

            try
            {
                //RunHomePageTests(driver);
                //RunAbTestPageTests(driver);
                //RunAddRemoveElementsPageTests(driver);
                //RunBasicAuthPageTests(driver);
                //RunBrokenImagesPageTests(driver);
                //RunChallengingDomPageTests(driver);
                //RunCheckboxesPageTests(driver);
                //RunContextMenuPageTests(driver);
                //RunDigestAuthPageTests(driver);
                //RunDisappearingElementsPageTests(driver);
                //RunDragAndDropPageTests(driver);
                //RunDropdownPageTests(driver);
                //RunDynamicContentPageTests(driver);
                //RunDynamicControlsTests(driver);


            }
            catch (Exception ex)
            {
                Console.WriteLine("Test Exception: " + ex.Message);
            }
            finally
            { 
                driver.Quit();
                Console.WriteLine("Browser closed.");
            }

            Console.WriteLine("All tests done.");
        }

        // Function to execute homepage tests
        static void RunHomePageTests(IWebDriver driver)
        {
            Console.WriteLine("===== Starting Home Page Tests =====");

            HomePage homePage = new HomePage(driver);
            homePage.ExecuteAllTests();

            Console.WriteLine("===== Home Page Tests Completed =====\n");
        }

        // Function to execute A/B Test Page tests
        static void RunAbTestPageTests(IWebDriver driver)
        {
            Console.WriteLine("===== Starting A/B Test Page Tests =====");

            AbTestPage abTestPage = new AbTestPage(driver);
            abTestPage.ExecuteAllTests();

            Console.WriteLine("===== A/B Test Page Tests Completed =====\n");
        }

        // Function to execute Add/Remove Elements Page tests
        static void RunAddRemoveElementsPageTests(IWebDriver driver)
        {
            Console.WriteLine("===== Starting Add/Remove Elements Page Tests =====");

            AddRemoveElementsPage addRemovePage = new AddRemoveElementsPage(driver);
            addRemovePage.ExecuteAllTests();

            Console.WriteLine("===== Add/Remove Elements Page Tests Completed =====\n");
        }

        // Function to execute Basic Auth Page tests
        static void RunBasicAuthPageTests(IWebDriver driver)
        {
            Console.WriteLine("===== Starting Basic Auth Page Tests =====");

            BasicAuthPage basicAuthPage = new BasicAuthPage(driver);
            basicAuthPage.ExecuteAllTests();

            Console.WriteLine("===== Basic Auth Page Tests Completed =====\n");
        }

        // Function to execute Broken Images Page tests
        static void RunBrokenImagesPageTests(IWebDriver driver)
        {
            Console.WriteLine("===== Starting Broken Images Page Tests =====");

            BrokenImagesPage brokenImagesPage = new BrokenImagesPage(driver);
            brokenImagesPage.ExecuteAllTests();

            Console.WriteLine("===== Broken Images Page Tests Completed =====\n");
        }

        // Function to execute Challenging DOM Page tests
        static void RunChallengingDomPageTests(IWebDriver driver)
        {
            Console.WriteLine("===== Starting Challenging DOM Page Tests =====");

            ChallengingDomPage challengingDomPage = new ChallengingDomPage(driver);
            challengingDomPage.ExecuteAllTests();

            Console.WriteLine("===== Challenging DOM Page Tests Completed =====\n");
        }
        // Function to execute Challenging Checkboxes Page tests
        static void RunCheckboxesPageTests(IWebDriver driver)
        {
            Console.WriteLine("===== Starting Checkboxes Page Tests =====");

            CheckboxesPage checkboxesPage = new CheckboxesPage(driver);
            checkboxesPage.ExecuteAllTests(); 

            Console.WriteLine("===== Checkboxes Page Tests Completed =====\n");
        }

        // Function to execute Context Menu Page tests
        static void RunContextMenuPageTests(IWebDriver driver)
        {
            Console.WriteLine("===== Starting Context Menu Page Tests =====");

            ContextMenuPage contextMenuPage = new ContextMenuPage(driver);
            contextMenuPage.ExecuteAllTests();

            Console.WriteLine("===== Context Menu Page Tests Completed =====\n");
        }

        // Function to execute Digest Auth Page tests
        static void RunDigestAuthPageTests(IWebDriver driver)
        {
            Console.WriteLine("===== Starting Digest Auth Page Tests =====");

            DigestAuthPage digestAuthPage = new DigestAuthPage(driver);
            digestAuthPage.ExecuteAllTests();

            Console.WriteLine("===== Digest Auth Page Tests Completed =====\n");
        }

        // Function to execute Disappearing Elements Page tests
        static void RunDisappearingElementsPageTests(IWebDriver driver)
        {
            Console.WriteLine("===== Starting Disappearing Elements Page Tests =====");

            DisappearingElementsPage disappearingPage = new DisappearingElementsPage(driver);
            disappearingPage.ExecuteAllTests();

            Console.WriteLine("===== Disappearing Elements Page Tests Completed =====\n");
        }

        // Function to execute drag and drop page tests
        static void RunDragAndDropPageTests(IWebDriver driver)
        {
            Console.WriteLine("===== Starting Drag And Drop Page Tests =====");

            DragAndDropPage dragAndDropPage = new DragAndDropPage(driver);
            dragAndDropPage.ExecuteAllTests();

            Console.WriteLine("===== Drag And Drop Page Tests Completed =====\n");
        }

        // Function to execute Dropdown page tests
        static void RunDropdownPageTests(IWebDriver driver)
        {
            Console.WriteLine("===== Starting Dropdown Page Tests =====");

            DropdownPage dropdownPage = new DropdownPage(driver);
            dropdownPage.ExecuteAllTests();

            Console.WriteLine("===== Dropdown Page Tests Completed =====\n");
        }

        // Function to execute Dynamic Content page tests
        static void RunDynamicContentPageTests(IWebDriver driver)
        {
            Console.WriteLine("===== Starting Dynamic Content Page Tests =====");

            var dynamicContentPage = new DynamicContentPage(driver);
            dynamicContentPage.ExecuteAllTests();
;

            Console.WriteLine("===== Dynamic Content Page Tests Completed =====\n");
        }

        // Function to execute Dynamic Controls page tests
        static void RunDynamicControlsTests(IWebDriver driver)
        {
            Console.WriteLine("===== Starting Dynamic Controls Tests =====");

            DynamicControlsPage dynamicControlsPage = new DynamicControlsPage(driver);
            dynamicControlsPage.ExecuteAll();

            Console.WriteLine("===== Dynamic Controls Tests Completed =====\n");
        }


    }
}
