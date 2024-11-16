using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введите путь к файлу: ");

            string fullPath = Path.GetFullPath(Console.ReadLine());

            try
            {
                if (Directory.Exists(fullPath))
                {
                    GetDirectorySize(fullPath);
                }
                else
                {
                    Console.WriteLine($"Директория по пути '{fullPath}' не существует.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
            Console.ReadKey();
        }

        public static void GetDirectorySize(string path)
        {
            if (!Directory.Exists(path))
            {
                Console.WriteLine($"Директория по пути '{path}' не существует.");
                return;
            }

            DirectoryInfo dir = new DirectoryInfo(path);
            long totalSize = dir.EnumerateFiles("*.*", SearchOption.AllDirectories).Sum(fi => fi.Length);
            Console.WriteLine($"Размер каталога '{path}': {totalSize} байт");
        }
    }
}
