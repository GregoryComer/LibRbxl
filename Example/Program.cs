using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibRbxl;

namespace Example
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Enter a filename.");
                var placeFile = Console.ReadLine();
                if (!File.Exists(placeFile))
                {
                    Console.WriteLine($"File \"{placeFile}\" does not exist.");
                    continue;
                }
                var sw = Stopwatch.StartNew();
                Console.WriteLine("Processing document...");
                var document = RobloxDocument.FromFile(placeFile);
                Console.WriteLine($"Place loaded. ({document.Objects.Count} instances in {sw.Elapsed.TotalSeconds} seconds)");
            }
        }
    }
}
