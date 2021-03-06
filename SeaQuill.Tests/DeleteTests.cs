﻿using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SeaQuill.Tests
{
    [TestClass]
    public class DeleteTests
    {
        [TestMethod]
        public void TestSimpleDelete()
        {
            const string sql = "delete users";

            var select = Sql.
                Delete().
                Target("users").
                ToString();

            Assert.AreEqual(sql, select);
        }

        [TestMethod]
        public void TestDeleteWithFilter()
        {
            const string sql = "delete users where Id = 'foo'";

            var select = Sql.
                Delete().
                Target("users").
                Where("Id = 'foo'").
                ToString();

            Assert.AreEqual(sql, select);
        }

        [TestMethod]
        public void TestDeleteWithJoin()
        {
            const string sql = "delete users from users u inner join employee e on u.UserId = e.UserId";

            var select = Sql.
                Delete().
                Target("users").
                From("users", "u").
                Join("employee", "e", "u.UserId = e.UserId").
                ToString();

            Assert.AreEqual(sql, select);
        }
    }
}
