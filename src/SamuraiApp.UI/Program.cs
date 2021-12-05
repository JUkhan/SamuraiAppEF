using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SamuraiApp.Data;
using SamuraiApp.Domain;

namespace SamuraiApp.UI
{
    class Program
    {
        static SamuraiContext dbContext = new SamuraiContext();
        static void Main(string[] args)
        {
            dbContext.Database.EnsureCreated();
            Samurai samurai = new Samurai { Name = "Jasim" };
            dbContext.Samuraies.Add(samurai);
            dbContext.SaveChanges();
            var count = dbContext.Samuraies.Count();
            Console.WriteLine(count);
        }

        static void LoadQuotesAndHorse()
        {
            var sam = dbContext.Samuraies.Find(1);
            dbContext.Entry(sam).Collection(s => s.Quotes).Load();
            dbContext.Entry(sam).Reference(s => s.Horse).Load();
        }

        static void LoadHappyQuotes()
        {
            var sam = dbContext.Samuraies.Find(1);
            var res=dbContext.Entry(sam).Collection(s => s.Quotes).Query().Where(s=>s.Text.Contains("Happy")).ToList();
            
        }
        static void State()
        {
            var sams = dbContext.Samuraies.Include(s => s.Quotes).FirstOrDefault(s => s.Id == 2);
            var quote = sams.Quotes[1];
            quote.Text = "update text";

            using var newContext = new SamuraiContext();
            newContext.Entry(quote).State = EntityState.Modified;
            newContext.SaveChanges();
        }
        static void RemveSamuraiFromABattle(int samuraiId, int battleId)
        {
            var battle = dbContext.Battles.Include(s => s.Samuraies.Where(s => s.Id == samuraiId))
            .Single(s => s.BattleId == battleId);

            var samurai = battle.Samuraies[0];
            dbContext.Samuraies.Remove(samurai);
            dbContext.SaveChanges();
        }

        static void AddNewHorseToSamuraiUsingId(int samuraiId)
        {
            var horse = new Horse { Name = "Jecos", SamuraiId = samuraiId };
            dbContext.Add(horse);
            dbContext.SaveChanges();
        }

        static void AddNewHorseToSamuraiObject(int samuraiId)
        {
            var samurai = dbContext.Samuraies.Find(samuraiId);
            samurai.Horse = new Horse { Name = "Jecoos" };
            dbContext.SaveChanges();
        }

        static void AddNewHorseToDiscunnectedSamuraiObject(int samuraiId)
        {
            var samurai = dbContext.Samuraies.FirstOrDefault(ss=>ss.Id==samuraiId);
            samurai.Horse = new Horse { Name = "Mr. Ed" };

            using var newContext = new SamuraiContext();
            newContext.Samuraies.Attach(samurai);
            dbContext.SaveChanges();
        }

        static void RepaceAHorse()
        {
            var horse = dbContext.Set<Horse>().FirstOrDefault(s => s.Name == "Mr. Ed");
            horse.SamuraiId = 5;
            dbContext.SaveChanges();
        }

        static void QuuerySamuraiBattleState()
        {
            var states = dbContext.SamuraiBatleStates.ToList();
            var sampsoonState = dbContext.SamuraiBatleStates
                .FirstOrDefault(s => s.Name == "Sampson");
        }

        static void QueryUsingRawSql()
        {
            var samuraies = dbContext.Samuraies.FromSqlRaw("Select * from samuraies").ToList();
            var name = "kawasaki";
            samuraies = dbContext.Samuraies.FromSqlInterpolated($"select * from Samuraies where name = {name}").ToList();
            samuraies = dbContext.Samuraies.FromSqlRaw("EXEC dbo.SamuraisWhoSaidAWorrd {0}", name).ToList();
            dbContext.Database.ExecuteSqlRaw("ExEC DeeteQuotesForSamurai {0}", 12);
        }
    }
}
