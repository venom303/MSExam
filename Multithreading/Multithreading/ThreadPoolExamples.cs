using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Examples
{
    public class ThreadPoolExamples
    {
        [Example("Thread pool example", false)]
        public void BasicExample()
        {
            
            ThreadPool.QueueUserWorkItem(new WaitCallback(Run), 10);
            
            Console.ReadKey();
        }

        private void Run(object state)
        {
            Console.WriteLine("A thread from thread pool");
            //Console.WriteLine(ThreadPool.GetMaxThreads(
        }
    }
}
