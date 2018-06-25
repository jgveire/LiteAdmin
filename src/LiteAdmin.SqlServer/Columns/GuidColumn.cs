namespace LiteAdmin.SqlServer.Columns
{
    using System;

    internal class GuidColumn : ColumnBase<Guid>
    {
        private readonly string _defaultValue;

        public GuidColumn(string name, bool isNullable, int maxLength, string defaultValue)
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
            //else if (_defaultValue.Equals("(newid())", StringComparison.OrdinalIgnoreCase))
            //{
            //    return Guid.NewGuid();
            //}

            return null;
        }
    }
}
