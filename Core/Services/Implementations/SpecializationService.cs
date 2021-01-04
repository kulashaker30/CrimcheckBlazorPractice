using System.Linq;
using System.Collections.Generic;

using AutoMapper;

using OnlineClinic.Core.DTOs;
using OnlineClinic.Data.Entities;
using OnlineClinic.Data.Repositories;
using System.Threading.Tasks;

namespace OnlineClinic.Core.Services
{
    public class SpecializationService : GenericService<Specialization>, ISpecializationService
    {
        public SpecializationService(
            ISpecializationRepository repository,
            IMapper mapper) : base(repository, mapper) { }

        public async Task<int> CreateAsync(SpecializationCreateDto specialization) => await _repository.CreateAsync(_mapper.Map<Specialization>(specialization));

        public int Create(SpecializationCreateDto specialization) => _repository.Create(_mapper.Map<Specialization>(specialization));

        public bool Exists(string name) => _repository.GetAll().Any(s => s.Name.ToLower() == name.ToLower());

        public IEnumerable<SpecializationGetDto> GetAll() => _mapper.Map<IEnumerable<SpecializationGetDto>>(_repository.GetAll());

        public async Task<IEnumerable<SpecializationGetDto>> GetAllAsync() => _mapper.Map<IEnumerable<SpecializationGetDto>>(await _repository.GetAllAsync());

        public async Task UpdateAsync(SpecializationEditDto specialization) => await _repository.UpdateAsync(_mapper.Map<Specialization>(specialization));

        public void Update(SpecializationEditDto specialization) => _repository.UpdateAsync(_mapper.Map<Specialization>(specialization));

        public bool IsNameAlreadyUsed(int id, string name) => _repository.GetAll().Any(s => s.Id != id && s.Name.ToLower() == name.ToLower());

        public async Task<SpecializationGetDto> GetByIdAsync(int id) => _mapper.Map<SpecializationGetDto>(await _repository.GetByIdAsync(id));

        public SpecializationGetDto GetById(int id) => _mapper.Map<SpecializationGetDto>(_repository.GetById(id));

        public Dictionary<int, string> GetSpecializationIdAndNames() => GetAll().ToDictionary(s => s.Id, s => s.Name);
    }
}