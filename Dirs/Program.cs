using System;
using System.Globalization;
using System.IO;
using System.Linq;

namespace Dirs
{
    class Program
    {
        // https://docs.google.com/document/d/1I1ct-BK_XRhGlXDqZ6_ZyFCBjYIU7QBzwGwHQSUeAt0/edit

        private static char separator = '/';

        static void Main(string[] args)
        {
            if (OperatingSystem.IsWindows()) separator = '\\';
            Console.Clear();
            Console.WriteLine();
            PrintDir(Directory.GetCurrentDirectory());
            Cycle();
        }

        private static void Cycle()
        {
            string message = "";
            string[] argss;
            while (true)
            {
                if (message == "")
                {
                    PrintDir(Directory.GetCurrentDirectory());
                }
                else
                    Console.WriteLine(message);

                Console.Write("-> ");
                string command = Console.ReadLine();
                argss = command.Split(' ');
                if (argss[0] == "exit")
                {
                    Console.WriteLine("Пока...");
                    break;
                }

                if (argss[0] == "cd") message = Cd(argss[1]);
                else if (argss[0] == "touch") message = Touch(argss[1]);
                else if (argss[0] == "md") message = MakeDir(argss[1]);
                else if (argss[0] == "removeF") message = RemoveFile(argss[1]);
                else if (argss[0] == "removeD") message = RemoveDir(argss[1]);
                else message = "Команда не найдена";
            }
        }

        private static string Touch(string fileName)
        {
            if (File.Exists(fileName)) return "Файл уже существует";
            File.Create(fileName);
            return "";
        }

        private static string MakeDir(string dirName)
        {
            if (Directory.Exists(dirName)) return "Директория уже существует";
            Directory.CreateDirectory(dirName);
            return "";
        }

        private static string RemoveDir(string dirName)
        {
            if (!Directory.Exists(dirName)) return "Такой директории нет";
            Directory.Delete(dirName);
            return "";
        }

        private static string RemoveFile(string fileName)
        {
            if (!File.Exists(fileName)) return "Такого файла нет";
            File.Delete(fileName);
            return "";
        }

        private static string Cd(string dirr)
        {
            if (!Directory.Exists(dirr)) return "Директория не найдена";
            Directory.SetCurrentDirectory(dirr);
            return "";
        }

        private static void PrintDir(string dirName)
        {
            Console.Clear();
            Console.WriteLine("╔" + new string('═', dirName.Length + 2) + "╗");
            Console.WriteLine("║ " + dirName + " ║");
            Console.WriteLine('╠' + new string('═', Console.WindowWidth / 2 - 2) + '╦' +
                              new string('═', Console.WindowWidth / 2 - 2) + '╗');
            var files = Directory.GetFiles(dirName);
            var dirrs = Directory.GetDirectories(dirName);
            // foreach (var i in files) Console.WriteLine(i);
            for (int i = 0; i < Math.Max(files.Length, dirrs.Length); i++)
            {
                PrintLine(i < dirrs.Length ? dirrs[i].Split(separator)[^1] : "",
                    i < files.Length ? files[i].Split(separator)[^1] : "");
            }

            Console.WriteLine('╚' + new string('═', Console.WindowWidth / 2 - 2) + '╩' +
                              new string('═', Console.WindowWidth / 2 - 2) + '╝');
        }

        private static void PrintLine(string d, string f)
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