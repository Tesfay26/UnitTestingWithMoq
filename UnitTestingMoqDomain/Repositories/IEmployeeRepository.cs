using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitTestingMoqDomain.Entity;

namespace UnitTestingMoqDomain.Repositories
{
    public interface IEmployeeRepository : IGenericRepository<Employee>
    {
    }
}
