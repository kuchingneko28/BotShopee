using System;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
namespace BotShopee
{
    class Program
    {
        static void Main(string[] args)
        {
            // Untuk menjalankan program
            Menu();
        }
        static void Menu()
        {
            int input;
            // Menu
            Console.WriteLine("Bot Shopee Auto CheckOut");
            Console.WriteLine("Silahkan pilih: ");
            Console.WriteLine("1. Auto CheckOut");
            Console.WriteLine("2. Auto AddtoCart");
            Console.WriteLine("3. About this bot");
            Console.WriteLine("4. Exit");
            Console.Write("Masukan pilihan: ");
            // Input pilihan
            while(!int.TryParse(Console.ReadLine(), out input))
            {
                Console.WriteLine("Yang kamu masukan salah!");
                Console.Write("Masukan pilihan: ");
            }
            // Cek pilihan
            if(input == 1)
            {
            AutoCheckOut();
            }
            else if(input == 2 )
            {
            AutoAddCart();
            }
            else if(input == 3)
            {
            string userinput;
            string readme = System.IO.File.ReadAllText("readme.txt");
            Console.Clear();
            Console.WriteLine(readme);
            Console.Write("Kembali ke menu ? [Y]/[N]: ");
            userinput = Console.ReadLine();
            if(userinput == "y" || userinput == "Y")
            {
                Console.Clear();
                Menu();
            }
            else if (userinput == "n" || userinput == "N")
            {
            Environment.Exit(0);
            }
            else
            {
                Console.Clear();
                Menu();
            }
            }
             else if(input == 4)
            {
            Environment.Exit(0);  
            }
            else
            {
                
            }
        }
        static void AutoAddCart()
        {
            bool check = true;
            // Get data dari txt file
            string[] login = System.IO.File.ReadAllLines("login.txt");
            string[] url = System.IO.File.ReadAllLines("url.txt");
            // Driver
            IWebDriver driver = new ChromeDriver();
            IJavaScriptExecutor js = (IJavaScriptExecutor) driver;
            Random r = new Random();

            // Untuk akses browser ke website
            driver.Navigate().GoToUrl(url[0]);
            driver.Manage().Window.Maximize();
            Thread.Sleep(2000);  
            js.ExecuteScript("window.scrollBy(0,500)", "");
            driver.FindElement(By.XPath("//*[@class='btn btn-tinted btn--l YtgjXY _3a6p6c']")).Click();
            
            // Untuk input email dan password
            Thread.Sleep(4000);
            var username = driver.FindElement(By.XPath("//*[@name='loginKey']"));
            var password = driver.FindElement(By.XPath("//*[@name='password']"));
            username.SendKeys(login[0]);
            password.SendKeys(login[1]);
            password.SendKeys(Keys.Enter);
            Thread.Sleep(5000);

            js.ExecuteScript("window.scrollBy(0,200)", "");
            
            // Check pilihan tersedia atau tidak
            try
            {
            string autoPilih = string.Format("//*[@class='product-variation']", r.Next(5)); 
            driver.FindElement(By.XPath(autoPilih)).Click();
            }
            catch(NoSuchElementException )
            {
            // Otomatis Add to Cart
            }
            do
            {
            try
            {
            driver.FindElement(By.XPath("//*[@class='btn btn-tinted btn--l YtgjXY _3a6p6c']")).Click();
            driver.FindElement(By.XPath("//*[@class='action-toast']"));
            }
            catch(NoSuchElementException)
            {
            driver.Navigate().Refresh();
            try
            {
            string autoPilih = string.Format("//*[@class='product-variation']", r.Next(5)); 
            driver.FindElement(By.XPath(autoPilih)).Click();
            }
            catch(NoSuchElementException )
            {   
            }
            driver.FindElement(By.XPath("//*[@class='btn btn-tinted btn--l YtgjXY _3a6p6c']")).Click();
            driver.FindElement(By.XPath("//*[@class='action-toast']"));
            }
            }while(!check);
            // Add to Cart Selesai
            Console.Clear();
        }
        static void AutoCheckOut()
        {
            // Get data dari txt file
            string[] login = System.IO.File.ReadAllLines("login.txt");
            string[] url = System.IO.File.ReadAllLines("url.txt");

            // Driver 
            IWebDriver driver = new ChromeDriver();
            IJavaScriptExecutor js = (IJavaScriptExecutor) driver;
            Random r = new Random();

            // Untuk akses browser ke website
            driver.Navigate().GoToUrl(url[0]);
            driver.Manage().Window.Maximize();
            Thread.Sleep(2000);  
            js.ExecuteScript("window.scrollBy(0,500)", "");
            driver.FindElement(By.XPath("//*[@class='btn btn-solid-primary btn--l YtgjXY']")).Click();
            
            // Untuk input email dan password
            Thread.Sleep(4000);
            var username = driver.FindElement(By.XPath("//*[@name='loginKey']"));
            var password = driver.FindElement(By.XPath("//*[@name='password']"));
            username.SendKeys(login[0]);
            password.SendKeys(login[1]);
            password.SendKeys(Keys.Enter);
            Thread.Sleep(5000);

            js.ExecuteScript("window.scrollBy(0,200)", "");
            
            // Check pilihan tersedia atau tidak
            try
            {
            string autoPilih = string.Format("//*[@class='product-variation']", r.Next(5)); 
            driver.FindElement(By.XPath(autoPilih)).Click();
            }
            catch(NoSuchElementException)
            {
            }

            // Otomatis checkout
            js.ExecuteScript("window.scrollBy(0,400)", "");
            driver.FindElement(By.XPath("//*[@class='btn btn-solid-primary btn--l YtgjXY']")).Click();
            Thread.Sleep(5000);
            driver.FindElement(By.CssSelector(".shopee-button-solid--primary")).Click();
            Thread.Sleep(5000);
            js.ExecuteScript("window.scrollBy(0,750)", "");
            driver.FindElement(By.XPath("//*[@class='product-variation'][contains(text(), 'Transfer Bank')]")).Click();
            Thread.Sleep(2000);
            driver.FindElement(By.XPath("//*[@class='checkout-bank-transfer-item__title'][contains(text(), 'Bank BNI (Dicek Otomatis)')]")).Click();
            // Check pop up
            try
            {
            driver.FindElement(By.XPath("//*[@class='stardust-popup-button stardust-popup-button--main']")).Click();
            }
            catch(NoSuchElementException)
            {
            }
            Thread.Sleep(5000);
            js.ExecuteScript("window.scrollBy(0,660)", "");
            driver.FindElement(By.CssSelector(".page-checkout .stardust-button--primary")).Click();
            
            // Checkout Selesai
            Console.Clear();
        }

    }
}
