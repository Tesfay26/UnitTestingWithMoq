using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitTestingMoqDomain.Entity;

namespace UnitTestingMoqInfrastracture.Data
{
    public class ApplicationContext : DbContext
    {
        DbSet<Employee> Employees { get; set; }
    }
}
