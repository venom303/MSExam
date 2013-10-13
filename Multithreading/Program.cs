using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examples
{
    class Program
    {
        static void Main(string[] args)
        {
            //new ThreadExamples().IsBackroundExample();
            //Console.ReadKey();

            var list = typeof(Program).Assembly.GetTypes();
            //
            foreach (var type in list)
            {
                foreach (var method in type.GetMethods())
                {
                    var attributes = method.GetCustomAttributes(typeof(ExampleAttribute), false);
                    var settings = attributes.FirstOrDefault() as ExampleAttribute;
                    //
                    if (settings != null && settings.Run)
                    {
                        Console.WriteLine("---------------  Running: {0} ---------------", settings.Description);
                        var newObject = Activator.CreateInstance(type);
                        method.Invoke(newObject, null);
                        Console.WriteLine("---------------  End of {0} ---------------", settings.Description);
                        //Console.ReadKey();
                    }
                }
            }
        }
    }
}