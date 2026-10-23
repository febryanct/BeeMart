using BeeMart.backend.Models;
using Microsoft.EntityFrameworkCore;

namespace BeeMart.backend.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        // Daftarkan kelima tabel agar bisa diakses oleh aplikasi
        public DbSet<MsUser> MsUsers { get; set; }
        public DbSet<MsProduct> MsProducts { get; set; }
        public DbSet<LtPaymentMethod> LtPaymentMethods { get; set; }
        public DbSet<TrTransaction> TrTransactions { get; set; }
        public DbSet<TrTransactionDetail> TrTransactionDetails { get; set; }
    }
}