using Microsoft.EntityFrameworkCore;
using OnlineEducation.DAL.Entities;

namespace OnlineEducation.DAL
{
    public class OnlineEducationDbContext : DbContext
    {
        public OnlineEducationDbContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Group>(entity =>
            {
                entity.HasKey(e => e.Id);
            });

            modelBuilder.Entity<Student>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasOne(e => e.Group).WithMany(c => c.Students);
            });

            modelBuilder.Entity<Item>(entity =>
            {
                entity.HasKey(e => e.Id);
            });

            modelBuilder.Entity<ItemGroup>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasOne(e => e.Group).WithMany(c => c.ItemGroups);
                entity.HasOne(e => e.Item).WithMany(c => c.ItemGroups);
            });

            modelBuilder.Entity<ItemsLesson>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasOne(e => e.Item).WithMany(c => c.ItemsLessons);
            });
        }

        public virtual DbSet<Group> Groups { get; set; }
        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<ItemsLesson> ItemsLessons { get; set; }
        public virtual DbSet<ItemGroup> ItemGroups { get; set; }
        public virtual DbSet<Item> Items { get; set; }
    }
}