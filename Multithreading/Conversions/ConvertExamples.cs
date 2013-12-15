using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examples.Conversions
{
    class ConvertExamples
    {
        [Example("UnderFlow Overflow Examples", false)]
        public void UnderFlowOverflowExamples()
        {
            checked
            {
                double largeFloat = -12E42;
                float smallFloat = (float)largeFloat;

                Console.WriteLine(largeFloat);
                Console.WriteLine(smallFloat);
            }
            //checked
            {

                int largeNum = 1261;
                byte smallNum = (byte)largeNum;

                Console.WriteLine(largeNum);
                Console.WriteLine(smallNum);
                Console.WriteLine(largeNum % 256);
            }
        }
    }
}
