
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using OnlineClinic.Core.Services;

namespace OnlineClinic.Core.DTOs
{
    public class SpecializationCreateDto: IValidatableObject
    {
        private readonly ISpecializationService _specializationService;

        public SpecializationCreateDto(ISpecializationService specializationService)
        {
            _specializationService = specializationService;
        }

        [Required]
        [MinLength(5)]
        [MaxLength(30)]
        public string Name { get; set; }

        [Required]
        [MinLength(10)]
        [MaxLength(100)]
        public string Description { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();
            
            Validator.TryValidateProperty(this.Name, new ValidationContext(this, null, null) { MemberName = "Name"}, results);
            Validator.TryValidateProperty(this.Description, new ValidationContext(this, null, null) { MemberName = "Description"}, results);

            if(!results.Any())
            {
                if(_specializationService.Exists(this.Name))
                    results.Add(new ValidationResult($"Specialization ({this.Name}) already exists.", new List<string> { "Name" }));

            }
            return results;
        }

        public int Save() => _specializationService.Create(this);
    }
}