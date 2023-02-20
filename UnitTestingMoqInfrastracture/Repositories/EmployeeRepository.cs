using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitTestingMoqDomain.Entity;
using UnitTestingMoqDomain.Repositories;
using UnitTestingMoqInfrastracture.Data;

namespace UnitTestingMoqInfrastracture.Repositories
{
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(ApplicationContext context) : base(context)
        {
        }
    }
}
