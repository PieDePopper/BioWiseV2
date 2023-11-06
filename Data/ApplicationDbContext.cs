using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using BioWiseV2.Models;

namespace BioWiseV2.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<BioWiseV2.Models.Product> Product { get; set; } = default!;
        public DbSet<BioWiseV2.Models.Weekly_report> Weekly_report { get; set; } = default!;
        public DbSet<BioWiseV2.Models.Goal> Goal { get; set; } = default!;
        public DbSet<BioWiseV2.Models.TransportUsage> TransportUsage { get; set; } = default!;
        public DbSet<BioWiseV2.Models.Consumer> Consumer { get; set; } = default!;
    }
}