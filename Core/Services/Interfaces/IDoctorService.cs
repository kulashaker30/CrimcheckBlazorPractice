using System.Collections.Generic;
using System.Threading.Tasks;
using OnlineClinic.Core.DTOs;
using OnlineClinic.Data.Repositories;

namespace OnlineClinic.Core.Services
{
    public interface IDoctorService
    {
        IEnumerable<DoctorGetDto> GetAllPhysicians();

        Task<int> CreateAsync(DoctorCreateDto createDto);

        bool PersonExists(int personId);
    }
}