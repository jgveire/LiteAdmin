﻿namespace LiteAdmin.Models
{
    public class ColumnModel
    {
        public ColumnModel()
        {
        }

        public ColumnModel(string name, string dataType, string defaultValue, bool isNullable, int maxLength)
        {
            DataType = dataType;
            DefaultValue = defaultValue;
            IsNullable = isNullable;
            MaxLength = maxLength;
            Name = name;
        }

        public string DataType { get; set; }

        public string DefaultValue { get; set; }

        public bool IsNullable { get; set; }

        public int MaxLength { get; set; }

        public string Name { get; set; }
    }
}