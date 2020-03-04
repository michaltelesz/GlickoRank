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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Character>().HasData(new Character { ID = 1, Name = "MajkPascal_1_2305843009296116294", CharacterId = "2305843009296116294", MembershipId = "4611686018470345232", MembershipType = 1 });

            modelBuilder.Entity<CharacterActivity>()
            .HasKey(ca => new { ca.CharacterId, ca.ActivityId });

            modelBuilder.Entity<CharacterActivity>()
            .HasOne(ca => ca.Character)
            .WithMany(c => c.CharacterActivities)
            .HasForeignKey(ca => ca.CharacterId);

            modelBuilder.Entity<CharacterActivity>()
                .HasOne(ca => ca.Activity)
                .WithMany(a => a.CharacterActivities)
                .HasForeignKey(ca => ca.ActivityId);
        }
    }
}
