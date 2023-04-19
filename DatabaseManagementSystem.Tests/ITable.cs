using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseManagementSystem.Tests
{
    public interface ITable
    {
        Row Select(int rowIndex);
        void Insert(Row row);
        void Delete(int rowIndex);
        void Update(int rowIndex, Row newRow);
    }
}
