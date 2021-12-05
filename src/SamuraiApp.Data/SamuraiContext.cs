using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SamuraiApp.Domain;

namespace SamuraiApp.Data
{
    public class SamuraiContext : DbContext
    {
        public SamuraiContext()
        {

        }
        public SamuraiContext(DbContextOptions options) : base(options)
        {

        }
        public SamuraiContext(DbContextOptions<SamuraiContext> options) : base(options)
        {

        }
        public DbSet<Samurai> Samuraies { get; set; }
        public DbSet<Quote> Quotes { get; set; }
        public DbSet<Battle> Battles { get; set; }
        public DbSet<SamuraiBatleState> SamuraiBatleStates { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured) return;
            optionsBuilder.UseSqlite(
                "Data Source=Database.db",
                options=>options.MaxBatchSize(100)
                )
                .LogTo(
                    Console.WriteLine,
                    new[] {
                        DbLoggerCategory.Database.Command.Name,
                        DbLoggerCategory.Database.Transaction.Name
                    },
                    LogLevel.Debug
                )
                .EnableSensitiveDataLogging();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //optional
            modelBuilder.Entity<Samurai>()
                .HasMany(s => s.Battles)
                .WithMany(b => b.Samuraies)
                .UsingEntity<BattleSamurai>(
                    bs => bs.HasOne<Battle>().WithMany(),
                    bs => bs.HasOne<Samurai>().WithMany()
                    )
                .Property(bs => bs.DateJoined)
                .HasDefaultValueSql("getdate()");

            modelBuilder.Entity<Horse>().ToTable("Horses");
            modelBuilder.Entity<SamuraiBatleState>().HasNoKey().ToView("SamuraiBatleStates");
        }

    }
}
