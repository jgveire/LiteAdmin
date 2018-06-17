namespace LiteAdmin.SqlServer
{
    using System;
    using System.Collections.Generic;

    public class DataTypeDictionary : Dictionary<string, Type>
    {
        public DataTypeDictionary()
        {
            InitDictionary();
        }

        private void InitDictionary()
        {
            Add("bigint", typeof(Int64));
            Add("numeric", typeof(decimal));
            Add("bit", typeof(byte));
            Add("smallint", typeof(Int16));
            Add("decimal", typeof(decimal));
            Add("smallmoney", typeof(decimal));
            Add("int", typeof(Int32));
            Add("tinyint", typeof(sbyte));
            Add("money", typeof(decimal));
            Add("float", typeof(float));
            Add("real", typeof(double));
            Add("date", typeof(DateTime));
            Add("datetime2", typeof(DateTime));
            Add("smalldatetime", typeof(DateTime));
            Add("datetime", typeof(DateTime));
            Add("time", typeof(DateTime));
            Add("char", typeof(char));
            Add("varchar", typeof(string));
            Add("text", typeof(string));
            Add("nchar", typeof(char));
            Add("nvarchar", typeof(string));
            Add("ntext", typeof(string));
            Add("uniqueidentifier", typeof(Guid));

            // Not supported
            //Add("datetimeoffset", typeof(TimeSpan));
            //Add("binary", typeof(byte[]));
            //Add("varbinary", typeof(byte[]));
            //Add("image", typeof(byte[]));
            //Add("rowversion", typeof(byte[]));
            //Add("hierarchyid", typeof(string));
            //Add("sql_variant", typeof(object));
            //Add("xml", typeof(string));
        }
    }
}
