using Aspose.Zip.Rar;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtractFilesFromCompressedRar
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ExtractRar.ExtractFiles();
            ExtractZip.ExtractFiles();

            Console.WriteLine("Finished");
        }
    }
}
