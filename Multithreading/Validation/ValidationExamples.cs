using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Examples.Validation
{
    class ValidationExamples
    {
        [Example("Regex Example", false)]
        public void RegexExamples()
        {
            var pattern = @"\w\s\W\S\D\d";

            var input = "s #ad0fq23r09as@#$@!asdf90nveeaA";

            var result = Regex.Match(input, pattern);

            Console.WriteLine(result.Value);
        }

        [Example("Assert Example", false)]
        public void AssertExamples()
        {
            var pattern = @"\w\ds\W\S\D\d";

            var input = "s #ad0fq23r09as@#$@!asdf90nveeaA";

            Debug.Assert(Regex.IsMatch(input, pattern), "Doesn't match the pattern");

            //Console.WriteLine(result.Value);
        }

        [Example("Preprocessor Example", false)]
        public void PreprocessorExamples()
        {
            Console.WriteLine("Start");


#if DEBUG
            Console.WriteLine("Test");

#else
            Console.WriteLine("No Test");
#endif

#if RELEASE
            Console.WriteLine("RELEASE");

#else
            Console.WriteLine("No RELEASE");
#warning Not in release mode
#endif
        }

        [Example("Trace Example", false)]
        public void TraceExamples()
        {
            var listener = new EventLogTraceListener("Valiation Examples");

            Trace.Listeners.Add(listener);
            Trace.AutoFlush = true;
            var traceStream = File.AppendText("c:\\TraceFile.txt");

            // Create a TextWriterTraceListener for the trace output file.
            TextWriterTraceListener traceListener =
                new TextWriterTraceListener(traceStream);
            Trace.Listeners.Add(traceListener);

            // Write a startup note into the trace file.
            Trace.WriteLine("Trace started " + DateTime.Now.ToString());

            Console.WriteLine("Traces");
            Trace.WriteLine("Validation Example Log");
            Console.ReadKey();
        }
    }
}
