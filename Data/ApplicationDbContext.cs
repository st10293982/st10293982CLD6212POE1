using Microsoft.EntityFrameworkCore;
using st10293982CLD6212POE1.Models;

namespace st10293982CLD6212POE1.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<LeadEntity> Leads { get; set; }
    }
}
