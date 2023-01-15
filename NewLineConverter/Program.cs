using System.Reflection.PortableExecutable;

namespace NewLineConverter;

public class Program
{
    public static int Main(string[] args)
    {
        var format = "-os";
        var folder = Environment.CurrentDirectory;
        var filters = new string[] { "*.txt" };
        var type = EOLType.Unknown;
        if (args.Length <2)
        {
            Console.WriteLine("NewLineConverter [-crlf(windows)/-lf(unix)/-cr(mac)/-os(depends)] folder filters ..");
            return 0;
        } 
        else if (args.Length >= 3)
        {
            format = args[0];
            folder = args[1];
            filters = args[2..];
            type = format.ToLower() switch
            {
                "-crlf" => EOLType.CRLF,
                "-lf" => EOLType.LF,
                "-cr" => EOLType.CR,
                "-os" => NewLineConvertReader.GetEOLType(Environment.NewLine) ,
                _ => NewLineConvertReader.GetEOLType(Environment.NewLine)
            };

        }
        else if (args.Length >= 2)
        {
            folder = args[0];
            filters = args[1..];
        }
        var info = new DirectoryInfo(folder);
        var total = 0;
        foreach(var filter in filters)
        {
            var count = 0;
            Console.WriteLine($"Converting for {filter}...");
            var files = info.GetFiles(filter, SearchOption.AllDirectories);
            foreach (var file in files)
            {
                try
                {
                    var content = File.ReadAllText(file.FullName);
                    var converted = NewLineConvertReader.ConvertAll(content, type);

                    if (converted != content)
                    {
                        File.WriteAllText(
                            file.FullName, converted);
                        Console.WriteLine("    Converted: " + file.FullName);
                    }
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine(ex.ToString());
                }
            }
            Console.WriteLine($"Converted files: {count}");
            total += count;
        }
        Console.WriteLine($"Total converted files: {total}");

        return 0;
    }
}