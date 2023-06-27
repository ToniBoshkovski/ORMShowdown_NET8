using Microsoft.EntityFrameworkCore;
using ORMShowdown_NET8.Models;

namespace ORMShowdown_NET8
{
    public class EFCoreDbContext : DbContext
    {
        public EFCoreDbContext() : base()
        { }

        public EFCoreDbContext(DbContextOptions<EFCoreDbContext> options) : base(options)
        {
        }

        public virtual DbSet<Product> Products { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=TONI-BOSHKOVSKI; Initial Catalog=ORMShowdown; MultipleActiveResultSets=true; Integrated Security=true; TrustServerCertificate=true;");
        }
    }
}
