using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace RemoteEmployeeService.Models
{
    public class EfDbConnect:DbContext
    {
        public EfDbConnect():base("")
        {

        }
        public DbSet<Employee> Employees { get; set; }
    }
}