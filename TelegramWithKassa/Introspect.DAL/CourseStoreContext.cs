using Introspekt.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace Introspekt.DAL
{
    public class CourseStoreContext : DbContext
    {
        public CourseStoreContext()
        {
            Database.EnsureCreated();
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<Course> Courses { get; set; }

        public DbSet<OrderDetails> OrderDetails { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=CourseStoreDb;Integrated Security=True;Connect Timeout=30;ApplicationIntent=ReadWrite;";
            optionsBuilder.UseSqlServer(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasOne(a => a.Order)
                .WithOne(b => b.User)
                .HasForeignKey<User>(b => b.OrderId);

            modelBuilder.Entity<Order>()
                .HasMany(a => a.OrderDetails)
                .WithOne(b => b.Order)
                .HasForeignKey(x => x.OrderId);


            modelBuilder.Entity<Course>().HasData(
                new Course
                {
                    Id = 1,
                    Price = 150,
                    Name = "Борьба с ленью",
                    Description = @"Марафон продлится 13 дней. Каждый день, вы будете получать задания, каждое из которых станет шагом к нашей с Вами цели. Помимо заданий, вам будут присылаться статьи и книги,  способные помочь вам, в превращении лени в суперсилу.",
                    Photo = "1.jpg"
                },
                new Course
                {
                    Id = 2,
                    Price = 250,
                    Name = "Ты красавчик",
                    Description = "Крутой марафон",
                    Photo = "2.jpg"
                });
        }
    }
}
