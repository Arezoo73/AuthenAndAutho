using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AuthenAndAutho
{
    public class EmployeeWithMoreYearsHandler : AuthorizationHandler<EmployeeWithMoreYearsRequirment>
    {
        private readonly IEmployeeNumberOfYears _employeeNumberOfYears;

        public EmployeeWithMoreYearsHandler(IEmployeeNumberOfYears employeeNumberOfYears)
        {
            _employeeNumberOfYears = employeeNumberOfYears;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, EmployeeWithMoreYearsRequirment requirement)
        {
            if (!context.User.HasClaim(c => c.Type == ClaimTypes.Name))
            {
                return Task.CompletedTask;
            }
            var name = context.User.FindFirst(c => c.Type == ClaimTypes.Name);
            var yearsofExperience = _employeeNumberOfYears.Get(name.Value);
            if (yearsofExperience >= requirement.Years)
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
