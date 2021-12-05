using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SamuraiApp.Data;
using SamuraiApp.Domain;

namespace SamuraiApp.Test
{
    [TestClass]
    public class DatabaseTest
    {
        [TestMethod]
        public void CanInsertSamuraiInDatabase()
        {
            using var context = new SamuraiContext();
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
            var samurai = new Samurai();
            context.Samuraies.Add(samurai);
            Debug.WriteLine($"Before add {samurai.Id}");
            context.SaveChanges();
            Debug.WriteLine($"After add {samurai.Id}");
            Assert.AreNotEqual(0, samurai.Id);
        }
    }
}
