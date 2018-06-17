namespace LiteAdmin.SqlServer.Columns
{
    internal class FloatColumn : ColumnBase<float>
    {
        private readonly string _defaultValue;

        public FloatColumn(string name, bool isNullable, int maxLength, string defaultValue)
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
                return float.Parse(_defaultValue.Substring(2, _defaultValue.Length - 4));
            }

            return null;
        }
    }
}
