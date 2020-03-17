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

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasOne(e => e.Student).WithOne(c => c.User)
                    .HasForeignKey<Student>(b => b.UserId);
            });

            modelBuilder.Entity<Student>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasOne(e => e.Group).WithMany(c => c.Students);
                entity.HasOne(e => e.User).WithOne(c => c.Student)
                    .HasForeignKey<User>(b => b.StudentId);
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

            modelBuilder.Entity<ItemLesson>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasOne(e => e.Item).WithMany(c => c.ItemsLessons);
            });
        }

        public virtual DbSet<Group> Groups { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<ItemLesson> ItemLessons { get; set; }
        public virtual DbSet<ItemGroup> ItemGroups { get; set; }
        public virtual DbSet<Item> Items { get; set; }
    }
}