﻿using System;
using System.Globalization;
using System.IO;
using System.Linq;

namespace Dirs
{
    class Program
    {
        private static string message = "";
        private static string[] argss = { };

        // https://docs.google.com/document/d/1I1ct-BK_XRhGlXDqZ6_ZyFCBjYIU7QBzwGwHQSUeAt0/edit
        static void Main(string[] args)
        {
            Console.Clear();
            Console.WriteLine();
            PrintDir(Directory.GetCurrentDirectory());
            Cycle();
        }

        public static void Cycle()
        {
            while (true)
            {
                if (message == "")
                {
                    Console.Clear();
                    PrintDir(Directory.GetCurrentDirectory());
                }
                else
                    Console.WriteLine(message);

                Console.Write("-> ");
                string command = Console.ReadLine();
                argss = command.Split(' ')[1..];
                if (command.StartsWith("cd"))
                {
                    if (!cd(argss[0])) continue;
                }
                else if (command.StartsWith("mf"))
                {
                    File.Create(Directory.GetCurrentDirectory() + "\\" + argss[0]);
                }
                else if (command.StartsWith("rm"))
                {
                    if (argss[0].Contains('.')) File.Delete(Directory.GetCurrentDirectory());
                }
            }
        }

        public static void mf(string filename)
        {
        }

        public static bool cd(string dirr)
        {
            if (dirr == "..")
            {
                Directory.SetCurrentDirectory(string.Join('\\',
                    Directory.GetCurrentDirectory().Split('\\')[..^1]));
                return true;
            }

            if (dirr == ".")
                return true;
            string dd = Directory.GetCurrentDirectory() + "\\" + dirr;
            if (Directory.Exists(dd))
            {
                Directory.SetCurrentDirectory(dd);
                message = "";
                return true;
            }

            message = "Директория не найдена";
            return false;
        }

        static void PrintDir(string dirName)
        {
            Console.WriteLine("╔" + new string('═', dirName.Length + 2) + "╗");
            Console.WriteLine("║ " + dirName + " ║");
            Console.WriteLine('╠' + new string('═', Console.WindowWidth / 2 - 2) + '╦' +
                              new string('═', Console.WindowWidth / 2 - 2) + '╗');
            var files = Directory.GetFiles(dirName);
            var dirrs = Directory.GetDirectories(dirName);
            // foreach (var i in files) Console.WriteLine(i);
            for (int i = 0; i < Math.Max(files.Length, dirrs.Length); i++)
            {
                PrintLine(i < dirrs.Length ? dirrs[i].Split('\\')[^1] : "",
                    i < files.Length ? files[i].Split('\\')[^1] : "");
            }

            Console.WriteLine('╚' + new string('═', Console.WindowWidth / 2 - 2) + '╩' +
                              new string('═', Console.WindowWidth / 2 - 2) + '╝');
            if (message != "") Console.WriteLine(message);
        }

        static void PrintLine(string d, string f)
        {
            Console.Write("║  ");
            Console.Write(d);
            Console.Write(new string(' ', Console.WindowWidth / 2 - d.Length - 4));
            Console.Write("║  ");
            Console.Write(f);
            Console.Write(new string(' ', Console.WindowWidth / 2 - f.Length - 4));
            Console.WriteLine('║');
        }
    }
}