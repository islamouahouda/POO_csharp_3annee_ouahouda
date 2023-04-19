using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseManagementSystem.Tests
{
    public class Row
    {
        public List<string> Values { get; set; }

        public Row()
        {
            Values = new List<string>();
        }
    }

}
