using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;
using VMTipping.Model;

namespace VMTippekonkurranse.Models
{
    public class TippeContext:DbContext
    {
        public TippeContext()
            : base("DefaultConnection")
        {
        }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Game> Matches { get; set; }
        public DbSet<MatchPrediction> MatchPredictions { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<RoundPrediction> RoundPredictions { get; set; }
        public DbSet<RoundPredictionTeam> RoundPredictionTeams { get; set; }
        public DbSet<Round> Rounds { get; set; }
        public DbSet<RoundTeam> RoundTeams{get; set;}

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Entity<Game>()
                    .HasRequired(m => m.HomeTeam)
                    .WithMany(t => t.HomeMatches)
                    .HasForeignKey(m => m.HomeTeamId)
                    .WillCascadeOnDelete(false);

            modelBuilder.Entity<Game>()
                        .HasRequired(m => m.AwayTeam)
                        .WithMany(t => t.AwayMatches)
                        .HasForeignKey(m => m.AwayTeamId)
                        .WillCascadeOnDelete(false);
            base.OnModelCreating(modelBuilder);
        }
    }
}