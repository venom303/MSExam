using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examples.Collections
{
    class CollectionExamples
    {
        [Example("Collections example", false)]
        public void Collections()
        {
            int[,] tab = new int[2, 2];
            tab[0,1] = 10;

            Console.WriteLine(tab.Rank);

            Console.WriteLine(tab.GetType().GetArrayRank());

            Console.ReadKey();
        }
    }
}
