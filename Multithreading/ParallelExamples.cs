using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Multithreading
{
    class ParallelExamples
    {
        [Example("Classic For Example", false)]
        public void ClassicForExample()
        {
            for (int i = 0; i < 10; i++)
            {
                Thread.Sleep(1000);
                Console.WriteLine("Iteration {0}", i);
            }
        }

        [Example("Parallel For Example", false)]
        public void ParallelForExample()
        {
            Parallel.For(0, 10, i =>
            {
                Thread.Sleep(1000);
                Console.WriteLine("Iteration {0}", i);
            });
        }

        [Example("Classic ForEach Example", false)]
        public void ClassicForEachExample()
        {
            var numbers = Enumerable.Range(10, 10);
            
            foreach (int number in numbers)
            {
                Thread.Sleep(1000);
                Console.WriteLine("Current Number: {0}", number);
            }
        }

        [Example("Parallel ForEach Example", false)]
        public void ParallelForEachExample()
        {
            var numbers = Enumerable.Range(10, 30);

            Parallel.ForEach(numbers, number =>
            {
                Thread.Sleep(1000);
                Console.WriteLine("Current Number: {0}", number);
            });
        }

        [Example("Max Degree Of Parallelism Example", false)]
        public void MaxDegreeOfParallelismExample()
        {
            var numbers = Enumerable.Range(10, 30);

            Parallel.ForEach(numbers, new ParallelOptions { MaxDegreeOfParallelism = 2 }, number =>
            {
                FindPrimes(1000);
                Console.WriteLine("Current Number: {0}", number);
            });
        }

        private void FindPrimes(int num)
        {
            Enumerable.Range(0, num).Aggregate(
            Enumerable.Range(2, 1000000).ToList(),
            (result, index) =>
            {
                result.RemoveAll(i => i > result[index] && i % result[index] == 0);
                return result;
            });
        }
    }
}
