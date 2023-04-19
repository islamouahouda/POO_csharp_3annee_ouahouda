using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseManagementSystem.Core
{
    public class StructTable : IStructTable
    {
        public string TableName { get; set; }
        public List<Field> Fields { get; set; }

        public StructTable()
        {
            Fields = new List<Field>();
        }

        public void AddField(Field field)
        {
            Fields.Add(field);
        }

        public void Describe()
        {
            Console.WriteLine($"Table: {TableName}");
            Console.WriteLine("Fields:");
            foreach (Field field in Fields)
            {
                Console.WriteLine($"- {field.Name}, {field.FieldType}, {field.FieldConstraint}");
            }
        }

        public Field FindField(string fieldName)
        {
            return Fields.FirstOrDefault(f => f.Name == fieldName);
        }

        public void RemoveField(string fieldName)
        {
            Field fieldToRemove = FindField(fieldName);
            if (fieldToRemove != null)
            {
                Fields.Remove(fieldToRemove);
            }
            else
            {
                throw new ArgumentException($"Field '{fieldName}' not found in table '{TableName}'.");
            }
        }

        public void UpdateField(string fieldName, Field updatedField)
        {
            Field fieldToUpdate = FindField(fieldName);
            if (fieldToUpdate != null)
            {
                fieldToUpdate.Name = updatedField.Name;
                fieldToUpdate.FieldType = updatedField.FieldType;
                fieldToUpdate.FieldConstraint = updatedField.FieldConstraint;
            }
            else
            {
                throw new ArgumentException($"Field '{fieldName}' not found in table '{TableName}'.");
            }
        }
    }

}
