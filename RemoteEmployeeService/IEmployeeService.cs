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
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IEmployeeService" in both code and config file together.
    [ServiceContract]
    public interface IEmployeeService
    {
        [OperationContract]
        Task<bool> CheckLogin(string employeeId, string password);
        [OperationContract]
        Task<Employee> GetEmployee(string employeeId);
        [OperationContract]
        Task<bool> PutEmployee(Employee employee );
    }
}
