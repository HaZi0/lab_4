using System;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        if (args.Length < 2)
        {
            Console.WriteLine("Потрібно вказати шляхи до каталогів та шаблон файлів.");
            return;
        }

        string[] directoryPaths = new string[args.Length - 1];
        Array.Copy(args, 0, directoryPaths, 0, args.Length - 1);
        string searchPattern = args[args.Length - 1];

        try
        {
            long totalSize = GetTotalFileSize(directoryPaths, searchPattern);
            Console.WriteLine($"Сумарний обсяг файлів: {totalSize} байт.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Виникла помилка: {ex.Message}");
        }
    }

    static long GetTotalFileSize(string[] directoryPaths, string searchPattern)
    {
        long totalSize = 0;

        foreach (string directoryPath in directoryPaths)
        {
            if (!Directory.Exists(directoryPath))
            {
                Console.WriteLine($"Каталог '{directoryPath}' не існує.");
                continue;
            }

            string[] files = Directory.GetFiles(directoryPath, searchPattern, SearchOption.AllDirectories);

            foreach (string file in files)
            {
                FileInfo fileInfo = new FileInfo(file);
                totalSize += fileInfo.Length;
            }
        }

        return totalSize;
    }
}