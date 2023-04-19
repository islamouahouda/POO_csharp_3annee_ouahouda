using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseManagementSystem.Core
{
    public class Field
    {
        public string Name { get; set; }
        public TypeField FieldType { get; set; }
        public Constraint FieldConstraint { get; set; }
    }
}
