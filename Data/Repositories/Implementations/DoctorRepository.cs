using System.Linq;
using OnlineClinic.Data.Entities;

namespace OnlineClinic.Data.Repositories
{
    public class DoctorRepository : Repository<Doctor>, IDoctorRepository
    {
        public DoctorRepository(OnlineClinicDbContext context): base(context) { }
    }
}
