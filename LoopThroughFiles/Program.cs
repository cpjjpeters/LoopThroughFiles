using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace LoopThroughFiles
{
    class Program
    {
        static void Main(string[] args)
        {
            // if no directoryname provided
            string directoryName;
            if (args.Length == 0)
            {
                directoryName = Directory.GetCurrentDirectory();
            }
            else
            {
                directoryName = args[0];
            }
            Console.WriteLine("Directoryname="+directoryName);

            // Get a list f all files in that dir
            FileInfo[] files = GetFileList(directoryName);

            // now iterate
            //performing a HEXdump of each file
            foreach(FileInfo file in files)
            {
                Console.WriteLine("\n\nhex dump of file {0}:",file.FullName);
                //now DUMP
               // DumpHex(file);
                Console.WriteLine(" enter return to continue to next file");
                Console.ReadLine();
            }
            Console.WriteLine("\n No files left");

            Console.Read();
        }

        private static FileInfo[] GetFileList(string directoryName)
        {
            // start with empty list
            FileInfo[] files = new FileInfo[0];
            try
            {
                DirectoryInfo di = new DirectoryInfo(directoryName);

                files = di.GetFiles();
            }
            catch (Exception e)
            {
                Console.WriteLine("Dir {0} invalid", directoryName);
                Console.WriteLine(e.Message);
            }
            return files;

        }
    }
}
