using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseManagementSystem.Core
{
    using System;
    using System.IO;
    using System.Runtime.Serialization.Formatters.Binary;

    public class StructDatabase : IStructDatabase
    {
        public string DatabaseName { get; set; }
        public List<StructTable> Tables { get; set; }

        public StructDatabase()
        {
            Tables = new List<StructTable>();
        }

        public void AddTable(StructTable table)
        {
            Tables.Add(table);
        }

        public StructTable FindTable(string tableName)
        {
            return Tables.FirstOrDefault(t => t.TableName == tableName);
        }

        public void Load(string filePath)
        {
            if (File.Exists(filePath))
            {
                using (FileStream fileStream = new FileStream(filePath, FileMode.Open))
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    StructDatabase loadedDatabase = (StructDatabase)formatter.Deserialize(fileStream);
                    DatabaseName = loadedDatabase.DatabaseName;
                    Tables = loadedDatabase.Tables;
                }
            }
            else
            {
                throw new FileNotFoundException($"File '{filePath}' not found.");
            }
        }

        public void RemoveTable(string tableName)
        {
            StructTable tableToRemove = FindTable(tableName);
            if (tableToRemove != null)
            {
                Tables.Remove(tableToRemove);
            }
            else
            {
                throw new ArgumentException($"Table '{tableName}' not found in database '{DatabaseName}'.");
            }
        }

        public void Save(string filePath)
        {
            using (FileStream fileStream = new FileStream(filePath, FileMode.Create))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(fileStream, this);
            }
        }

        public void ShowTables()
        {
            Console.WriteLine($"Database: {DatabaseName}");
            Console.WriteLine("Tables:");
            foreach (StructTable table in Tables)
            {
                Console.WriteLine($"- {table.TableName}");
            }
        }

        public void UpdateTable(string tableName, StructTable updatedTable)
        {
            StructTable tableToUpdate = FindTable(tableName);
            if (tableToUpdate != null)
            {
                tableToUpdate.TableName = updatedTable.TableName;
                tableToUpdate.Fields = updatedTable.Fields;
            }
            else
            {
                throw new ArgumentException($"Table '{tableName}' not found in database '{DatabaseName}'.");
            }
        }
    }

}
