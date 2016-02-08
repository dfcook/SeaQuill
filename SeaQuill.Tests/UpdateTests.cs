using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SeaQuill.Tests
{
    [TestClass]
    public class UpdateTests
    {
        [TestMethod]
        public void TestSimpleUpdate()
        {
            const string sql = "update users set foo = 'bar'";

            var select = Sql.
                Update().
                Target("users").
                Set("foo", "bar").
                ToString();

            Assert.AreEqual(sql, select);
        }

        [TestMethod]
        public void TestSimpleNumericUpdate()
        {
            const string sql = "update users set foo = 10";

            var select = Sql.
                Update().
                Target("users").
                Set("foo", 10).
                ToString();

            Assert.AreEqual(sql, select);
        }

        [TestMethod]
        public void TestUpdateWithWhere()
        {
            const string sql = "update users set foo = 'bar' where Id = 10";

            var select = Sql.
                Update().
                Target("users").
                Set("foo", "bar").
                Where("Id = 10").
                ToString();

            Assert.AreEqual(sql, select);
        }

        [TestMethod]
        public void TestUpdateWithJoin()
        {
            const string sql = "update users set u.foo = e.bar from users u inner join employee e on u.Id = e.UserId";

            var select = Sql.
                Update().
                Target("users").
                Set("u.foo", "e.bar", true).
                From("users", "u").
                Join("employee", "e", "u.Id = e.UserId").
                ToString();

            Assert.AreEqual(sql, select);
        }
    }
}
