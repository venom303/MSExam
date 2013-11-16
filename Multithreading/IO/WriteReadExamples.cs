using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Examples.IO
{
    class WriteReadExamples
    {
        private const string FilePath = @"C:\file.txt";

        [Example("File  example", false)]
        public void FileExamples()
        {
            var file = new FileInfo(FilePath);

            Console.WriteLine(file.Directory.Root);

            var stream = file.Open(FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None);

            Console.ReadKey();

            stream.Close();
        }

        private bool Waiter = false;
        private readonly object _locker = new object();

        [Example("Directory example", false)]
        public void DirectoryExamples()
        {
            var autoResetEvent = new AutoResetEvent(false);

            var directory = Directory.GetParent(FilePath);


            var task = Task.Run(() => { ReadDirRecursively(directory, 0); 
                Console.WriteLine("The end"); });
            while (true)
            {
                Console.ReadKey();
                lock (_locker)
                    Waiter = true;
            }
        }

        private void ReadDirRecursively(DirectoryInfo dirInfo, int counter)
        {
            var directories = dirInfo.GetDirectories().ToList();
            foreach(var dir in directories)
            {
                lock (_locker)
                {
                    if(Waiter)
                    {
                        Waiter = false;
                        Thread.Sleep(1000);
                    }
                }
                Console.WriteLine(counter + ": " + dir.FullName);
                ReadDirRecursively(dir, counter + 1);
            }
        }

        [Example("File Stream example", false)]
        public void FileStreamExamples()
        {
            var stream = new FileStream(FilePath, FileMode.Create);

            for (int i = 0; i < 10; i++)
            {
                byte[] number = new UTF8Encoding(true).GetBytes(i.ToString());
                stream.Write(number, 0, number.Length);
            }

            stream.Close();

        }

        [Example("Reader Writer example", false)]
        public void ReaderWriterExamples()
        {
            StreamReader streamReader = new StreamReader(FilePath);
            Console.WriteLine("Char by Char");
            while (!streamReader.EndOfStream)
            {
                Console.WriteLine((char)streamReader.Read());
            }

            streamReader.Close();

            streamReader = new StreamReader(FilePath);
            Console.WriteLine("Line by line");
            while (!streamReader.EndOfStream)
            {
                Console.WriteLine(streamReader.ReadLine());
            }

            streamReader.Close();

            streamReader = new StreamReader(FilePath);
            Console.WriteLine("Entire file");
            Console.WriteLine(streamReader.ReadToEnd());
        }

        [Example("Text Writer example", false)]
        public void TextWriterExamples()
        {
            var streamWriter = new StreamWriter(FilePath);
            var stringWriter = new StringWriter();
            var fileStream = new FileStream(FilePath, FileMode.OpenOrCreate);
            var binaryWriter = new BinaryWriter(fileStream);
        }
    }
}
