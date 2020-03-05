using GlickoRank.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GlickoRank.Data
{
    public class MvcGlickoRankContext : DbContext
    {
        public MvcGlickoRankContext(DbContextOptions<MvcGlickoRankContext> options)
            : base(options)
        {
        }

        public DbSet<Character> Character { get; set; }
        public DbSet<Activity> Activity { get; set; }

        public DbSet<CharacterActivity> CharacterActivity { get; set; }
        public DbSet<CharacterModeRank> CharacterModeRank { get; set; }
        public DbSet<CharacterActivityModeRank> CharacterActivityModeRank { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Character>().HasData(new Character { ID = 1, Name = "MajkPascal_1_2305843009296116294", CharacterId = "2305843009296116294", MembershipId = "4611686018470345232", MembershipType = 1 });

            modelBuilder.Entity<CharacterActivity>()
            .HasKey(ca => new { ca.CharacterID, ca.ActivityID });

            modelBuilder.Entity<CharacterActivity>()
            .HasOne(ca => ca.Character)
            .WithMany(c => c.CharacterActivities)
            .HasForeignKey(ca => ca.CharacterID);

            modelBuilder.Entity<CharacterActivity>()
                .HasOne(ca => ca.Activity)
                .WithMany(a => a.CharacterActivities)
                .HasForeignKey(ca => ca.ActivityID);

            modelBuilder.Entity<CharacterModeRank>().Property(r => r.Rating).HasDefaultValue(1500f);
            modelBuilder.Entity<CharacterModeRank>().Property(r => r.RD).HasDefaultValue(350f);
            modelBuilder.Entity<CharacterModeRank>().Property(r => r.Volatility).HasDefaultValue(0.06f);

            modelBuilder.Entity<CharacterActivityModeRank>().Property(r => r.Rating).HasDefaultValue(1500f);
            modelBuilder.Entity<CharacterActivityModeRank>().Property(r => r.RD).HasDefaultValue(350f);
            modelBuilder.Entity<CharacterActivityModeRank>().Property(r => r.Volatility).HasDefaultValue(0.06f);
        }
    }
}
