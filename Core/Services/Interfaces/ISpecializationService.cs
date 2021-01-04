using System.Collections.Generic;
using System.Threading.Tasks;
using OnlineClinic.Core.DTOs;

namespace OnlineClinic.Core.Services
{
    public interface ISpecializationService
    {
        bool Exists(string name);

        bool Exists(int id);

        bool IsNameAlreadyUsed(int id, string name);

        Task<SpecializationGetDto> GetByIdAsync(int id);

        SpecializationGetDto GetById(int id);

        Task<int> CreateAsync(SpecializationCreateDto specialization);

        int Create(SpecializationCreateDto specialization);

        Task UpdateAsync(SpecializationEditDto specialization);

        void Update(SpecializationEditDto specialization);

        IEnumerable<SpecializationGetDto> GetAll();

        Task<IEnumerable<SpecializationGetDto>> GetAllAsync();

        Dictionary<int, string> GetSpecializationIdAndNames();
    }
}