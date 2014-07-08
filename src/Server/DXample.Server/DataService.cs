using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Threading.Tasks;

using DXample.Server.Interfaces;

namespace DXample.Server
{
    [ServiceBehavior(InstanceContextMode=InstanceContextMode.PerCall, UseSynchronizationContext=false)]
    public class DataService : IDataService
    {
        public IEnumerable<Employee> GetAllEmployees()
        {
            return Enumerable.Range(1, DataServiceState.Instance.NumberOfEmployees)
                             .Select(_ => 
                             {
                                 return new Employee
                                 {
                                    EmployeeType = GetRandomEmployeeType(_),
                                    Name = String.Format("Employee{0}", _),
                                    Age = (_ + 20),
                                    Salary = (_ + 200000)
                                 };
                             });
        }

        internal EmployeeType GetRandomEmployeeType(int count)
        {
            if (count % 7 == 0)
            {
                return EmployeeType.ProjectManager;
            }

            if (count % 5 == 0)
            {
                return EmployeeType.Leader;
            }

            if (count % 3 == 0)
            {
                return EmployeeType.SubLeader;
            }

            return EmployeeType.Developer;
        }
    }

    public class DataServiceState
    {
        public static readonly DataServiceState Instance = new DataServiceState();

        internal DataServiceState()
        {
            NumberOfEmployees = 100;
        }

        public int NumberOfEmployees
        {
            get;
            set;
        }
    }
}
