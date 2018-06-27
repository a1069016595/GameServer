using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LolServer
{
    //public enum LogLevel
    //{
    //    Test,
    //    Error,
    //    Normal,
    //}

    class Debug
    {

        public static void Log(object mes)
        {
            Console.WriteLine(mes);
        }

        public static void LogError(object mes)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(mes);
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static void LogTest(object mes)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(mes);
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
