using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Task1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Укажите путь к папке: ");
            string fullPath = Path.GetFullPath(Console.ReadLine());

            FolderCleaning(fullPath);

            Console.ReadKey();
        }

        public static void FolderCleaning(string path)
        {
            if (Directory.Exists(path))
            {
                TimeSpan LastAccess = TimeSpan.FromMinutes(30);

                string[] dirs = Directory.GetDirectories(path);
                foreach (string dir in dirs)
                {
                    if ((Directory.GetLastAccessTime(dir)).Add(LastAccess) < DateTime.Now)
                    {
                        try
                        {
                            Directory.Delete(dir, true);
                            Console.WriteLine($"Каталог {dir} удален.");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                    }
                }

                string[] files = Directory.GetFiles(path);
                foreach (string file in files)
                {
                    if ((File.GetLastAccessTime(file)).Add(LastAccess) < DateTime.Now)
                    {
                        try
                        {
                            File.Delete(file);
                            Console.WriteLine($"Файл {file} удален.");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                    }
                }
            }
        }
    }
}
