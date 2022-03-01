using System;
using System.Globalization;
using System.IO;

namespace Dirs
{
    class Program
    {
        private static string message = "";
        private static string dir = string.Join('\\', Directory.GetCurrentDirectory().Split('\\')[..^5]);
        private static string[] argss = { };

        // https://docs.google.com/document/d/1I1ct-BK_XRhGlXDqZ6_ZyFCBjYIU7QBzwGwHQSUeAt0/edit
        static void Main(string[] args)
        {
            Console.Clear();
            Console.WriteLine();
            Cycle();
        }

        public static void Cycle()
        {
            while (true)
            {
                PrintDir(dir);
                string command = Console.ReadLine();
                argss = command.Split(' ')[1..];
                if (command.StartsWith("cd"))
                {
                    cd(argss[0]);
                } else if (command.StartsWith("mf"))
                {
                    
                }

                Console.Clear();
            }
        }

        public static void cd(string dirr)
        {
            if (dirr == "..")
            {
                Directory.SetCurrentDirectory(string.Join('\\',
                    Directory.GetCurrentDirectory().Split('\\')[..^1]));
            }
            else if (dirr == ".")
            {
            }
            else
            {
                string dd = Directory.GetCurrentDirectory() + dirr;
                if (Directory.Exists(dd))
                {
                    Directory.SetCurrentDirectory(dd);
                    message = "";
                }
                else
                {
                    message = "Директория не найдена";
                }
            }
        }

        static void PrintDir(string dirName)
        {
            Console.Write("╔═══" + dirName);
            Console.WriteLine(
                new string('═', Console.WindowWidth / 2 - 5 - dirName.Length) + '╦' +
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
            Console.Write("-> ");
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