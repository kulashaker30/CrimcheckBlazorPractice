using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using OnlineClinic.Core.Services;
using OnlineClinic.Core.ValidationAttributes;
using System.Threading.Tasks;


namespace OnlineClinic.Core.DTOs
{
    public class PersonEditDto : IValidatableObject
    {
        protected IPersonService _personService;

        public PersonEditDto(IPersonService personService) { }

        public PersonEditDto() { }

        public void SetPersonService(IPersonService personService) => _personService = personService;

        [Required]
        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string MiddleName { get; set ;}

        [Required]
        public string LastName { get; set; }
        
        public DateTime DOB { get; set; } = DateTime.UtcNow;

        [Required]
        [ValidDate("Date Of Birth")]
        public string DOBString { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string ZipCode { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime? DateModified { get; set; }

        public async Task Save() => await _personService.UpdateAsync(this);

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();
            
            Validator.TryValidateProperty(FirstName, new ValidationContext(this, null, null) { MemberName = "FirstName"}, results);
            Validator.TryValidateProperty(MiddleName, new ValidationContext(this, null, null) { MemberName = "MiddleName"}, results);
            Validator.TryValidateProperty(LastName, new ValidationContext(this, null, null) { MemberName = "LastName"}, results);
            Validator.TryValidateProperty(DOBString, new ValidationContext(this, null, null) { MemberName = "DOBString"}, results);
            Validator.TryValidateProperty(Address, new ValidationContext(this, null, null) { MemberName = "Address"}, results);
            Validator.TryValidateProperty(City, new ValidationContext(this, null, null) { MemberName = "City"}, results);
            Validator.TryValidateProperty(ZipCode, new ValidationContext(this, null, null) { MemberName = "ZipCode"}, results);

            if(!results.Any())
            {
                DOB = DateTime.Parse(DOBString);

                if(_personService.Exists(Id, FirstName, MiddleName, LastName, DateTime.Parse(DOBString), Address))
                    results.Add(new ValidationResult("Person already exists", new string[] { "FirstName", "MiddleName", "LastName", "DOBString", "Address" }));
            }

            return results;
        }
    }
}