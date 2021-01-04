
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using OnlineClinic.Core.Services;
using System.Threading.Tasks;
using OnlineClinic.Core.ValidationAttributes;
using System;

namespace OnlineClinic.Core.DTOs
{
    public class SpecializationEditDto: IValidatableObject
    {
        private ISpecializationService _specializationService;

        public SpecializationEditDto(ISpecializationService specializationService)
        {
            _specializationService = specializationService;
        }

        public SpecializationEditDto() { }

        public void SetSpecializationService(ISpecializationService specializationService) => _specializationService = specializationService;

        [Required]
        public int Id { get; set; }

        [Required]
        [MinLength(5)]
        [MaxLength(30)]
        public string Name { get; set; }

        [Required]
        [MinLength(10)]
        [MaxLength(100)]
        public string Description { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime? DateModified { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();
            
            Validator.TryValidateProperty(Name, new ValidationContext(this, null, null) { MemberName = "Name"}, results);
            Validator.TryValidateProperty(Description, new ValidationContext(this, null, null) { MemberName = "Description"}, results);

            if(!results.Any())
            {
                if(_specializationService.IsNameAlreadyUsed(Id, Name))
                    results.Add(new ValidationResult($"Specialization ({this.Name}) is already used.", new List<string> { "Name" }));
            }
            return results;
        }

        public async Task Save() => await _specializationService.UpdateAsync(this);
    }
}