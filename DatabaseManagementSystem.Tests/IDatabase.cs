using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseManagementSystem.Tests
{
    public interface IDatabase
    {
        Table FindTable(string tableName);
        void AddTable(Table table);
        void RemoveTable(string tableName);
        void UpdateTable(string tableName, Table updatedTable);
        void ShowTables();
        void Save(string filePath);
        void Load(string filePath);
        List<Row> ExecuteQuery(string sqlQuery);
        void ExecuteUpdate(string sqlQuery);
    }
}
