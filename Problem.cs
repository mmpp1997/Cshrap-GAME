using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OTTER
{
    public class Problem:Exception
    {
        public static string poruka = "Niste dobro Odabrali";
        public Problem()
            :base(poruka)
        {
            
        }
    }
}
