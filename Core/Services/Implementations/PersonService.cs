using System.Linq;
using System.Collections.Generic;

using AutoMapper;

using OnlineClinic.Core.DTOs;
using OnlineClinic.Data.Entities;
using OnlineClinic.Data.Repositories;
using System.Threading.Tasks;
using System;

namespace OnlineClinic.Core.Services
{
    public class PersonService : GenericService<Person>, IPersonService
    {
        public PersonService(
            IPersonRepository repository,
            IMapper mapper) : base(repository, mapper) { }


        public Task<int> Create(PersonCreateDto createDto) => _repository.CreateAsync(_mapper.Map<Person>(createDto));

        public bool Exists(string firstName, string middleName, string lastName, DateTime dob, string address) => _repository.GetAll().Any(p => $"{($"{p.FirstName}{p.MiddleName}{p.LastName}{p.DOB}{p.Address}").ToLower()}" == $"{($"{firstName}{middleName}{lastName}{dob}{address}").ToLower()}");

        public bool Exists(int id, string firstName, string middleName, string lastName, DateTime dob, string address) => _repository.GetAll().Any(p => p.Id != id && $"{($"{p.FirstName}{p.MiddleName}{p.LastName}{p.DOB}{p.Address}").ToLower()}" == $"{($"{firstName}{middleName}{lastName}{dob}{address}").ToLower()}");

        public IEnumerable<PersonGetDto> GetAll(bool asNoTracking = true) => _mapper.Map<IEnumerable<PersonGetDto>>(_repository.GetAll(asNoTracking));

        public PersonGetDto GetById(int id) => _mapper.Map<PersonGetDto>(_repository.GetById(id));

        public Dictionary<int, string> GetPersonIdAndNames() => ((IPersonRepository)_repository).GetPersonIdAndNames();

        public Task UpdateAsync(PersonEditDto editDto) => _repository.UpdateAsync(_mapper.Map<Person>(editDto));
    }
}