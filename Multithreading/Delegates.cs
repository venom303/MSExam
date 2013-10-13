using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examples
{
    class DelegateHelper
    {
        public delegate void SomeAction();

        public SomeAction OnChange;

        public void Raise()
        {
            if (OnChange != null)
                OnChange();
        }
    }

    class EventHelper
    {
        public delegate void SomeAction(string toWrite);

        public event SomeAction OnChange = delegate { };

        public event EventHandler<WriteLineEventArgs> OnRun = delegate { };

        public void RaiseWrite(string toWrite)
        {
            OnRun(OnRun, new WriteLineEventArgs(toWrite));
        }

        public void Raise(string toWrite)
        {
            OnChange(toWrite);
        }
    }

    class WriteLineEventArgs : EventArgs
    {
        public WriteLineEventArgs(string toWrite)
        {
            ToWrite = toWrite;
        }

        public string ToWrite { get; set; }
    }

    class Delegates
    {
        //public Operation OnCalculate = null;
        public event Operation OnCalculate;

        public delegate int Operation(int x, int y);

        public Func<int, int, int> OperationFunction;

        [Example("InvokeEvents", false)]
        public void InvokeEventsExample()
        {
            var helper = new EventHelper();

            //helper.OnChange += Write1;
            helper.OnChange += (x) => Console.WriteLine(x);
            helper.OnChange += (x) => Console.WriteLine(x + " " + x);

            helper.Raise("Invoked");

            Console.ReadKey();
        }

        [Example("EventHandler", false)]
        public void EventHandlerExample()
        {
            var helper = new EventHelper();

            //helper.OnChange += Write1;
            helper.OnRun += (sender, args) => Console.WriteLine(args.ToWrite);

            helper.RaiseWrite("Event Handler Invoked");

            Console.ReadKey();
        }

     

        [Example("InvokeDeletages", false)]
        public void InvokeDeletagesExample()
        {
            var helper = new DelegateHelper();

            helper.OnChange = Write1;
            helper.OnChange += Write2;

            helper.OnChange();
            helper.Raise();

            Console.ReadKey();
        }

        [Example("InvokeDeletages2", false)]
        public void InvokeDeletages2Example()
        {
            var obj = new Delegates();

            obj.OnCalculate += Add;
            obj.OnCalculate += Substract;
            obj.OnCalculate += Multiply;
            obj.OnCalculate += Multiply;
            obj.OnCalculate -= Multiply;

            var result = obj.OnCalculate(2, 9);

            Console.WriteLine("Result: " + result);

            Console.ReadKey();
        }

        private void Write1()
        {
            Console.WriteLine("Test 1");
        }
        private void Write2()
        {
            Console.WriteLine("Test 2");
        }

        [Example("Deletages", false)]
        public void DelegatesExample()
        {
            var add = new Operation(Add);
            
            var res1 = add.Invoke(10, 20);
            var res2 = add(10, 10);

            Console.WriteLine(res1);
            Console.WriteLine(res2);

            Console.ReadKey();
        }

        [Example("Actions", false)]
        public void ActionsExample()
        {
            var addAction = new Action<int, int>((x, y) => Console.WriteLine(x + y));

            addAction(5, 7);

            Console.ReadKey();
        }

        [Example("Functors", false)]
        public void FunctorsExample()
        {
            OperationFunction = Substract;

            Console.WriteLine(OperationFunction(3, 1));
            WriteToConsole(9, 9, Multiply);
            WriteToConsole(9, 9, Substract);

            Console.ReadKey();
        }

        private void WriteToConsole(int x, int y, Func<int, int, int> operation)
        {
            Console.WriteLine(operation(x, y));
        }

        private int Add(int x, int y)
        {
            Console.WriteLine("Adding {0} and {1}", x, y);
            return x + y;
        }

        private int Substract(int x, int y)
        {
            Console.WriteLine("Substracting {0} and {1}", x, y);
            return x - y;
        }

        private int Multiply(int x, int y)
        {
            Console.WriteLine("Multiplying {0} and {1}", x, y);
            return x * y;
        }
    }
}
