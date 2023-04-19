using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseManagementSystem.Core
{
    public interface IStructTable
    {
        Field FindField(string fieldName);
        void AddField(Field field);
        void RemoveField(string fieldName);
        void UpdateField(string fieldName, Field updatedField);
        void Describe();
    }
}
