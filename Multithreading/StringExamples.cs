using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examples
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

        [Example("Standard Date formating", false)]
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

        [Example("Parse Decimal Example", false)]
        public void ParseDecimalExample()
        {
            // Fails
            // decimal amount = decimal.Parse("$123,456.78");

            decimal amount = decimal.Parse("$123,456.78", NumberStyles.AllowCurrencySymbol | NumberStyles.AllowThousands | NumberStyles.AllowDecimalPoint);
            Console.WriteLine(amount);

            Console.ReadKey();
        }

        [Example("Array Example", false)]
        public void ArrayExample()
        {
            // Make an array of numbers.
            int[] array1 = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            // This doesn't work because array1.Clone is an object.
            dynamic array2 = array1.Clone();

            // This works.
            int[] array3 = (int[])array1.Clone();
            array3[5] = 55;

            // This also works.
            dynamic array4 = array1.Clone();
            array4[6] = 66;

            array4[7] = "This won't work";
        }
    }
}