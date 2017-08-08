using Microsoft.EntityFrameworkCore;

namespace ToDoXUnitTest.Models
{
    public class TestDbContext : ToDoDbContext
    {
        public override DbSet<Item> Items { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=ToDoXUnitTest_test;integrated security = True");
        }
    }
}
