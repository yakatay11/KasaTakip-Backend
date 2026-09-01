using Microsoft.EntityFrameworkCore;
using KasaAPI.Models;

namespace KasaAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Kullanici> Kullanicilar { get; set; }
        public DbSet<GiderTalebi> GiderTalepleri { get; set; }
        public DbSet<Gider> Giderler { get; set; }
        public DbSet<Gelir> Gelirler { get; set; }
        public DbSet<AylikRapor> AylikRaporlar { get; set; }
    }
}