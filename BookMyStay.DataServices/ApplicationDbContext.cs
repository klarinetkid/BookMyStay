using BookMyStay.DataServices.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookMyStay.DataServices
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<TblReservation> TblReservations { get; set; }

        public ApplicationDbContext(string connectionString) : base(GetOptions(connectionString))
        {
        }

        private static DbContextOptions GetOptions(string connectionString)
        {
            return SqlServerDbContextOptionsExtensions.UseSqlServer(new DbContextOptionsBuilder(), connectionString).Options;
        }
    }
}
