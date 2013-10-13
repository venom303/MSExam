using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examples.ObjectLifeCycle
{
    class LifeCycleExamples
    {
        [Example("Disposable Example", false)]
        public void DisposableClassExample()
        {
            using (new DisposableClass())
            {
            }
            Console.WriteLine("---------------");
            new DisposableClass();
        }
    }
}
