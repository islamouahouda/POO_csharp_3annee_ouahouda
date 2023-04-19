using System;
using System.Collections.Generic;
using DatabaseManagementSystem.Core;

namespace DatabaseManagementSystem.Tests
{
    class Test1
    {
        
        static void test02()
        {
            Console.WriteLine("Creating a predefined StructTable...");
            StructTable structTable = new StructTable
            {
                TableName = "Users",
                Fields = new List<Field>
                {
                    new Field { Name = "Id", FieldType = TypeField.Integer, FieldConstraint = Constraint.PrimaryKey },
                    new Field { Name = "Name", FieldType = TypeField.Text, FieldConstraint = Constraint.None },
                    new Field { Name = "Age", FieldType = TypeField.Integer, FieldConstraint = Constraint.None }
                }
            };

            Console.WriteLine("Creating a Table with the predefined StructTable...");
            Table table = new Table { TableStructure = structTable };

            Console.WriteLine("Adding predefined rows to the Table...");
            table.Insert(new Row { Values = new List<string> { "1", "Alice", "30" } });
            table.Insert(new Row { Values = new List<string> { "2", "Bob", "25" } });
            table.Insert(new Row { Values = new List<string> { "3", "Kari", "17" } });

            Console.WriteLine("Creating a Database with the predefined data...");
            Database database = new Database
            {
                DatabaseName = "TestDB",
                Tables = new List<Table> { table }
            };

            Console.WriteLine("Saving the predefined database to an XML file...");
            string filePath = "D:\\Assets\\database.xml";
            database.Save(filePath);

            Console.WriteLine("Loading the predefined database from an XML file...");
            Database loadedDatabase = new Database();
            loadedDatabase.Load(filePath);

            Console.WriteLine("Executing SELECT query on the loaded predefined database...");
            List<Row> result = loadedDatabase.ExecuteQuery("SELECT * FROM Users");

            Console.WriteLine("\nSELECT * FROM Users (loaded predefined database):");
            foreach (Row row in result)
            {
                Console.WriteLine(string.Join(", ", row.Values));
            }
        }

        static void test01()
        {

            Console.WriteLine("Creating a StructTable...");

            StructTable structTable = new StructTable
            {
                TableName = "Users",
                Fields = new List<Field>
                {
                    new Field { Name = "Id", FieldType = TypeField.Integer, FieldConstraint = Constraint.PrimaryKey },
                    new Field { Name = "Name", FieldType = TypeField.Text, FieldConstraint = Constraint.None },
                    new Field { Name = "Age", FieldType = TypeField.Integer, FieldConstraint = Constraint.None }
                }
            };

            Console.WriteLine("Creating a Table with the StructTable...");
            Table table = new Table { TableStructure = structTable };

            Console.WriteLine("Adding rows to the Table...");
            table.Insert(new Row { Values = new List<string> { "1", "Oulfa", "30" } });
            table.Insert(new Row { Values = new List<string> { "2", "Boutaina", "25" } });
            table.Insert(new Row { Values = new List<string> { "3", "Karim", "17" } });
            Console.WriteLine();

            Console.WriteLine("Creating a Database...");
            Database database = new Database
            {
                DatabaseName = "TestDB",
                Tables = new List<Table> { table }
            };

            Console.WriteLine("Executing SELECT query... SQL (SELECT * FROM Users)");
            List<Row> result = database.ExecuteQuery("SELECT * FROM Users");
            foreach (Row row in result)
            {
                Console.WriteLine(string.Join(", ", row.Values));
            }
            Console.WriteLine();

            Console.WriteLine("\nExecuting UPDATE query... SQL (UPDATE Users SET Age = 35 WHERE Id = 1)");
            database.ExecuteUpdate("UPDATE Users SET Age = 35 WHERE Id = 1");
            Console.WriteLine();

            Console.WriteLine("Executing SELECT query... SQL (SELECT * FROM Users)");
            result = database.ExecuteQuery("SELECT * FROM Users");
            foreach (Row row in result)
            {
                Console.WriteLine(string.Join(", ", row.Values));
            }
            Console.WriteLine();

            Console.WriteLine("Executing DELETE query... SQL (DELETE FROM Users WHERE Id = 2)");
            database.ExecuteUpdate("DELETE FROM Users WHERE Id = 2");

            Console.WriteLine("Executing SELECT query... SQL (SELECT * FROM Users)");
            result = database.ExecuteQuery("SELECT * FROM Users");
            foreach (Row row in result)
            {
                Console.WriteLine(string.Join(", ", row.Values));
            }

            Console.WriteLine("Executing INSERT query... SQL (INSERT INTO Users (Id, Name, Age) VALUES (4, 'Charlie', 22))");
            database.ExecuteUpdate("INSERT INTO Users (Id, Name, Age) VALUES (4, 'Charlie', 22)");

            Console.WriteLine("\nExecuting SELECT query... SQL (SELECT * FROM Users) ");
            result = database.ExecuteQuery("SELECT * FROM Users");

            Console.WriteLine("\nUpdated data:");
            foreach (Row row in result)
            {
                Console.WriteLine(string.Join(", ", row.Values));
            }
        }
        static void Main(string[] args)
        {
            //test without save and export
            //test01();

            // export
            test02();
        }
    }
}
