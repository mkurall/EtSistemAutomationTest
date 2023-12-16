using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Threading;

namespace EtSistemTest
{
    [TestFixture]
    public class UserTests
    {
        static IWebDriver Driver;

        [OneTimeSetUp]
        public void Setup()
        {

            Driver = new ChromeDriver();
 

            Console.WriteLine("Test started...");
        }
        
        [Test]
        public void Test001_UserFailLogin()
        {
            Driver.Navigate().GoToUrl("http://localhost:3917/");

            var txtUserName = Driver.FindElement(By.Id("username"));
            var txtPassword = Driver.FindElement(By.Id("password"));
            var btnLogin = Driver.FindElement(By.XPath("/html/body/div[2]/div/div/div[1]/form/div[4]/button"));

            txtUserName.SendKeys("mustafakural@outlook.com");
            txtPassword.SendKeys("3535");
            btnLogin.Submit();

            var lblInvalidUserOrPassword = Driver.FindElement(By.XPath("/html/body/div[2]/div/div/div[1]/div/div/ul/li"));

            Assert.That(lblInvalidUserOrPassword.Displayed, Is.True, "Hatal� kullan�c� yada parola mesaj� g�r�nmeli");

            Assert.Pass();
        }

        [Test]
        public void Test002_UserSuccessLogin()
        {
            Driver.Navigate().GoToUrl("http://localhost:3917/");

            var txtUserName = Driver.FindElement(By.Id("username"));
            var txtPassword = Driver.FindElement(By.Id("password"));
            var btnLogin = Driver.FindElement(By.XPath("/html/body/div[2]/div/div/div[1]/form/div[4]/button"));

            txtUserName.SendKeys("mustafakural@outlook.com");
            txtPassword.SendKeys("1234");
            btnLogin.Submit();
            
            var lnkReadBarcode = Driver.FindElement(By.XPath("//*[@id=\"navbarDropdown2\"]"));

            Assert.That(lnkReadBarcode.Displayed, Is.True, "Giri� yap�ld�ysa barkod okuma butonu g�r�nmeli");

            Assert.Pass();
        }
       
        [Test]
        public void Test003_UserMustNotAccessUserList()
        {
            Driver.Navigate().GoToUrl("http://localhost:3917/User/List");

            Assert.IsTrue(!Driver.Url.EndsWith("/User/List"),"Kullan�c�, kullan�c�lar sayfas�na eri�emez");

            Assert.Pass();
        }

        [Test]
        public void Test004_UserLogout()
        {
            Driver.Navigate().GoToUrl("http://localhost:3917/");


            var txtUserName = Driver.FindElement(By.Id("username"));
            var txtPassword = Driver.FindElement(By.Id("password"));
            var btnLogin = Driver.FindElement(By.XPath("/html/body/div[2]/div/div/div[1]/form/div[4]/button"));

            txtUserName.SendKeys("mustafakural@outlook.com");
            txtPassword.SendKeys("1234");
            btnLogin.Submit();

            var lnkUser = Driver.FindElement(By.XPath("//*[@id=\"navbarDropdown2\"]"));
            var lnkUserLogout = Driver.FindElement(By.XPath("//*[@id=\"navbarCollapse\"]/ul[2]/li/ul/li[3]/a"));

            Assert.That(lnkUser.Displayed, Is.True, "Kullan�c� men�s� g�r�nmeli");

            lnkUser.Click();

            lnkUserLogout.Click();

            btnLogin = Driver.FindElement(By.XPath("/html/body/div[2]/div/div/div[1]/form/div[4]/button"));

            Assert.That(btnLogin.Displayed, Is.True, "Giri� sayfas� gelmeli");

            Assert.Pass();
        }
        
        [OneTimeTearDown]
        public void CloseDriver()
        {
            Thread.Sleep(3000);
            Driver.Close();
        }
    }
}