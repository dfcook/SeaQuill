using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SeaQuill.Tests
{
    [TestClass]
    public class SelectTests
    {
        [TestMethod]
        public void TestWithUnionSelect()
        {
            const string sql = "select foo from Users union select foo from Admins";

            var select = Sql.
                Select().
                Field("foo").
                From("Users").
                Union(Sql.Select().Field("foo").From("Admins")).
                ToString();

            Assert.AreEqual(sql, select);
        }

        [TestMethod]
        public void TestWithUnionAllSelect()
        {
            const string sql = "select foo from Users union all select foo from Admins";

            var select = Sql.
                Select().
                Field("foo").
                From("Users").
                UnionAll(Sql.Select().Field("foo").From("Admins")).
                ToString();

            Assert.AreEqual(sql, select);
        }

        [TestMethod]
        public void TestWithSubQuerySelect()
        {
            const string sql = "select foo from (select foo from Users) u";

            var select = Sql.
                Select().
                Field("foo").                
                From(Sql.Select().Field("foo").From("Users"), "u").                
                ToString();

            Assert.AreEqual(sql, select);
        }

        [TestMethod]
        public void TestWithAliasSelect()
        {
            const string sql = "select foo, min(bar) from Users u group by foo";

            var select = Sql.
                Select().
                Field("foo").
                Field("min(bar)").
                From("Users", "u").
                GroupBy("foo").
                ToString();

            Assert.AreEqual(sql, select);
        }

        [TestMethod]
        public void TestSimpleSelect()
        {
            const string sql = "select foo, bar from Users";

            var select = Sql.
                Select().
                Field("foo").
                Field("bar").
                From("Users").
                ToString();

            Assert.AreEqual(sql, select);
        }

        [TestMethod]
        public void TestWhereSelect()
        {
            const string sql = "select foo, bar from Users where Active = 1";

            var select = Sql.
                Select().
                Field("foo").
                Field("bar").
                From("Users").
                Where("Active = 1").
                ToString();

            Assert.AreEqual(sql, select);
        }

        [TestMethod]
        public void TestDistinctSelect()
        {
            const string sql = "select distinct foo, bar from Users";

            var select = Sql.
                Select().
                Field("foo").
                Field("bar").
                From("Users").
                Distinct().
                ToString();

            Assert.AreEqual(sql, select);
        }

        [TestMethod]
        public void TestOrderBySelect()
        {
            const string sql = "select foo, bar from Users order by foo asc";

            var select = Sql.
                Select().
                Field("foo").
                Field("bar").
                From("Users").
                OrderBy("foo").
                ToString();

            Assert.AreEqual(sql, select);
        }

        [TestMethod]
        public void TestMultipleOrderBySelect()
        {
            const string sql = "select foo, bar from Users order by foo asc, bar desc";

            var select = Sql.
                Select().
                Field("foo").
                Field("bar").
                From("Users").
                OrderBy("foo").
                OrderBy("bar", false).
                ToString();

            Assert.AreEqual(sql, select);
        }

        [TestMethod]
        public void TestGroupBySelect()
        {
            const string sql = "select foo, min(bar) from Users group by foo";

            var select = Sql.
                Select().
                Field("foo").
                Field("min(bar)").
                From("Users").
                GroupBy("foo").
                ToString();

            Assert.AreEqual(sql, select);
        }

        [TestMethod]
        public void TestMissingFieldListSelect()
        {
            const string sql = "select * from Users";

            var select = Sql.
                Select().                
                From("Users").
                ToString();

            Assert.AreEqual(sql, select);
        }

        [TestMethod]
        public void TestTopSelect()
        {
            const string sql = "select top 10 foo from Users order by foo asc";

            var select = Sql.
                Select().
                Top(10).
                Field("foo").
                From("Users").
                OrderBy("foo").
                ToString();

            Assert.AreEqual(sql, select);
        }
    }
}
