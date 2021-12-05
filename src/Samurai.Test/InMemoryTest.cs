using System;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SamuraiApp.Data;
using SamuraiApp.Domain;

namespace SamuraiApp.Test
{
    [TestClass]
    public class InMemoryTest
    {
        [TestMethod]
        public void CanInsertSamuraiInMemry()
        {
            var builder = new DbContextOptionsBuilder();
            builder.UseInMemoryDatabase("CanInserrtSamurai");
            using var context = new SamuraiContext(builder.Options);
            
            var samurai = new Samurai();
            context.Samuraies.Add(samurai);
            context.SaveChanges();
            //Assert.AreNotEqual(0, samurai.Id);
            Assert.AreEqual(EntityState.Added, context.Entry(samurai).State);
        }
    }
}
