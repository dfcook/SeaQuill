using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DanielCook.Sql;
using DanielCook.Sql.DataAnnotations;

namespace SeaQuellTests
{
    [Table("Users")]
    public class User
    {
        [Id]
        public string Id { get; set; }

        [Column("user_name")]
        public string UserName { get; set; }
    }

    [TestClass]
    public class TestObjectSelects
    {
        [TestMethod]
        public void TestObjectSelect()
        {
            const string sql = "select Id, user_name from Users";

            var select = SeaQuell.
                SelectFor<User>().                                                               
                ToString();

            Assert.AreEqual(select, sql);
        }

        [TestMethod]
        public void TestObjectSelectWithClause()
        {
            const string sql = "select Id, user_name from Users where Id != null";

            var select = SeaQuell.
                SelectFor<User>().
                Where("Id != null").
                ToString();

            Assert.AreEqual(select, sql);
        }
    }
}
