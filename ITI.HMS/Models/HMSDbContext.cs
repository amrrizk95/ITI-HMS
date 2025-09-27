using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace ITI.HMS.Models
{
    public class HMSDbContext : DbContext
    {
        public HMSDbContext(DbContextOptions<HMSDbContext> options) : base(options)
        {
              
        }
        public DbSet<Doctor> Doctors { get; set; }
    }
}
