using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DanielCook.Sql;

namespace SeaQuellTests
{
    [TestClass]
    public class TestSqlBuilder
    {
        [TestMethod]
        public void TestWithSubQuerySelect()
        {
            const string sql = "select foo from (select foo from Users) u";

            var select = SeaQuell.
                Select().
                Field("foo").                
                From(SeaQuell.Select().Field("foo").From("Users"), "u").                
                ToString();

            Assert.AreEqual(select, sql);
        }

        [TestMethod]
        public void TestWithAliasSelect()
        {
            const string sql = "select foo, min(bar) from Users u group by foo";

            var select = SeaQuell.
                Select().
                Field("foo").
                Field("min(bar)").
                From("Users", "u").
                GroupBy("foo").
                ToString();

            Assert.AreEqual(select, sql);
        }

        [TestMethod]
        public void TestSimpleSelect()
        {
            const string sql = "select foo, bar from Users";

            var select = SeaQuell.
                Select().
                Field("foo").
                Field("bar").
                From("Users").
                ToString();

            Assert.AreEqual(select, sql);
        }

        [TestMethod]
        public void TestWhereSelect()
        {
            const string sql = "select foo, bar from Users where Active = 1";

            var select = SeaQuell.
                Select().
                Field("foo").
                Field("bar").
                From("Users").
                Where("Active = 1").
                ToString();

            Assert.AreEqual(select, sql);
        }

        [TestMethod]
        public void TestDistinctSelect()
        {
            const string sql = "select distinct foo, bar from Users";

            var select = SeaQuell.
                Select().
                Field("foo").
                Field("bar").
                From("Users").
                Distinct().
                ToString();

            Assert.AreEqual(select, sql);
        }

        [TestMethod]
        public void TestOrderBySelect()
        {
            const string sql = "select foo, bar from Users order by foo asc";

            var select = SeaQuell.
                Select().
                Field("foo").
                Field("bar").
                From("Users").
                OrderBy("foo").
                ToString();

            Assert.AreEqual(select, sql);
        }

        [TestMethod]
        public void TestMultipleOrderBySelect()
        {
            const string sql = "select foo, bar from Users order by foo asc, bar desc";

            var select = SeaQuell.
                Select().
                Field("foo").
                Field("bar").
                From("Users").
                OrderBy("foo").
                OrderBy("bar", false).
                ToString();

            Assert.AreEqual(select, sql);
        }

        [TestMethod]
        public void TestGroupBySelect()
        {
            const string sql = "select foo, min(bar) from Users group by foo";

            var select = SeaQuell.
                Select().
                Field("foo").
                Field("min(bar)").
                From("Users").
                GroupBy("foo").
                ToString();

            Assert.AreEqual(select, sql);
        }

        [TestMethod]
        public void TestMissingFieldListSelect()
        {
            const string sql = "select * from Users";

            var select = SeaQuell.
                Select().                
                From("Users").
                ToString();

            Assert.AreEqual(select, sql);
        }

        [TestMethod]
        public void TestTopSelect()
        {
            const string sql = "select top 10 foo from Users order by foo asc";

            var select = SeaQuell.
                Select().
                Top(10).
                Field("foo").
                From("Users").
                OrderBy("foo").
                ToString();

            Assert.AreEqual(select, sql);
        }
    }
}
