using Microsoft.VisualStudio.TestTools.UnitTesting;
using SeaQuill.DataAccess;
using SeaQuill.DataAnnotations;
using SeaQuill.ObjectMapping;
using System.Linq;

namespace SeaQuillTests
{
    [Table("Users")]
    public class UserDetail
    {
        [Id]
        public int UserId { get; set; }

        public string User_Ref { get; set; }

        public string User_Name { get; set; }

        public bool NonActive { get; set; }
    }

    [TestClass]
    public class DataRetrievalTests
    {
        [TestMethod]
        public void TestSelectExecute()
        {
            var users = Sql.
                SelectFor<UserDetail>().
                Where(x => x.UserId > 500).
                ExecuteList();

            Assert.IsNotNull(users);
            Assert.IsTrue(users.Any());
        }

        [TestMethod]
        public void TestSelectSingle()
        {
            const int userId = 485;
            var user = Sql.
                SelectFor<UserDetail>().
                Where(x => x.UserId == userId).
                ExecuteSingle();

            Assert.IsNotNull(user);
        }
    }
}
