using Microsoft.VisualStudio.TestTools.UnitTesting;
using SeaQuill.DataAnnotations;

namespace SeaQuill.Tests
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

            var select = DataAnnotations.Sql.
                SelectFor<User>().                                                               
                ToString();

            Assert.AreEqual(sql, select);
        }

        [TestMethod]
        public void TestObjectSelectWithClause()
        {
            const string sql = "select Id, user_name from Users where Id != null";

            var select = DataAnnotations.Sql.
                SelectFor<User>().
                Where("Id != null").                
                ToString();

            Assert.AreEqual(sql, select);
        }
    }
}
