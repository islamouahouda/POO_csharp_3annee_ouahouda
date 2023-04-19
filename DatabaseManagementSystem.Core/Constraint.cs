using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseManagementSystem.Core
{
    public enum Constraint
    {
        None,
        PrimaryKey,
        NotNull,
        Null,
        Unique
    }

}
