using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;

namespace DatabaseManagementSystem.Tests
{
    public class Database : IDatabase
    {
        public string DatabaseName { get; set; }
        public List<Table> Tables { get; set; }

        public Database()
        {
            Tables = new List<Table>();
        }

        public Table FindTable(string tableName)
        {
            return Tables.FirstOrDefault(table => table.TableStructure.TableName == tableName);
        }

        public void AddTable(Table table)
        {
            if (FindTable(table.TableStructure.TableName) == null)
            {
                Tables.Add(table);
            }
            else
            {
                throw new ArgumentException($"Table with name '{table.TableStructure.TableName}' already exists.");
            }
        }

        public void RemoveTable(string tableName)
        {
            var table = FindTable(tableName);
            if (table != null)
            {
                Tables.Remove(table);
            }
            else
            {
                throw new ArgumentException($"Table with name '{tableName}' not found.");
            }
        }

        public void UpdateTable(string tableName, Table updatedTable)
        {
            var table = FindTable(tableName);
            if (table != null)
            {
                table.TableStructure = updatedTable.TableStructure;
                table.Rows = updatedTable.Rows;
            }
            else
            {
                throw new ArgumentException($"Table with name '{tableName}' not found.");
            }
        }

        public void ShowTables()
        {
            foreach (var table in Tables)
            {
                Console.WriteLine(table.TableStructure.TableName);
            }
        }

        public void Save(string filePath)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Database));
            using (FileStream fileStream = new FileStream(filePath, FileMode.Create))
            {
                serializer.Serialize(fileStream, this);
            }
        }

        public void Load(string filePath)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Database));
            using (FileStream fileStream = new FileStream(filePath, FileMode.Open))
            {
                Database loadedDatabase = (Database)serializer.Deserialize(fileStream);
                DatabaseName = loadedDatabase.DatabaseName;
                Tables = loadedDatabase.Tables;
            }
        }

        public List<Row> ExecuteQuery(string sqlQuery)
        {
            // Basic validation and extraction of table name
            Regex regex = new Regex(@"SELECT\s+(.*?)\s+FROM\s+(\w+)", RegexOptions.IgnoreCase);
            Match match = regex.Match(sqlQuery);
            if (!match.Success)
            {
                throw new ArgumentException("Invalid SQL SELECT query");
            }

            string tableName = match.Groups[2].Value;

            // Find the table in the database
            Table table = Tables.FirstOrDefault(t => t.TableStructure.TableName.Equals(tableName, StringComparison.OrdinalIgnoreCase));
            if (table == null)
            {
                throw new ArgumentException($"Table '{tableName}' not found in the database");
            }

            // This simplified implementation returns all rows without any filtering or sorting
            return table.Rows;
        }

        public void ExecuteUpdate(string sqlQuery)
        {
            // Basic validation and extraction of action type and table name
            Regex regex = new Regex(@"^(INSERT|DELETE|UPDATE)\s+((?:INTO|FROM)?\s*\w+)\s+(?:SET|VALUES|WHERE)?.*?$", RegexOptions.IgnoreCase);
            Match match = regex.Match(sqlQuery);
            if (!match.Success)
            {
                throw new ArgumentException("Invalid SQL query");
            }

            string action = match.Groups[1].Value.ToUpper();
            string tableName = match.Groups[2].Value.Replace("INTO ", "").Replace("FROM ", "");

            // Find the table in the database
            Table table = Tables.FirstOrDefault(t => t.TableStructure.TableName.Equals(tableName, StringComparison.OrdinalIgnoreCase));
            if (table == null)
            {
                throw new ArgumentException($"Table '{tableName}' not found in the database");
            }

            switch (action)
            {
                case "INSERT":
                    // Implement basic INSERT logic here
                    Regex insertRegex = new Regex(@"^INSERT\s+INTO\s+(.*?)\s*\((.*?)\)\s*VALUES\s*\((.*?)\)\s*$", RegexOptions.IgnoreCase);
                    Match insertMatch = insertRegex.Match(sqlQuery);
                    if (!insertMatch.Success)
                    {
                        throw new ArgumentException("Invalid INSERT SQL query");
                    }

                    string columnsString = insertMatch.Groups[2].Value;
                    string valuesString = insertMatch.Groups[3].Value;

                    var columns = columnsString.Split(',').Select(column => column.Trim()).ToList();
                    var values = valuesString.Split(',').Select(value => value.Trim()).ToList();

                    if (columns.Count != values.Count)
                    {
                        throw new ArgumentException("The number of columns and values do not match");
                    }

                    Row newRow = new Row();
                    for (int i = 0; i < columns.Count; i++)
                    {
                        string insertColumnName = columns[i];
                        string value = values[i];

                        int insertColumnIndex = table.TableStructure.Fields.FindIndex(field => field.Name.Equals(insertColumnName, StringComparison.OrdinalIgnoreCase));
                        if (insertColumnIndex == -1)
                        {
                            throw new ArgumentException($"Column '{insertColumnName}' not found in the table '{tableName}'");
                        }

                        newRow.Values.Add(value);
                    }

                    table.Rows.Add(newRow);
                    break;


                case "DELETE":
                    // Implement basic DELETE logic with WHERE clause
                    Regex whereRegex = new Regex(@"^DELETE\s+FROM\s+(.*?)\s+WHERE\s+(.*?)\s*=\s*(.*?)$", RegexOptions.IgnoreCase);
                    Match whereMatch = whereRegex.Match(sqlQuery);
                    if (!whereMatch.Success)
                    {
                        throw new ArgumentException("Invalid DELETE SQL query");
                    }

                    string columnName = whereMatch.Groups[2].Value; // Extract the column name from the WHERE clause
                    string columnValue = whereMatch.Groups[3].Value; // Extract the column value from the WHERE clause

                    int columnIndex = -1;
                    for (int i = 0; i < table.TableStructure.Fields.Count; i++)
                    {
                        if (table.TableStructure.Fields[i].Name.Equals(columnName, StringComparison.OrdinalIgnoreCase))
                        {
                            columnIndex = i;
                            break;
                        }
                    }

                    if (columnIndex == -1)
                    {
                        throw new ArgumentException($"Column '{columnName}' not found in the table '{tableName}'");
                    }

                    table.Rows.RemoveAll(row => row.Values[columnIndex] == columnValue);
                    break;


                case "UPDATE":
                    // Implement basic UPDATE logic with SET and WHERE clause
                    Regex updateRegex = new Regex(@"^UPDATE\s+(.*?)\s+SET\s+(.*?)\s+WHERE\s+(.*?)\s*=\s*(.*?)$", RegexOptions.IgnoreCase);
                    Match updateMatch = updateRegex.Match(sqlQuery);
                    if (!updateMatch.Success)
                    {
                        throw new ArgumentException("Invalid UPDATE SQL query");
                    }

                    string setClause = updateMatch.Groups[2].Value;
                    string whereColumnName = updateMatch.Groups[3].Value;
                    string whereColumnValue = updateMatch.Groups[4].Value;

                    // Extract column-value pairs from the SET clause
                    var setPairs = setClause.Split(',')
                        .Select(pair => pair.Split('='))
                        .Select(pair => new { Column = pair[0].Trim(), Value = pair[1].Trim() })
                        .ToList();

                    // Find the row to update based on the WHERE clause
                    int whereColumnIndex = table.TableStructure.Fields.FindIndex(field => field.Name.Equals(whereColumnName, StringComparison.OrdinalIgnoreCase));
                    if (whereColumnIndex == -1)
                    {
                        throw new ArgumentException($"Column '{whereColumnName}' not found in the table '{tableName}'");
                    }

                    Row rowToUpdate = table.Rows.FirstOrDefault(row => row.Values[whereColumnIndex] == whereColumnValue);
                    if (rowToUpdate == null)
                    {
                        throw new ArgumentException($"No row found with the value '{whereColumnValue}' in column '{whereColumnName}'");
                    }

                    // Update the row with the new values
                    foreach (var pair in setPairs)
                    {
                        int setColumnIndex = table.TableStructure.Fields.FindIndex(field => field.Name.Equals(pair.Column, StringComparison.OrdinalIgnoreCase));
                        if (setColumnIndex == -1)
                        {
                            throw new ArgumentException($"Column '{pair.Column}' not found in the table '{tableName}'");
                        }

                        rowToUpdate.Values[setColumnIndex] = pair.Value;
                    }
                    break;



                default:
                    throw new NotImplementedException("Unsupported SQL query");
            }
        }

    }

}
