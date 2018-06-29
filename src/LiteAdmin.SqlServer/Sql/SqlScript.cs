namespace LiteAdmin.SqlServer.Sql
{
    using System.IO;

    internal static class SqlScript
    {
        public static string Schema => GetResource("Schema.sql");

        private static string GetResource(string fileName)
        {
            var type = typeof(SqlScript);
            var assembly = type.Assembly;
            var resourceName = $"{type.Namespace}.{fileName}";

            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            using (StreamReader reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
            }
        }
    }
}
