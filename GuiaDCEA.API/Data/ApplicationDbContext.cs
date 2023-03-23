using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GuiaDCEA.API.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        public DbSet<Lugar> Lugares { get; set; }
        public DbSet<Fotografia> Fotografias { get; set; }
        public DbSet<FotografiaLugar> FotografiasLugares { get; set; }
    }
}
