// See https://aka.ms/new-console-template for more information

using System.IO.Compression;



var path = @"d:\test";

var files =Directory.EnumerateFiles(
    path, 
    "*.*", 
    SearchOption.TopDirectoryOnly);

var validExtensions = new List<string> {".lha"};

foreach (var file in files)
{
    var extension = Path.GetExtension(file);
    var fileName = Path.GetFileNameWithoutExtension(file);
    var filePath = Path.GetDirectoryName(file);
    
    if(!validExtensions.Contains(extension.ToLower())) continue;
    var zipFileName = $"{filePath}{Path.DirectorySeparatorChar}{fileName}.zip";
    if (File.Exists(zipFileName)) continue;
    using var zip = ZipFile.Open(zipFileName, ZipArchiveMode.Create);
        zip.CreateEntryFromFile(file, Path.GetFileName(file));
    File.Delete(file);
}
Console.WriteLine("Finished.");