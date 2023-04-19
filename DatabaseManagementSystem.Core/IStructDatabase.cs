using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseManagementSystem.Core
{
    public interface IStructDatabase
    {
        StructTable FindTable(string tableName);
        void AddTable(StructTable table);
        void RemoveTable(string tableName);
        void UpdateTable(string tableName, StructTable updatedTable);
        void ShowTables();
        void Save(string filePath);
        void Load(string filePath);
    }

}
