using center.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace center.Api.Dbcontexts
{
    public class CenterDbContext : DbContext
    {
        public DbSet<Student> Students { get; set; } 
        public DbSet<Exam> Exams { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source = CenterDb.db");
            base.OnConfiguring(optionsBuilder);
        }

    }
}
