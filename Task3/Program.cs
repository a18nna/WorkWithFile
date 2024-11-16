using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Укажите путь к папке: ");
            string fullPath = Path.GetFullPath(Console.ReadLine());

            Console.WriteLine($"Текущий размер каталога: {GetDirectorySize(fullPath)}");

            FolderCleaning(fullPath);

            Console.WriteLine($"После очистки размер каталога: {GetDirectorySize(fullPath)}");

            Console.ReadKey();
        }

        public static void FolderCleaning(string path)
        {
            int CountDeleteObj = 0;

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
                            CountDeleteObj++;
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
                            CountDeleteObj++;
                            Console.WriteLine($"Файл {file} удален.");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                    }
                }
                Console.WriteLine($"Удалено {CountDeleteObj} объектов из каталога");
            }
        }

        public static long GetDirectorySize(string path)
        {
            long totalSize = 0;
            if (!Directory.Exists(path))
            {
                Console.WriteLine($"Директория по пути '{path}' не существует.");
                return totalSize;
            }

            DirectoryInfo dir = new DirectoryInfo(path);
            totalSize = dir.EnumerateFiles("*.*", SearchOption.AllDirectories).Sum(fi => fi.Length);
            return totalSize;
        }
    }
}
