using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examples.Interfaces
{
    class UnitComparer : IComparer<SC2Unit>
    {
        public enum CompareField
        {
            Name,
            Damage,
            Speed
        }

        public CompareField SortBy = CompareField.Name;

        public int Compare(SC2Unit x, SC2Unit y)
        {
            switch (SortBy)
            {
                case CompareField.Name:
                    return x.Name.CompareTo(y.Name);
                case CompareField.Damage:
                    return x.Damage.CompareTo(y.Damage);
                case CompareField.Speed:
                    return x.Speed.CompareTo(y.Speed);
            }
            return x.Name.CompareTo(y.Name);
        }
    }
}
