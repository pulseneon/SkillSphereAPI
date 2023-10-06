using asp_net_db.Models;
using Microsoft.EntityFrameworkCore;

namespace asp_net_db.Data
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
            // Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Lesson>().HasQueryFilter(x => !x.isDeleted);
            modelBuilder.Entity<Course>().HasQueryFilter(x => !x.isDeleted);
        }
        

        public DbSet<Test> Test { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<Tracker> Trackers { get; set; }
        public DbSet<Homework> Homeworks { get; set; }
        public DbSet<SolvedHomework> SolvedHomeworks { get; set; }
        public DbSet<Content> Contents { get; set; }
    }
}
