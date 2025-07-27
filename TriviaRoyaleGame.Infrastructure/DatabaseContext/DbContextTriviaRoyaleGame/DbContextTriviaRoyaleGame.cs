using TriviaRoyaleGame.Infrastructure.Models.Classes;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace TriviaRoyaleGame.Infrastructure.DatabaseContext.DbContextTriviaRoyaleGame
{
    public class DbContextTriviaRoyaleGame(DbContextOptions<DbContextTriviaRoyaleGame> options) : DbContext(options)
    {
        public DbSet<Member>? Members { get; set; }
        public DbSet<Question>? Questions { get; set; }
        public DbSet<User>? Users { get; set; }
        public DbSet<ClientAppLog>? ClientAppLogs { get; set; }
        public DbSet<ScoreBoard>? ScoreBoards { get; set; }
        public DbSet<Category>? Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // START Config for Auto-increment PostgreSQL
            //modelBuilder.UseSerialColumns();
            // END Config for Auto-increment PostgreSQL

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
            base.OnConfiguring(optionsBuilder);
        }

    }
}
