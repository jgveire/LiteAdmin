namespace LiteAdmin.SqlServer.Columns
{
    using System;

    internal class DecimalColumn : ColumnBase<Guid>
    {
        private readonly string _defaultValue;

        public DecimalColumn(string name, bool isNullable, int maxLength, string defaultValue)
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
                return decimal.Parse(_defaultValue.Substring(2, _defaultValue.Length - 4));
            }

            return null;
        }
    }
}
