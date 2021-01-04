using System.Collections.Generic;
using OnlineClinic.Data.Entities;

namespace OnlineClinic.Data.Repositories
{
    public interface IPersonRepository: IRepository<Person> 
    {
        Dictionary<int, string> GetPersonIdAndNames();
    }
}