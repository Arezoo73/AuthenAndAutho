using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthenAndAutho
{
    public class EmployeeNumberOfYears : IEmployeeNumberOfYears
    {
        public int Get(string name)
        {
            if (name == "arezoo")
            {
                return 21;
            }
            return 10;
        }
    }
}
