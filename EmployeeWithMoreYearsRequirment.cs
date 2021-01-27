using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthenAndAutho
{
    public class EmployeeWithMoreYearsRequirment:IAuthorizationRequirement
    {
        public int Years { get; set; }
        public EmployeeWithMoreYearsRequirment(int years)
        {
            Years = years;
        }
    }
}
