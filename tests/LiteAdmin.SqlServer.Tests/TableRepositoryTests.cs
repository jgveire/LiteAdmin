using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LiteAdmin.SqlServer.Tests
{
    using System.Collections.Generic;

    [TestClass]
    public class TableRepositoryTests
    {
        [TestMethod]
        public void When_GetTables_is_called_then_a_collection_tables_should_be_returned()
        {
            // Arrange
            var systemUnderTest = new SchemaRepository(@"Server=.\SQLExpress;Database=Example;Trusted_Connection=True", new List<string>());

            // Act
            var result = systemUnderTest.GetTables();

            // Assert
            Assert.IsNotNull(result);
        }
    }
}
