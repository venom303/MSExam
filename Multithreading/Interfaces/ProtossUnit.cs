using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examples.Interfaces
{
    public class ProtossUnit : SC2Unit, IEquatable<ProtossUnit>, ICloneable
    {
        public sbyte Size { get; set; }

        public bool Equals(ProtossUnit other)
        {
            return other.Name.Equals(Name);
        }

        public object Clone()
        {
            var newUnit = new ProtossUnit();

            newUnit.Name = Name;
            newUnit.Speed = Speed;
            newUnit.Damage = Damage;

            return newUnit;
        }
    }
}
