using Microsoft.EntityFrameworkCore;
using System;
using TvShowService.BusinessLogic.Entities;

namespace TvShowService.BusinessLogic.Dal
{
    /// <summary>
    /// TvShow entity framework dbcontext
    /// </summary>
    public class TvShowDbContext : DbContext
    {
        public DbSet<Actor> Actors { get; set; }
        public DbSet<TvShow> TvShows { get; set; }

        public TvShowDbContext(DbContextOptions options)
              : base(options)
        {
            // TODO generate database instead of ensurecreated
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            if (modelBuilder == null)
            {
                throw new ArgumentNullException(nameof(modelBuilder));
            }

            modelBuilder
                .Entity<TvShow>(entity =>
                {
                    entity.HasKey(e => e.Id);
                    entity.HasIndex(e => e.TvMazeId).IsUnique(true);
                })
                .Entity<Actor>(entity =>
                {
                    entity.HasKey(e => e.Id);
                    entity.HasIndex(e => e.TvMazeId).IsUnique(true);
                })
                .Entity<ActorTvShow>(entity =>
                {
                    entity.HasKey(e => new { e.ActorId, e.TvShowId });
                });
        }
    }
}
