using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SeaQuill.Tests
{
    [TestClass]
    public class JoinSelectTests
    {
        [TestMethod]
        public void TestJoinSelect()
        {
            const string sql = "select foo from Users inner join Address";

            var select = Sql.
                Select().
                Field("foo").
                From("Users").
                Join("Address").
                ToString();

            Assert.AreEqual(sql, select);
        }

        [TestMethod]
        public void TestMultipleInnerJoinSelect()
        {
            const string sql = "select foo from Users inner join Address inner join Postcodes";

            var select = Sql.
                Select().
                Field("foo").
                From("Users").
                Join("Address").
                Join("Postcodes").
                ToString();

            Assert.AreEqual(sql, select);
        }

        [TestMethod]
        public void TestJoinSelectWithAlias()
        {
            const string sql = "select foo from Users u inner join Address a";

            var select = Sql.
                Select().
                Field("foo").
                From("Users", "u").
                Join("Address", "a").
                ToString();

            Assert.AreEqual(sql, select);
        }

        [TestMethod]
        public void TestJoinSelectWithCriteria()
        {
            const string sql = "select foo from Users inner join Address on AddressId = UserAddressId";

            var select = Sql.
                Select().
                Field("foo").
                From("Users").
                Join("Address", string.Empty, "AddressId = UserAddressId").
                ToString();

            Assert.AreEqual(sql, select);
        }

        [TestMethod]
        public void TestJoinSelectWithAliasAndCriteria()
        {
            const string sql = "select foo from Users u inner join Address a on AddressId = UserAddressId";

            var select = Sql.
                Select().
                Field("foo").
                From("Users", "u").
                Join("Address", "a", "AddressId = UserAddressId").
                ToString();

            Assert.AreEqual(sql, select);
        }

        [TestMethod]
        public void TestMultipleJoinSelect()
        {
            const string sql = "select foo from Users inner join Address left outer join Postcodes";

            var select = Sql.
                Select().
                Field("foo").
                From("Users").
                Join("Address").
                LeftJoin("Postcodes").
                ToString();

            Assert.AreEqual(sql, select);
        }
    }
}
