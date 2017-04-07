using NUnitLite;
using System;
using System.Reflection;

namespace EasyIoC.Tests
{
    public class Program
    {
        public static int Main(string[] args)
        {
            Console.Out.WriteLine("Starting Tests");
            return new AutoRun(typeof(Program).GetTypeInfo().Assembly).Execute(args);
        }
    }
}