using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examples.Exceptions
{
    class ExceptionExamples
    {
        [Example("Exception Example", false)]
        public void ExceptionExample()
        {
            try
            {
                Method1();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                Console.WriteLine("---------------------");
            }

            Console.ReadKey();
        }

        [Example("Rethrow e Exception Example", true)]
        public void RethrowEExceptionExample()
        {
            try
            {
                Method1();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                Console.WriteLine("---------------------");
                throw e;
            }

            Console.ReadKey();
        }

        [Example("Rethrow Exception Example", false)]
        public void RethrowExceptionExample()
        {
            try
            {
                Method1();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                Console.WriteLine("---------------------");
                throw;
            }

            Console.ReadKey();
        }

        [Example("Rethrow new InnerException Example", false)]
        public void RethrowNewInnerExceptionExample()
        {
            try
            {
                Method1();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                Console.WriteLine("---------------------");
                throw new Exception("", e);
            }

            Console.ReadKey();
        }

        [Example("Rethrow new Exception Example", false)]
        public void RethrowNewExceptionExample()
        {
            try
            {
                Method1();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                Console.WriteLine("---------------------");
                throw new Exception();
            }

            Console.ReadKey();
        }


        [Example("Exception without catch Example", true)]
        public void ExceptionWithoutCatchExample()
        {
            try
            {
                Console.WriteLine("Before Throwing");
                ThrowException();
                Console.WriteLine("After Throwing");
            }
            finally
            {
                Console.WriteLine("Finnaly block");
            }

            Console.ReadKey();
        }

        private void Method1()
        {
            Method2();
        }

        private void Method2()
        {
            Method3();
        }

        private void Method3()
        {
            ThrowException();
        }

        private void ThrowException()
        {
            throw new NotImplementedException();
        }
    }
}
