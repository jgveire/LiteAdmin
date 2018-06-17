namespace LiteAdmin.SqlServer.Columns
{
    using System;

    internal class Int64Column : ColumnBase<Int64>
    {
        private readonly string _defaultValue;

        public Int64Column(string name, bool isNullable, int maxLength, string defaultValue)
            : base(name, isNullable, maxLength)
        {
            _defaultValue = defaultValue;
        }

        public override object DefaultValue => GetDefaultValue();

        private object GetDefaultValue()
        {
            if (string.IsNullOrEmpty(_defaultValue))
            {
                return null;
            }
            else if (_defaultValue.Length > 4)
            {
                return long.Parse(_defaultValue.Substring(2, _defaultValue.Length - 4));
            }

            return null;
        }
    }
}
