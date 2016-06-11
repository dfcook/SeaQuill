using Microsoft.VisualStudio.TestTools.UnitTesting;
using SeaQuill.DataAnnotations;

namespace SeaQuill.Tests
{
    [Table("Users")]
    public class User
    {
        [Id]
        public int Id { get; set; }

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

            var select = ObjectMapping.Sql.
                SelectFor<User>().
                ToString();

            Assert.AreEqual(sql, select);
        }

        [TestMethod]
        public void TestObjectSelectWithClause()
        {
            const string sql = "select Id, user_name from Users where Id != null";

            var select = ObjectMapping.Sql.
                SelectFor<User>().
                Where("Id != null").
                ToString();

            Assert.AreEqual(sql, select);
        }

        [TestMethod]
        public void TestObjectSelectWithPredicateClause()
        {
            const string sql = "select Id, user_name from Users where Id = 10 and user_name = \"foo\"";

            var select = ObjectMapping.Sql.
                SelectFor<User>().
                Where(x => x.Id == 10).
                Where(x => x.UserName == "foo").
                ToString();

            Assert.AreEqual(sql, select);
        }
    }
}
