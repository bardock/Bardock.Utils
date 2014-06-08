using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bardock.Utils.Logger.Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            LogManager.Default.GetLog<Program>().Error("Program - error");
            LogManager.Default.GetLog<Program>().Warn("Program - warn");
            LogManager.Default.GetLog<Program>().Debug("Program - debug");
            LogManager.Default.GetLog<Program>().Info("Program - info");
            LogManager.Default.GetLog<MyClass>().Error("MyClass - error");
            LogManager.Default.GetLog(new MyClass()).Warn("MyClass instance - warn");

            Console.WriteLine("DONE");
            Console.ReadLine();
        }

        public class MyClass { }
    }
}
