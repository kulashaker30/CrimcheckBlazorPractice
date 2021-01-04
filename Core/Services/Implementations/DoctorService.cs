using System.Linq;
using System.Collections.Generic;

using AutoMapper;

using OnlineClinic.Core.DTOs;
using OnlineClinic.Data.Entities;
using OnlineClinic.Data;
using OnlineClinic.Data.Repositories;
using System.Threading.Tasks;

namespace OnlineClinic.Core.Services
{
    public class DoctorService : GenericService<Doctor>, IDoctorService
    {
        public DoctorService(
            IDoctorRepository repository,
            IMapper mapper) :base(repository, mapper) { }

        public Task<int> CreateAsync(DoctorCreateDto createDto) => _repository.CreateAsync(_mapper.Map<Doctor>(createDto));

        public IEnumerable<DoctorGetDto> GetAllPhysicians() => _mapper.Map<IEnumerable<DoctorGetDto>>(_repository.GetAll(false));

        public bool PersonExists(int personId) => _repository.GetAll().Any(d => d.PersonId == personId);
    }
}