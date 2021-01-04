using System.Collections.Generic;
using System.Threading.Tasks;
using OnlineClinic.Data.Entities;

namespace OnlineClinic.Core.Services
{
    public interface IGenericService<E> where E: Entity
    {
        int Create<M>(M dataTransferObj);

        Task<int> CreateAsync<M>(M dataTransferObj);

        void Update<M>(M dataTransferObj);

        M GetById<M>(int id);

        IEnumerable<M> GetAll<M>(bool asNoTracking = true);

        bool Exists(int id);
    }

}