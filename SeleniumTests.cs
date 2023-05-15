using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using System;

namespace SeleniumTests
{
    public class SeleniumTests
    {

        private WebDriver driver;
        private const string baseUrl = "https://taskboard--kameliya-m.repl.co/";

        [SetUp]
        public void OpenWebApp()
        {
            this.driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(3);
            driver.Url = baseUrl;
        }
        [TearDown]
        public void CloseWebApp()
        {
            driver.Quit();
        }

        [Test]

        public void Test_Check_TaskTitle ()
            {
            var linkTaskBoard = driver.FindElement(By.LinkText("Task Board"));
            linkTaskBoard.Click();

            var cellDoneTask = driver.FindElement(By.CssSelector("body > main > div > div:nth-child(3) > h1"));
            var firstTaskName = driver.FindElement(By.CssSelector("#task1 > tbody > tr.title > td"));
            Assert.That(cellDoneTask.Text, Is.EqualTo("Done"));
            Assert.That(firstTaskName.Text, Is.EqualTo("Project skeleton"));
        }
        [Test]
        public void Test_SearchTaskByValidKeyword()
        {
            var linkSearch = driver.FindElement(By.LinkText("Search"));
            linkSearch.Click();

            var inputSearchField = driver.FindElement(By.Id("keyword"));

            inputSearchField.SendKeys("home");

            var buttonSearch = driver.FindElement(By.Id("search"));
            buttonSearch.Click();

            var resultTitle = driver.FindElement(By.CssSelector("#task2 > tbody > tr.title > td"));
            Assert.That(resultTitle.Text, Is.EqualTo("Home page"));
            
        }
        [Test]
        public void Test_SearchByInvalidKeyword()
        {
            var linkSearch = driver.FindElement(By.LinkText("Search"));
            linkSearch.Click();

            var inputSearchField = driver.FindElement(By.Id("keyword"));

            inputSearchField.SendKeys("missing");

            var buttonSearch = driver.FindElement(By.Id("search"));
            buttonSearch.Click();

            var resultMessage = driver.FindElement(By.Id("searchResult"));
            Assert.That(resultMessage.Text, Is.EqualTo("No tasks found."));
        }
        [Test]
        public void Test_CreateInvalidTask()
        {
          
            var iconCreate = driver.FindElement(By.PartialLinkText("Create"));
            iconCreate.Click();

            Thread.Sleep(500);

            var textBoxTitle = driver.FindElement(By.Id("title"));
            textBoxTitle.SendKeys("");

            var buttonCreate = driver.FindElement(By.Id("create"));
            buttonCreate.Click();

           
            var errorMsg = driver.FindElement(By.CssSelector("div.err"));
            Assert.That(errorMsg.Text, Is.EqualTo("Error: Title cannot be empty!"));
        }
        [Test]
        public void Test_Create_ValidTask()
        {
           
            var iconCreate = driver.FindElement(By.PartialLinkText("Create"));
            iconCreate.Click();
            Thread.Sleep(500);
            string title = "Title " + DateTime.Now.Ticks;
            string description = "Some description " + DateTime.Now.Ticks;


            var textBoxTitle = driver.FindElement(By.Id("title"));
            textBoxTitle.SendKeys(title);

            var textBoxDescription = driver.FindElement(By.Id("description"));
            textBoxDescription.SendKeys(description);


            var buttonCreate = driver.FindElement(By.Id("create"));
            buttonCreate.Click();

            
            var cellTitle = driver.FindElement(By.CssSelector(".tasks-grid div:nth-child(1) > table:last-child > tbody > tr.title > td"));
            Assert.That(cellTitle.Text, Is.EqualTo(title));

            var cellDescription = driver.FindElement(By.CssSelector("div.tasks-grid div:nth-child(1) > table:last-child > tbody >  tr.description > td"));
            Assert.That(cellDescription.Text, Is.EqualTo(description));

            
        }
    }
}
