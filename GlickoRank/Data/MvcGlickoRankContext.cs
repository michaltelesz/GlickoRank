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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
    }
}
