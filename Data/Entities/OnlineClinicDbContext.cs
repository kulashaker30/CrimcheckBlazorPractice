using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace OnlineClinic.Data.Entities
{
    public class OnlineClinicDbContext: IdentityDbContext
    {
        public OnlineClinicDbContext() { }

        public DbSet<Specialization> Specializations { get; set; }

        public DbSet<Doctor> Doctors { get; set; }

        public DbSet<Person> People { get; set; }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=localhost; Database=OnlineClinic; User=sa; Password=Password1; integrated security=false;MultipleActiveResultSets=True;");
        }

        public OnlineClinicDbContext(DbContextOptions<OnlineClinicDbContext> options)
            : base(options)
        { }
    }
}