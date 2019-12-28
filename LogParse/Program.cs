using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace LogParse
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = @"C:\ProgramData\InduSoft\I-LDS\2\Log";
            LogControll logControll;

            DirectoryInfo dir = new DirectoryInfo(path);
            List<string> fileNames = new List<string>();

            foreach (var item in dir.GetFiles())
            {
                fileNames.Add(item.FullName);
            }

            logControll = new LogControll(fileNames.First());
            logControll.LoadExceptions();
            logControll.ExceptionInfoControll.SimplyPrintExceptionInfoList();


            Console.ReadLine();

        }
    }
}
