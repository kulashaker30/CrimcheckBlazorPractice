using System.Collections.Generic;
using OnlineClinic.Data.Entities;

namespace OnlineClinic.Data.Repositories
{
    public interface ISpecializationRepository: IRepository<Specialization>
    {
        Dictionary<int, string> GetSpecializationIdAndNames();
    }
}