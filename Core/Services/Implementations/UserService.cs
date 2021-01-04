using System.Linq;
using System.Collections.Generic;

using AutoMapper;

using OnlineClinic.Core.DTOs;
using OnlineClinic.Data.Entities;
using OnlineClinic.Data;
using OnlineClinic.Data.Repositories;
using System.Threading.Tasks;
using OnlineClinic.Core.Utilities;

namespace OnlineClinic.Core.Services
{
    public class UserService : IUserService
    {
        private readonly IDoctorRepository _doctorRepository;

        private readonly IPasswordEncryptionUtil _passwordEncyptionUtill;

        private readonly IMapper _mapper;

        public UserService(
            IDoctorRepository doctorRepository,
            IPasswordEncryptionUtil passwordEncryptionUtil,
            IMapper mapper
        )
        {
            _doctorRepository = doctorRepository;
            _passwordEncyptionUtill = passwordEncryptionUtil;
            _mapper = mapper;
        }

        public bool Authenticate(string username, string password, bool AdminUser = false)
        {
            if(!AdminUser)
            {
                var doctorUserCredentialDto = _mapper.Map<DoctorUserCredentialDto>(_doctorRepository.GetAll().FirstOrDefault(d => d.EmailAddress == username));
                
                if(doctorUserCredentialDto is null)
                    return false;

                if(_passwordEncyptionUtill.Decrypt(doctorUserCredentialDto.EncryptedPassword) != password)
                    return false;
            }

            return true;
        }

        public bool IsValidUser(string username) => _doctorRepository.GetAll().Any(d => d.EmailAddress == username);
    }
}