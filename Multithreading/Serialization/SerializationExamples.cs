using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace Examples.Serialization
{
    class SerializationExamples
    {
        private const string BinaryFilePath = @"C:\file.bin";
        private const string JsonFilePath = @"C:\file.json";
     
        [Example("BinarySerialization Example", false)]
        public void BinarySerializationExample()
        {
            var person = new Person
            {
                FirstName = "Roman",
                LastName = "Pokrywka"
            };

            person.SetId(10);

            var formatter = new BinaryFormatter();

            var stream = new FileStream(BinaryFilePath, FileMode.OpenOrCreate);
            formatter.Serialize(stream, person);
            stream.Close();
        }

        [Example("BinarySerialization Read Example", false)]
        public void BinarySerializationReadExample()
        {
            var formatter = new BinaryFormatter();
            var stream = new FileStream(BinaryFilePath, FileMode.OpenOrCreate);

            var person = formatter.Deserialize(stream) as Person;
            stream.Close();

            Console.ReadKey();
        }

        [Example("JsonSerialization Write/Read Example", false)]
        public void JsonSerializationReadExample()
        {
            var person = new Employee
            {
                FirstName = "Roman",
                LastName = "Pokrywka"
            };
            
            person.SetId(10);

            var serializer = new DataContractJsonSerializer(typeof(Employee));
            var stream = new FileStream(JsonFilePath, FileMode.OpenOrCreate);
            serializer.WriteObject(stream, person);
            
            stream.Close();

            var employee = (Employee)serializer.ReadObject(new FileStream(JsonFilePath, FileMode.Open));

            Console.ReadKey();
        }

        [Example("Custom Serialization Write/Read Example", false)]
        public void CustomSerializationReadExample()
        {
            var person = new Employee
            {
                FirstName = "Roman",
                LastName = "Pokrywka"
            };

            person.SetId(10);

            var serializer = new DataContractJsonSerializer(typeof(Employee));
            var stream = new FileStream(JsonFilePath, FileMode.OpenOrCreate);
            serializer.WriteObject(stream, person);

            stream.Close();

            Console.ReadKey();
        }

        [Example("ISerializable Example", false)]
        public void ISerializableExample()
        {
            var person = new Guy
            {
                FirstName = "Roman",
                LastName = "Pokrywka"
            };

            person.SetId(10);

            var serializer = new DataContractJsonSerializer(typeof(Guy));
            var stream = new FileStream(JsonFilePath, FileMode.Create);
            serializer.WriteObject(stream, person);

            stream.Close();

            var guy = serializer.ReadObject(new FileStream(JsonFilePath, FileMode.Open)) as Guy;

            Console.ReadKey();
        }
    }

    [DataContract]
    public class Employee
    {
        [DataMember]
        public string FirstName;
        [DataMember]
        public string LastName;
        [DataMember]
        private int _id;

        public void SetId(int id)
        {
            _id = id;
        }
    }

    [Serializable]
    public class Guy : ISerializable
    {
        public string FirstName;
        public string LastName;
        private int id;

        public void SetId(int id)
        {
            id = id;
        }

        public Guy() { }

        public Guy(SerializationInfo info, StreamingContext context)
        {
            FirstName = info.GetString("imie");
            LastName = info.GetString("nazwisko");
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("imie", FirstName);
            info.AddValue("nazwisko", LastName);
        }
    }

    [Serializable]
    public class Person
    {
        public string FirstName;
        [NonSerialized]
        public string LastName;
        private int _id;

        public void SetId(int id)
        {
            _id = id;
        }

        [OnSerializing()]
        internal void OnSerializingMethod(StreamingContext context)
        {
            FirstName = "Bob";
        }

        [OnSerialized()]
        internal void OnSerializedMethod(StreamingContext context)
        {
            FirstName = "Serialize Complete";
        }

        [OnDeserializing()]
        internal void OnDeserializingMethod(StreamingContext context)
        {
            FirstName = "John";
        }

        [OnDeserialized()]
        internal void OnDeserializedMethod(StreamingContext context)
        {
            FirstName = "Deserialize Complete";
        }
    }
}
