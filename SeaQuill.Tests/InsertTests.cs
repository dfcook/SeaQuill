using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SeaQuill.Tests
{
    [TestClass]
    public class InsertTests
    {
        [TestMethod]
        public void TestInsertFromQuery()
        {
            const string sql = "insert foo select * from bar";

            var select = Sql.
                Insert().
                Target("foo").
                FromQuery(Sql.Select().From("bar")).
                ToString();

            Assert.AreEqual(sql, select);
        }

        [TestMethod]
        public void TestSimpleInsert()
        {
            const string sql = "insert foo (bar) values ('foo')";

            var select = Sql.
                Insert().
                Target("foo").
                Set("bar", "foo").
                ToString();

            Assert.AreEqual(sql, select);
        }
    }
}
