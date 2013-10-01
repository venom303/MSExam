using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Multithreading
{
    class PLINQ
    {
        [Example("Simple PLINQ example", false)]
        public void SimpleExample()
        {
            var numbers = Enumerable.Range(0, 10);

            var result = numbers.AsParallel().AsUnordered().WithExecutionMode(ParallelExecutionMode.ForceParallelism).Where(i => i % 2 == 0).ToArray();

            foreach (var i in result)
                Console.WriteLine(i);

            Console.ReadKey();
        }

        [Example("Ordered PLINQ example ", false)]
        public void SimpleExampleOrdered()
        {
            var numbers = Enumerable.Range(0, 10);

            var result = numbers.AsParallel().AsOrdered().Where(i => i % 2 == 0).ToArray();

            foreach (var i in result)
                Console.WriteLine(i);

            Console.ReadKey();
        }

        [Example("AsSequential PLINQ example ", false)]
        public void AsSequentialExample()
        {
            var numbers = Enumerable.Range(0, 10);

            var result = numbers.AsParallel().AsOrdered().Where(i => i % 2 == 0).AsSequential();

            foreach (var i in result.Take(5))
                Console.WriteLine(i);

            Console.ReadKey();
        }

        [Example("ForAll PLINQ example ", false)]
        public void ForAllExample()
        {
            var numbers = Enumerable.Range(0, 10);

            var result = numbers.AsParallel().AsOrdered().Where(i => i % 2 == 0);

            result.ForAll(param => Console.WriteLine(param));

            Console.ReadKey();
        }

        [Example("Exception Handling PLINQ example ", false)]
        public void HandleExceptionExample()
        {
            var numbers = Enumerable.Range(0, 20);

            try
            {
                var result = numbers.AsParallel().AsOrdered().Where(i => IsEven(i));

                result.ForAll(param => Console.WriteLine(param));
            }
            catch (AggregateException ae)
            {
                Console.WriteLine("{0} Exception were thrown", ae.InnerExceptions.Count);
            }

            Console.ReadKey();
        }

        private bool IsEven(int i)
        {
            if (i % 10 == 0) throw new ArgumentException(i.ToString());
            return i % 2 == 0;
        }
    }
}
