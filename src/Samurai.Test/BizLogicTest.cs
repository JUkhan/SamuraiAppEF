using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SamuraiApp.Data;
using SamuraiApp.Domain;
namespace SamuraiApp.Test
{
    [TestClass]
    public class BizLogicTest
    {
        [TestMethod]
        public void CanAddSamuraiByNames()
        {
            var builder = new DbContextOptionsBuilder();
            builder.UseInMemoryDatabase("CanAddSamuraiByNames");
            using var context = new SamuraiContext(builder.Options);
            var bizLogic = new BuusinessDataLogic(context);
            var nameList = new string[] { "arif", "rripoon" };
            var resut = bizLogic.AddSamuraiNames(nameList);

            Assert.AreEqual(nameList.Length, resut);
        }
    }
}
