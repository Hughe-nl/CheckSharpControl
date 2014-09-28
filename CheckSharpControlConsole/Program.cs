using CheckSharpControlLibrary;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CheckSharpControlConsole
{
    class Program
    {


        static void Main(string[] args)
        {
            if (args.Length != 1)
            {
                PrintUsage(Environment.CommandLine.Split()[0]);
                return;
            }


            var memory = new MemoryStream();
            var file = new StreamReader(args[0]);

            CheckSharpControl.Check(file, new StreamWriter(memory));
            memory.Seek(0, 0);

            TextReader reader = new StreamReader(memory);
            string line;

            while ((line = reader.ReadLine()) != null)
            {
                Regex r = new Regex("^\\[([^\\[\\]]*)\\].*$");
                string match = "";

                try
                {
                    match = r.Match(line).Groups[1].Value;
                }
                catch (Exception e)
                {

                }

                if (match == "")
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(line);
                    continue;
                }
                else if (match == "fail")
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                }
                else if (match == "pass")
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                }

                Console.Write(line.Substring(0, match.Length + 2));

                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine(line.Substring(match.Length + 2));


            }




        }

        private static void PrintUsage(string programName)
        {
            Console.WriteLine("Usage: {0} <checksharpcontrol.config>", programName);
        }


    }
}
