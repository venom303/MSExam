using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examples.Interfaces
{
    public class InterfaceExamples
    {
        [Example("Comparable Example", false)]
        public void ComparableExample()
        {
            var zealot = new SC2Unit { Name = "Zealot" };
            var marine = new SC2Unit { Name = "Marine" };
            var viper = new SC2Unit { Name = "Viper" };

            var tab = new[] { zealot, marine, viper };

            Console.WriteLine("Before sort: ");

            foreach (var item in tab)
                Console.WriteLine(item.Name);

            Array.Sort(tab);

            Console.WriteLine("After sort: ");

            foreach (var item in tab)
                Console.WriteLine(item.Name);

            Console.ReadKey();
        }

        [Example("Comparable Generic Example", false)]
        public void ComparableGenericExample()
        {
            var zealot = new SC2Unit { Name = "Zealot" };
            var marine = new SC2Unit { Name = "Marine" };
            var viper = new SC2Unit { Name = "Viper" };

            var tab = new[] { zealot, marine, viper };

            Console.WriteLine("Before sort: ");

            foreach (var item in tab)
                Console.WriteLine(item.Name);

            Array.Sort<SC2Unit>(tab);

            Console.WriteLine("After sort: ");

            foreach (var item in tab)
                Console.WriteLine(item.Name);

            Console.ReadKey();
        }

        [Example("Comparer Generic Example", false)]
        public void ComparerGenericExample()
        {
            var zealot = new SC2Unit { Name = "Zealot", Damage = 20, Speed = 10 };
            var marine = new SC2Unit { Name = "Marine", Damage = 10, Speed = 10 };
            var viper = new SC2Unit { Name = "Viper", Damage = 0, Speed = 20 };

            var tab = new[] { zealot, marine, viper };

            var comparer = new UnitComparer();

            Console.WriteLine("Before sort: ");

            foreach (var item in tab)
                Console.WriteLine(item.Name);

            Console.WriteLine("Sort by Damage");
            comparer.SortBy = UnitComparer.CompareField.Damage;

            Array.Sort(tab, comparer);

            foreach (var item in tab)
                Console.WriteLine(item.Name);

            Console.WriteLine("Sort by Speed");
            comparer.SortBy = UnitComparer.CompareField.Speed;

            Array.Sort(tab, comparer);

            foreach (var item in tab)
                Console.WriteLine(item.Name);

            Console.ReadKey();
        }

        [Example("Equatable Example", false)]
        public void EquatableExample()
        {
            var zealot = new ProtossUnit { Name = "Zealot" };
            var oracle = new ProtossUnit { Name = "Oracle" };

            var list = new List<ProtossUnit> { zealot, oracle };

            Console.WriteLine(list.Contains(new ProtossUnit { Name = "Zealot" }));

            Console.WriteLine(list.Contains(new ProtossUnit { Name = "Dark Templar" }));

            Console.ReadKey();
        }

        [Example("Clonable Example", false)]
        public void ClonableExample()
        {
            var zealot = new ProtossUnit { Name = "Zealot", Damage=20 };

            var anotherZealot = (ProtossUnit)zealot.Clone();

            Console.WriteLine(anotherZealot.Damage);

            Console.ReadKey();
        }

        [Example("GetEnumerator Example", false)]
        public void GetEnumeratorExample()
        {
            var warpPrism = new WarpPrism();
            //
            warpPrism.Add(new ProtossUnit { Name = "Colosus", Size = 4 });
            warpPrism.Add(new ProtossUnit { Name = "Colosus", Size = 4 });

            foreach (var unit in warpPrism)
                Console.WriteLine(unit.ToString());


            var warpPrism2 = new WarpPrism();
            //
            warpPrism2.Add(new ProtossUnit { Name = "Zealot", Size = 2 });
            warpPrism2.Add(new ProtossUnit { Name = "Stalker", Size = 2 });
            warpPrism2.Add(new ProtossUnit { Name = "Stalker", Size = 2 });
            warpPrism2.Add(new ProtossUnit { Name = "Zealot", Size = 2 });

            foreach (var unit in warpPrism2)
                Console.WriteLine(unit.ToString());

            Console.ReadKey();
        }
    }
}
