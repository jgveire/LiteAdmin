namespace LiteAdmin.SqlServer.Columns
{
    internal class CharColumn : ColumnBase<double>
    {
        private readonly string _defaultValue;

        public CharColumn(string name, bool isNullable, int maxLength, string defaultValue, bool isPrimaryKey)
            : base(name, isNullable, maxLength, isPrimaryKey)
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
                return char.Parse(_defaultValue.Substring(2, _defaultValue.Length - 4));
            }

            return null;
        }
    }
}
