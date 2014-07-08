using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace DXample.Server.Interfaces
{
    [DataContract]
    public class Employee
    {
        [DataMember]
        public EmployeeType EmployeeType
        {
            get;
            internal set;
        }

        [DataMember]
        public string Name
        {
            get;
            internal set;
        }

        [DataMember]
        public int Age
        {
            get;
            internal set;
        }

        [DataMember]
        public decimal Salary
        {
            get;
            internal set;
        }
    }
}
