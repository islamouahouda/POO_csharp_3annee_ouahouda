using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseManagementSystem.Tests
{
    public class Table : ITable
    {
        public Core.StructTable TableStructure { get; set; }
        public List<Row> Rows { get; set; }

        public Table()
        {
            Rows = new List<Row>();
        }

        public Row Select(int rowIndex)
        {
            if (rowIndex >= 0 && rowIndex < Rows.Count)
            {
                return Rows[rowIndex];
            }
            else
            {
                throw new ArgumentOutOfRangeException($"Invalid row index '{rowIndex}'.");
            }
        }

        public void Insert(Row row)
        {
            if (row.Values.Count == TableStructure.Fields.Count)
            {
                // Here you can add validation for the constraints and data types, if needed
                Rows.Add(row);
            }
            else
            {
                throw new ArgumentException($"The row does not have the correct number of values. Expected {TableStructure.Fields.Count}, got {row.Values.Count}.");
            }
        }

        public void Delete(int rowIndex)
        {
            if (rowIndex >= 0 && rowIndex < Rows.Count)
            {
                Rows.RemoveAt(rowIndex);
            }
            else
            {
                throw new ArgumentOutOfRangeException($"Invalid row index '{rowIndex}'.");
            }
        }

        public void Update(int rowIndex, Row newRow)
        {
            if (newRow.Values.Count == TableStructure.Fields.Count)
            {
                if (rowIndex >= 0 && rowIndex < Rows.Count)
                {
                    // Here you can add validation for the constraints and data types, if needed
                    Rows[rowIndex] = newRow;
                }
                else
                {
                    throw new ArgumentOutOfRangeException($"Invalid row index '{rowIndex}'.");
                }
            }
            else
            {
                throw new ArgumentException($"The row does not have the correct number of values. Expected {TableStructure.Fields.Count}, got {newRow.Values.Count}.");
            }
        }
    }

}
