using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace DXample.Server.Interfaces
{
    [DataContract]
    public enum EmployeeType
    {
        [EnumMember]
        Developer,
        [EnumMember]
        SubLeader,
        [EnumMember]
        Leader,
        [EnumMember]
        ProjectManager
    }
}

