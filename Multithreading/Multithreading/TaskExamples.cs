using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Examples
{
    class TaskExamples
    {
        [Example("Running Task", false)]
        public void RunningTask()
        {
            var task = new Task(() => Console.WriteLine("New Task"));
            task.Start();
            
            Console.WriteLine("Main Thread");
            Console.ReadKey();

            Task.Factory.StartNew(() => Console.WriteLine("New Task"));

            Console.WriteLine("Main Thread");
            Console.ReadKey();

            Task.Run(() => Console.WriteLine("New Task"));

            Console.WriteLine("Main Thread");
            Console.ReadKey();
        }

        [Example("Wait Example", false)]
        public void WaitExample()
        {
            var task = new Task(() =>
            {
                for (int i = 0; i < 100; i++)
                {
                    Console.Write("*");
                    Thread.Sleep(10);
                }
            });
                      
            task.Start();
            // equivalent to Thread.Join()
            // task.Wait();
        }

        [Example("Return Value Example", false)]
        public void ReturnValueExample()
        {
            var task = Task.Run(() =>
            {
                Thread.Sleep(3000);
                return 10;
            });
            
            // the call will block the current thread
            Console.WriteLine(task.Result);
            Console.ReadKey();
        }

        [Example("Continuation Example", false)]
        public void ContinuationExample()
        {
            var task = Task.Run(() =>
            {
                return 10;
            }).ContinueWith((i) =>
            {
                return i.Result * 4;
            }).ContinueWith((i)=>
            {
                return i.Result / 2;  
            });

            // the call will block the current thread
            Console.WriteLine(task.Result);
            Console.ReadKey();
        }

        #region << Continuation Example 2 >>

        [Example("Continuation Example 2", false)]
        public void ContinuationExample2()
        {
            var tokenSource = new CancellationTokenSource();

            var task = Task.Factory.StartNew(() =>
            {
                return 10;
            }, tokenSource.Token);
            task.ContinueWith((i) =>
            {
                throw new Exception();
                //tokenSource.Cancel();
                return i.Result * 4;
            })
            
            .ContinueWith((i) =>
            {
                Console.WriteLine("Canceled");
            }, TaskContinuationOptions.OnlyOnCanceled)
            .ContinueWith((i) =>
            {
                Console.WriteLine("Faulted");
            }, TaskContinuationOptions.OnlyOnFaulted)
             .ContinueWith((i) =>
             {
                 Console.WriteLine("Completed");
             }, TaskContinuationOptions.OnlyOnRanToCompletion)
            ;

            task.Wait();
            Console.ReadKey();
            return;

            var taskTest = Task.Factory.StartNew(() =>
            {
                System.Threading.Thread.Sleep(1000);

            }).ContinueWith((Task t) =>
            {
                Console.WriteLine("ERR");
            }, TaskContinuationOptions.OnlyOnFaulted)
            .ContinueWith((i) =>
            {
                Console.WriteLine("Canceled");
            }, TaskContinuationOptions.OnlyOnCanceled);

            try
            {
                taskTest.Wait();
            }
            catch (AggregateException ex)
            {
                foreach (var e in ex.InnerExceptions)
                    Console.WriteLine(e.Message + Environment.NewLine + e.StackTrace);
            }
            Console.ReadKey();
            return;
        }

        #endregion

        [Example("Task hierarchy Example", false)]
        public void TaskHierarchyExample()
        {

            var parent = Task.Factory.StartNew(() =>
            {
                Console.WriteLine("Parent Task started");
                var child = Task.Factory.StartNew(() =>
                    {
                        Thread.Sleep(3000);
                        Console.WriteLine("Child task attached");
                    }
                    //, TaskCreationOptions.AttachedToParent
                    );
            });

            parent.Wait();

            Console.WriteLine("All completed");

            Console.ReadKey();
        }

        [Example("Task factory with default values Example", false)]
        public void TaskFactoryExample()
        {
            var parent = Task.Factory.StartNew(() =>
            {
                Console.WriteLine("Parent Task started");
                var factory = new TaskFactory();
                //var factory = new TaskFactory(TaskCreationOptions.AttachedToParent, TaskContinuationOptions.None);
                
                factory.StartNew(() =>
                {
                    Thread.Sleep(1000);
                    Console.WriteLine("Child 1 task attached");
                });
                factory.StartNew(() =>
                {
                    Thread.Sleep(1000);
                    Console.WriteLine("Child 2 task attached");
                });
            });

            parent.Wait();

            Console.WriteLine("All completed");
            Console.ReadKey();
        }

        [Example("Wait All Example", false)]
        public void WaitAllExample()
        {
            var tasks = new Task[3];

            tasks[0] = Task.Run(() => { Console.WriteLine("Task 1"); Thread.Sleep(3000); });
            tasks[1] = Task.Run(() => { Console.WriteLine("Task 2"); Thread.Sleep(3000); });
            tasks[2] = Task.Run(() => { Console.WriteLine("Task 3"); Thread.Sleep(3000); });

            Task.WaitAll(tasks);
        }

        [Example("Wait Any Example", false)]
        public void WaitAnyExample()
        {
            var tasks = new List<Task<int>>();

            tasks.Add(Task.Run(() => { Console.WriteLine("Task 1 started"); Thread.Sleep(5000); return 1; }));
            tasks.Add(Task.Run(() => { Console.WriteLine("Task 2 started"); Thread.Sleep(1000); return 2; }));
            tasks.Add(Task.Run(() => { Console.WriteLine("Task 3 started"); Thread.Sleep(3000); return 3; }));

            while (tasks.Count > 0)
            {
                var taskIndex = Task.WaitAny(tasks.ToArray());
                Console.WriteLine("Task {0} completed", tasks[taskIndex].Result);
                tasks.RemoveAt(taskIndex);
            }

            Console.ReadKey();
        }

        [Example("Task canceling Example", false)]
        public void TaskCancelingExample()
        {
            var tokenSource = new CancellationTokenSource();

            new Task(() =>
            {
                while (!tokenSource.IsCancellationRequested)
                    Console.WriteLine("Test");
            }, tokenSource.Token).Start();

            Console.ReadKey();
            tokenSource.Cancel();
        }

        [Example("Task hierarchy Example", false)]
        public void TaskHierarchyExampleNotWorking()
        {

            var parent = Task.Run(() =>
            //var parent = Task.Factory.StartNew(() =>

                {
                    var results = new int[3];

                    Task.Factory.StartNew(() => { results[0] = 1; }, TaskCreationOptions.AttachedToParent);

                    Task.Factory.StartNew(() => results[1] = 2
                        , TaskCreationOptions.AttachedToParent
                        );
                    Task.Factory.StartNew(() => results[2] = 3
                        , TaskCreationOptions.AttachedToParent
                        );

                    return results;
                });

            parent.Wait();

            foreach (var i in parent.Result)
                Console.WriteLine(i);

            Console.ReadKey();
        }

        [Example("Excaption handling Example", false)]
        public void ExceptionHandlingExample()
        {
            try
            {
                var task = Task.Run(() =>
                {
                    throw new Exception();
                });
            }
            catch (Exception)
            {
                Console.WriteLine("Exception");
            }
            
            Console.ReadKey();
        }
    }
}
