using AypWebAPI.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace AypWebAPI.Context
{
    public class ApplicationContext: DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {

        }

        public DbSet<Player> Players { get; set; }
        public DbSet<Team> Teams { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Write Fluent API configurations here

            //Property Configurations
            modelBuilder.Entity<Player>()
                .HasKey(p => p.Id);
            modelBuilder.Entity<Player>()
                .Property(p => p.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Team>()
                .HasKey(t => t.Id);
            modelBuilder.Entity<Team>()
                .Property(t => t.Id)
                .ValueGeneratedOnAdd();


            //Relationship Configurations
            modelBuilder.Entity<Player>()
                .HasOne<Team>(p => p.Team)
                .WithMany(t => t.Players)
                .HasForeignKey(p => p.TeamId);
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseInMemoryDatabase("exampleDatabase");
        //}
    }
}
