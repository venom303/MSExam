using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Examples.Database;

namespace Examples.ADO
{
    class EFExamples
    {
        [Example("Basic Example", true)]
        public void BasicExample()
        {

            var users = new Entities().MCC_User.ToList();

            Console.ReadKey();
        }
    }
}
