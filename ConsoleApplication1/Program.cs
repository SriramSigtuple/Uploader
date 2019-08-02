using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
namespace ConsoleApplication1
{
    class Program
    {
        delegate string TestDelegate(string s);
        static void Main(string[] args)
        {
            FactoryCamera f = new FactoryCamera();
            CameraAbtract c1 = f.CreateCamera("Vendor1");
            CameraAbtract c2 = f.CreateCamera("Vendor2");
            Console.WriteLine(c1.cameraVendorInfo());
            Console.WriteLine(c2.cameraVendorInfo());
            c1.virtualMethod();
            c2.virtualMethod();

            FactoryCameraImpl fImpl = new FactoryCameraImpl();
            ICamera cImpl1 = fImpl.CreateCamera("Vendor1");
            ICamera cImpl2 = fImpl.CreateCamera("Vendor2");
            Console.WriteLine(cImpl1.cameraVendorInfo());
            Console.WriteLine(cImpl2.cameraVendorInfo());

            Action printText = new Action(MyClass.PrintText);

            printText();
            //Func<string, string> printWithFunc = new Func<string, string>(MyClass.PrintTextWithReturn);
            Func<string> printWithFunc = () =>
            {
                string value = "This is a inline test ";
                value += "sriram";
                return value;
            };

            Action<string> inlineAction = x =>
                {
                    string value = "This is a inline test Action  ";
                    value += x;
                    Console.WriteLine(value);
                };
            //new Func<string, string>(MyClass.PrintTextWithReturn);
            TestDelegate del = new TestDelegate(MyClass.PrintTextWithReturn);
           Console.WriteLine( del("Test Delegate"));
          Console.WriteLine(  printWithFunc());
          inlineAction("sriram");

            Console.ReadKey();
        }
    }
    public class MyClass
    {
        public MyClass()
        {

        }
        public static void PrintText()
        {
            Console.WriteLine("Printing the text");
        }
        public static String PrintTextWithReturn(string Value)
        {
            return "Printing the text " + Value;
        }

    }
}
