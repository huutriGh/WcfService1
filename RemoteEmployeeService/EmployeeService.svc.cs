using RemoteEmployeeService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace RemoteEmployeeService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "EmployeeService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select EmployeeService.svc or EmployeeService.svc.cs at the Solution Explorer and start debugging.
    public class EmployeeService : IEmployeeService
    {
        EfDbConnect db = new EfDbConnect();
        public async Task<bool> CheckLogin(string employeeId, string password)
        {
            var ret = db.Employees.Where(emp => emp.EmployeeId == employeeId && emp.Password == password).FirstOrDefault();
            return ret != null;
           

            
        }

        public async Task<Employee> GetEmployee(string employeeId)
        {
            var ret = db.Employees.Where(emp => emp.EmployeeId == employeeId ).FirstOrDefault();
            return ret;
        }

        public Task<bool> PutEmployee(Employee employee)
        {
            throw new NotImplementedException();
        }
    }
}
