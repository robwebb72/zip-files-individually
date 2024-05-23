using System.IO.Compression;

namespace ZipFiles;

public static class Program
{
    public static void Main(string [] args)
    {
        var (path, extensionList) = ProcessArguments(args);
        if (path == string.Empty)
            return;

        var files = Directory.EnumerateFiles(path, "*.*", SearchOption.TopDirectoryOnly);

        foreach (var file in files)
        {
            var extension = Path.GetExtension(file);
            if(extensionList.Contains(extension.ToLower()))
                CreateZipFile(file);
        }
        Console.WriteLine("Finished.");    
    }

    private static (string, List<string>) ProcessArguments(string[] arguments)
    {
        if (arguments.Length != 2)
        {
            Console.WriteLine(
                "usage is zip-files <extension-list> <path>\n\n" + 
                "where extension list is a comma separated list of extensions to zip."
            );
            
            return (string.Empty, []);
        }

        var extensions = arguments[0].Split(",").Select(a => a.Trim().TrimStart('.')).Select(b => "." + b).ToList();

        return (arguments[1], extensions);
    }

    private static void CreateZipFile(string file)
    {
        var zipFileName = 
            $"{Path.GetDirectoryName(file)}{Path.DirectorySeparatorChar}{Path.GetFileNameWithoutExtension(file)}.zip";
            
        Console.Write($"zipping   {file}...  ");
        if (File.Exists(zipFileName))
        {
            Console.WriteLine("not written. zip file already exists.");
            return;
        }
            
        using var zip = ZipFile.Open(zipFileName, ZipArchiveMode.Create);
        zip.CreateEntryFromFile(file, Path.GetFileName(file));
        File.Delete(file);
        Console.WriteLine("done.");
    }
}
