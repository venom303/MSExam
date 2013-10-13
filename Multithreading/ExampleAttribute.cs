using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examples
{
    public class ExampleAttribute : Attribute
    {
        public ExampleAttribute(string description, bool run = false)
        {
            Description = description;
            Run = run;
        }

        public bool Run { get; set; }
        public string Description { get; set; }
    }
}
