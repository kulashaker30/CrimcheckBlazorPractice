using System.Collections.Generic;
using System.Linq;
using OnlineClinic.Data.Entities;

namespace OnlineClinic.Data.Repositories
{
    public class SpecializationRepository : Repository<Specialization>, ISpecializationRepository
    {
        public SpecializationRepository(OnlineClinicDbContext context): base(context) { }

        public Dictionary<int, string> GetSpecializationIdAndNames() => GetAll().ToDictionary(s => s.Id, s => s.Name);
    }
}
