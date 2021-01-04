using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OnlineClinic.Core.DTOs;

namespace OnlineClinic.Core.Services
{
    public interface IPersonService
    {
        Task<int> Create(PersonCreateDto createDto);

        bool Exists(int id);

        bool Exists(string firstName, string middleName, string lastName, DateTime dob, string address);

        bool Exists(int id, string firstName, string middleName, string lastName, DateTime dob, string address);

        Task UpdateAsync(PersonEditDto editDto);

        IEnumerable<PersonGetDto> GetAll(bool asNoTracking = true);

        PersonGetDto GetById(int id);

        Dictionary<int, string> GetPersonIdAndNames();
    }
}