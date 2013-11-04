using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Examples.Reflection
{
    class ReflectionExamples
    {
        [Example("Quick Look", false)]
        public void QuickLook()
        {
            var assembly = this.GetType().Assembly;
        }

        [Example("Load Assembly", false)]
        public void LoadAssemblyExample()
        {
            var assembly = Assembly.Load("System.Data, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089");

            Console.WriteLine(assembly.CodeBase);
            Console.WriteLine(assembly.FullName);
            Console.WriteLine(assembly.GlobalAssemblyCache);
            Console.WriteLine(assembly.ImageRuntimeVersion);
            Console.WriteLine(assembly.Location);

            Console.ReadKey();
        }

        [Example("Get Assembly types", false)]
        public void GetTypesExample()
        {
            var assembly = Assembly.GetExecutingAssembly();

            var types = assembly.GetTypes();

            foreach (var type in types)
            {
                Console.WriteLine(type.Name);
            }

            Console.WriteLine("Modules: ");

            foreach (var type in assembly.GetModules(true))
            {
                Console.WriteLine(type.Name);
            }

            Console.ReadKey();
        }

        [Example("Create Instance Example", false)]
        public void CreateInstanceExample()
        {

            Assembly myAssembly = Assembly.Load("System.Data, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089");

            dynamic dataTable = myAssembly.CreateInstance("System.Data.DataTable");

            Console.WriteLine("Number of rows: {0}", dataTable.Rows.Count);

            Console.ReadKey();
        }

        [Example("Referencing Assemblies Example", false)]
        public void ReferencingAssembliesExample()
        {
            var assembly = Assembly.GetExecutingAssembly();

            var list = assembly.GetReferencedAssemblies();

            foreach (var item in list)
                Console.WriteLine(item.FullName);

            Console.ReadKey();
        }

        [Example("Enum Example", false)]
        public void EnumExample()
        {
            var type = typeof(Cards);
            Console.WriteLine(type.IsEnum);
            Console.WriteLine(type.IsEnumDefined(Cards.Clubs));
            Console.WriteLine(type.IsEnumDefined(2));
            Console.WriteLine(type.IsEnumDefined(99));

            Console.WriteLine();

            Console.WriteLine(type.GetEnumName(2));
            Console.WriteLine(type.GetEnumName(Cards.Spades));

            Console.WriteLine();

            foreach (var val in type.GetEnumValues())
                Console.WriteLine((int)val);

            Console.WriteLine();

            foreach (var val in type.GetEnumNames())
                Console.WriteLine(val.ToString());

            Console.ReadKey();
        }

        [Example("Get Fields Example", false)]
        public void GetFieldsExample()
        {
            var type = typeof(ReflectionExampleClass);


            Console.WriteLine(type.GetFields()[0].Name);

            Console.WriteLine(type.GetFields(BindingFlags.Instance|BindingFlags.NonPublic|BindingFlags.Public|BindingFlags.Static).Length);

            Console.ReadKey();
        }

        [Example("Invoke method Example", false)]
        public void InvokeExample()
        {
            var type = Assembly.GetExecutingAssembly().GetTypes().First(t => t.Name.Contains("ReflectionExampleClass"));

            var exampleObj = Activator.CreateInstance(type, 10);

            var methodInfo = type.GetMethod("GenericRepeat");
            methodInfo = methodInfo.MakeGenericMethod(typeof(string));
            
            Console.WriteLine(methodInfo.Invoke(exampleObj, new[] { "Papug"}));

            Console.ReadKey();
        }

        [Example("Generic Example", false)]
        public void GenericExample()
        {
            var type = typeof(List<>);

            type = type.MakeGenericType(typeof(int));

            var list = Activator.CreateInstance(type);

            list.GetType().InvokeMember("Add", BindingFlags.InvokeMethod | BindingFlags.Instance | BindingFlags.Public, null, list, new object[] { 10 });
            list.GetType().InvokeMember("Add", BindingFlags.InvokeMethod | BindingFlags.Instance | BindingFlags.Public, null, list, new object[] { 20 });

            var indexer = list.GetType().GetProperties().Where(p => p.GetIndexParameters().Any()).FirstOrDefault();

            Console.WriteLine(indexer.GetValue(list, new object[] { 1 }));

            Console.ReadKey();
        }
    }
}
