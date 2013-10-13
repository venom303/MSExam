using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Examples
{
    public class ThreadExamples
    {
       

        private void WriteInfo(int iterations, string text)
        {
            for (int i = 0; i < iterations; i++) 
                Console.WriteLine(text);
        }

        private static int Thread1Counter = 0;
        private static int Thread2Counter = 0;


        [Example("Priority")]
        public void PriorityExample()
        {
            var thread1 = new Thread(() => { for (; ; ) Interlocked.Increment(ref Thread1Counter); });
            var thread2 = new Thread(() => { for (; ; ) Interlocked.Increment(ref Thread2Counter); });

            thread1.Priority = ThreadPriority.Lowest;
            thread2.Priority = ThreadPriority.Highest;

            thread1.IsBackground = true;
            thread2.IsBackground = true;

            thread2.Start();
            thread1.Start();

            var mainThread = new Thread(() => Thread.Sleep(1000));
            mainThread.Start();
            mainThread.Join();

            Console.WriteLine("Thread 1: {0}", Thread1Counter);
            Console.WriteLine("Thread 2: {0}", Thread2Counter);
        }

        public void ThreadMethod()
        {
            for (int i = 0; i < 30; i++)
            {
                Console.WriteLine("Iteration: {0}", i);
                Thread.Sleep(100);
            }
        }

        public void ThreadMethod(object count)
        {
            var number = (int)count;

            for (int i = 0; i < number; i++)
            {
                Console.WriteLine("Iteration: {0}", i);
                Thread.Sleep(100);
            }
        }

        [Example("IsBackground property", false)]
        public void IsBackroundExample()
        {
            // starting a thread - a ThreadStart delegate is passed to the constructor of the Thread class
            var thread = new Thread(new ThreadStart(ThreadMethod));
            thread.IsBackground = true;
            thread.Start();

            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine("Main Thread");
                Thread.Sleep(100);
            }
        }

        [Example("IsBackground property with parametrized start")]
        public void IsBackroundExample2()
        {
            var thread = new Thread(new ParameterizedThreadStart(ThreadMethod));
            thread.IsBackground = false;
            thread.Start(20);

            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine("Main Thread");
                Thread.Sleep(100);
            }
        }

        [Example("Stopping a thread")]
        public void StoppingAThreadExample()
        {
            var stopped = false;

            var thread = new Thread(() =>
                {
                    while (!stopped)
                    {
                        Console.WriteLine("Running...");
                        Thread.Sleep(100);
                    }
                });
            thread.Start();
            Console.ReadKey();
            stopped = true;
        }

        [ThreadStatic]
        private static int StaticCounter;

        [Example("ThreadStatic attribute example", false)]
        public void ThreadStaticExample()
        {
            new Thread(() =>
                {
                    for (int i = 0; i < 10; i++)
                    {
                        StaticCounter++;
                        Console.WriteLine("Thread 1. Static Counter Value: {0}", StaticCounter);
                    }
                }).Start();

            new Thread(() =>
            {
                for (int i = 0; i < 10; i++)
                {
                    StaticCounter++;
                    Console.WriteLine("Thread 2. Static Counter Value: {0}", StaticCounter);
                }
            }).Start();

            Console.ReadKey();
        }

        private static ThreadLocal<int> ThreadIndex = new ThreadLocal<int>(() =>
            {
                return Thread.CurrentThread.ManagedThreadId;
            });

        [Example("ThreadLocal<T> Example")]
        public void ThreadLocalExample()
        {
            new Thread(() =>
                {
                    for (int i = 0; i < ThreadIndex.Value; i++)
                    {
                        Console.WriteLine("Thread 1. Iteration: {0}", i);
                    }
                }).Start();

            new Thread(() =>
            {
                for (int i = 0; i < ThreadIndex.Value; i++)
                {
                    Console.WriteLine("Thread 2. Iteration: {0}", i);
                }
            }).Start();

            Console.ReadKey();
        }

        private void ThrowException()
        {
            throw new Exception();
        }

        [Example("Exception example", false)]
        public void ExceptionExample()
        {
            try
            {
                new Thread(ThrowException).Start();
            }
            catch (Exception)
            {
                Console.WriteLine("Exception");
            }
        }
    }
}
