using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using EmployeeApiProject.Models;

namespace EmployeeApiProject.Data
{
    public class EmployeeApiProjectContext : DbContext
    {
        public EmployeeApiProjectContext (DbContextOptions<EmployeeApiProjectContext> options)
            : base(options)
        {
        }

        public DbSet<EmployeeApiProject.Models.Employee> Employee { get; set; } = default!;
    }
}
