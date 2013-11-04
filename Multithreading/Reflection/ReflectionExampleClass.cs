using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examples.Reflection
{
    class ReflectionExampleClass
    {
        public ReflectionExampleClass(int number)
        {

        }

        private string _privateField = "Hello";
        public string _publicField = "Goodbye";
        internal string _internalfield = "Hola";
        protected string _protectedField = "Adios";
        static string _staticField = "Bonjour";

        public T GenericRepeat<T>(T text)
        {
            return text;
        }

        public string Repeat(int text)
        {
            return text.ToString();
        }

        public string Repeat(string text)
        {
            return text;
        }

        public string Repeat(string text1, string text2)
        {
            return text1 + text2;
        }
    }
}
