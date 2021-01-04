using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using OnlineClinic.Core.Services;
using System.Threading.Tasks;
using OnlineClinic.Core.Utilities;

namespace OnlineClinic.Core.DTOs
{
    public class DoctorCreateDto : IValidatableObject
    {
        private IDoctorService _doctorService;

        private IPersonService _personService;

        private ISpecializationService _specializationService;

        private IPasswordEncryptionUtil _passwordEncryptionUtil;

        public DoctorCreateDto(
            IDoctorService doctorService,
            IPersonService personService,
            ISpecializationService specializationService,
            IPasswordEncryptionUtil passwordEncryptionUtil)
        {
            _doctorService = doctorService;
            _personService = personService;
            _specializationService = specializationService;
            _passwordEncryptionUtil = passwordEncryptionUtil;
        }

        public DoctorCreateDto() { }

        public void SetPersonService(IPersonService personService) => _personService = personService;
        
        public void SetSpecializationService(ISpecializationService specializationService) => _specializationService = specializationService;

        public int PersonId { get; set; }

        public int SpecializationId { get; set; }

        [Required]
        public string Title { get; set; }

        [EmailAddress]
        public string EmailAddress { get; set; }

        [Required]
        [MinLength(8)]
        public string Password { get; set; }

        [Required]
        public string ConfirmPassword { get; set; }

        public string EncryptedPassword { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();
            
            Validator.TryValidateProperty(Title, new ValidationContext(this, null, null) { MemberName = "Title"}, results);
            Validator.TryValidateProperty(EmailAddress, new ValidationContext(this, null, null) { MemberName = "EmailAddress"}, results);
            Validator.TryValidateProperty(Password, new ValidationContext(this, null, null) { MemberName = "Password"}, results);
            Validator.TryValidateProperty(ConfirmPassword, new ValidationContext(this, null, null) { MemberName = "ConfirmPassword"}, results);

            if(!results.Any())
            {
                if(Password != ConfirmPassword)
                    results.Add(new ValidationResult("Password and Confirm Password did not match.", new string[] { "ConfirmPassword"}));

                if(!_specializationService.Exists(SpecializationId))
                    results.Add(new ValidationResult("Specialization does not exist.", new string[] { "SpecializationId"}));

                if(!_personService.Exists(PersonId))
                    results.Add(new ValidationResult("Person does not exist.", new string[] { "PersonId"}));

                if(!results.Any())
                {
                    if(_doctorService.PersonExists(PersonId))
                        results.Add(new ValidationResult("The person has already been added as a doctor.", new string[] { "PersonId"}));
                }

            }

            EncryptedPassword = _passwordEncryptionUtil.Encrypt(Password);
            
            return results;
        }

        public async Task<int> Save() => await _doctorService.CreateAsync(this);
    }
}