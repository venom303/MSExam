using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examples.Interfaces
{
    public class SC2Unit : IComparable, IComparable<SC2Unit>
    {
        public string Name { get; set; }
        public int Damage { get; set; }
        public int Speed { get; set; }

        public int CompareTo(object obj)
        {
            if (!(obj is SC2Unit))
                throw new ArgumentException("Object is not SC2Unit");

            var otherUnit = obj as SC2Unit;
            return Name.CompareTo(otherUnit.Name);
        }

        public override string ToString()
        {
            return Name;
        }

        public int CompareTo(SC2Unit other)
        {
            return other.Name.CompareTo(Name);
        }
    }
}
