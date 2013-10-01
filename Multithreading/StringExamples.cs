using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Multithreading
{
    class StringExamples
    {
        public void StringWriter()
        {
            var stringWriter = new StringWriter();
            stringWriter.WriteLine("test");

            //var stream = stringWriter.
        }

        [Example("Standard formating", false)]
        public void StandardFormating()
        {
            Console.WriteLine(string.Format("{0,10}", "abc"));
            Console.WriteLine(string.Format("{0,10}", "abcdef"));
            Console.WriteLine(string.Format("{0,10}", "abcdefghi"));
            Console.WriteLine(string.Format("{0,10}", "abcdefghijk"));
            Console.WriteLine();

            Console.WriteLine("{0:X}", 163);

            Console.WriteLine(1234.769.ToString("C"));
            Console.WriteLine(string.Format("{0:C}", 1234.769));
            Console.WriteLine(string.Format("{0:C4}", 1234.769));

            Console.WriteLine(1234.ToString("d"));
            Console.WriteLine(string.Format("{0:n}", 1234.769));
            Console.WriteLine(string.Format("{0:p}", 1234.769));
            Console.WriteLine(string.Format("{0:f}", 1234.769));
            Console.WriteLine(string.Format("{0:e}", 1234.769));

            Console.ReadKey();
        }

        [Example("Standard Date formating", true)]
        public void StandardDateFormating()
        {
            Console.WriteLine(string.Format("{0:d}", DateTime.Now));
            Console.WriteLine(string.Format("{0:D}", DateTime.Now));
            Console.WriteLine(string.Format("{0:F}", DateTime.Now));
            Console.WriteLine(string.Format("{0:t}", DateTime.Now));
            Console.WriteLine(string.Format("{0:T}", DateTime.Now));
            Console.WriteLine(string.Format("{0:dd-MM-yyyy MM:hh}", DateTime.Now));

            Console.ReadKey();
        }
    }
}
