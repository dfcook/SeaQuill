using Microsoft.VisualStudio.TestTools.UnitTesting;
using SeaQuill.ObjectMapping;
using SeaQuill.Tests;

namespace SeaQuillTests
{
    [TestClass]
    public class TestObjectDeletes
    {
        [TestMethod]
        public void TestObjectDelete()
        {
            const string sql = "delete Users";

            var select = Sql.
                DeleteFor<User>().
                ToString();

            Assert.AreEqual(sql, select);
        }

        [TestMethod]
        public void TestObjectDeleteWithClause()
        {
            const string sql = "delete Users where Id = 1";

            var select = Sql.
                DeleteFor<User>().
                Where(x => x.Id == 1).
                ToString();

            Assert.AreEqual(sql, select);
        }
    }
}
