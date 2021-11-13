using GuilhermeRocha.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace GuilhermeRocha.Infrastructure
{
    public class GuilhermeContext : DbContext
    {
        public GuilhermeContext(DbContextOptions<GuilhermeContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging();
        }

        public DbSet<User> User { get; set; }
    }
}
