using System.Diagnostics;

namespace Проводничёк2_0
{
    internal class Explorer
    {
        public static string? GetDriver()
        {
            Console.Clear();
            Console.WriteLine("Выберите диск");
            Console.WriteLine();

            DriveInfo[] allDrives = DriveInfo.GetDrives();
            foreach (DriveInfo drive in allDrives)
            {
                double freeSpace = Math.Round(drive.TotalFreeSpace / 1000000000f, 2);
                double totalSpace = Math.Round(drive.TotalSize / 1000000000f, 2);
                Console.WriteLine($"  {drive.Name} {freeSpace} ГБ/{totalSpace} ГБ");
            }

            Arrow arrow = new Arrow(2, allDrives.Length + 1);
            ConsoleKeyInfo key = Console.ReadKey(true);
            while (key.Key != ConsoleKey.Escape)
            {
                switch (key.Key)
                {
                    case ConsoleKey.DownArrow:
                        arrow.Next();
                        break;
                    case ConsoleKey.UpArrow:
                        arrow.Back();
                        break;
                    case ConsoleKey.Enter:
                        int index = arrow.GetIndex();
                        return allDrives[index].Name;
                }
                key = Console.ReadKey(true);
            }

            return null;
        }

        public static string GetDir(string path)
        {
            Console.Clear();
            Console.WriteLine($"Директория: {path}");
            Console.WriteLine("");

            string[] dirs = Directory.GetDirectories(path);
            string[] files = Directory.GetFiles(path);

            List<string> all = new List<string>();

            foreach (string dir in dirs)
            {
                all.Add(dir);
                DirectoryInfo info = new DirectoryInfo(dir);
                Console.WriteLine($"  {info.Name} | {info.CreationTime}");
            }
            foreach (string file in files)
            {
                all.Add(file);
                FileInfo info = new FileInfo(file);
                Console.WriteLine($"  {info.Name} | {info.Extension} | {info.CreationTime}");
            }

            Arrow arrow = new Arrow(2, all.Count + 1);
            ConsoleKeyInfo key = Console.ReadKey(true);
            while (key.Key != ConsoleKey.Escape)
            {
                switch (key.Key)
                {
                    case ConsoleKey.DownArrow:
                        arrow.Next();
                        break;
                    case ConsoleKey.UpArrow:
                        arrow.Back();
                        break;
                    case ConsoleKey.Enter:
                        int index = arrow.GetIndex();
                        string select = all[index];
                        if (Directory.Exists(select))
                        {
                            return select;
                        }
                        else if (File.Exists(select))
                        {
                            Process.Start(new ProcessStartInfo { FileName = select, UseShellExecute = true });
                        }
                        break;
                }
                key = Console.ReadKey(true);
            }

            DirectoryInfo parent = Directory.GetParent(path);
            if (parent == null)
            {
                return "";
            }
            else
            {
                return parent.FullName;
            }
        }
    }
}
