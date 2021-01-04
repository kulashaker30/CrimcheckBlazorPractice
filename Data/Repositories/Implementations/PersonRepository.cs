using System;
using System.Collections.Generic;
using System.Linq;
using OnlineClinic.Data.Entities;

namespace OnlineClinic.Data.Repositories
{
    public class PersonRepository : Repository<Person>, IPersonRepository
    {
        public PersonRepository(OnlineClinicDbContext context): base(context) { }

        public Dictionary<int, string> GetPersonIdAndNames() => GetAll().ToDictionary(p => p.Id, p => p.Fullname);
    }
}
