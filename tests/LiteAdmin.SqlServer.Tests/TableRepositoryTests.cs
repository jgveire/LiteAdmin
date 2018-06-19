using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LiteAdmin.SqlServer.Tests
{
    [TestClass]
    public class TableRepositoryTests
    {
        [TestMethod]
        public void When_GetTables_is_called_then_a_collection_tables_should_be_returned()
        {
            // Arrange
            var systemUnderTest = new TableRepository(@"Server=.\SQLExpress;Database=Example;Trusted_Connection=True");

            // Act
            var result = systemUnderTest.GetTables();

            // Assert
            Assert.IsNotNull(result);
        }
    }
}
