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
                DumpHex(file);
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
        // Given a file, dump the file contents
        public static void DumpHex(FileInfo file)
        {
            FileStream fs;
            BinaryReader reader;
            try
            {
                fs = file.OpenRead();
                // Wreap the filestream in a BinaryReader
                reader = new BinaryReader(fs);
            }
            catch(Exception e) {
                Console.WriteLine("Cannot read from {0}", file.FullName);
                Console.WriteLine(e.Message);
                return;
            }

            // Iterate through the contents of the file one line at the time
            for(int line = 1; true; line++)
            {
                byte[] buffer = new byte[10];
                int numBytes = reader.Read(buffer, 0, buffer.Length);
                if (numBytes == 0)
                {
                    return;
                }
                // write the datea in a single line
                Console.Write("{0:D3} - ", line);
                DumpBuffer(buffer, numBytes);

                //stop every 20 lines
                if((line % 20) == 0)
                {
                    Console.WriteLine("return");
                    Console.ReadLine();
                }
            }

        }

        private static void DumpBuffer(byte[] buffer, int numBytes)
        {
            for (int index = 0; index < numBytes; index++)
            {
                byte b = buffer[index];
                Console.Write("{0:X2}, ", b);
            }
            Console.WriteLine() ;
        }
    }
}
