using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Threading.Tasks;

namespace DXample.Server.Interfaces
{
    [ServiceContract]
    public interface IDataService
    {
        [OperationContract]
        IEnumerable<Employee> GetAllEmployees();
    }
}
