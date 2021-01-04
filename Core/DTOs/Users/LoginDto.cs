
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using OnlineClinic.Core.Services;

namespace OnlineClinic.Core.DTOs
{
    public class LoginDto: IValidatableObject
    {
        private readonly IUserService _userService;
        
        public LoginDto(IUserService userService) => _userService = userService;

        [EmailAddress]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        public bool IsAdmin { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();
            
            Validator.TryValidateProperty(Username, new ValidationContext(this, null, null) { MemberName = "Username"}, results);
            Validator.TryValidateProperty(Password, new ValidationContext(this, null, null) { MemberName = "Password"}, results);

            if(!results.Any())
            {
                if(!_userService.Authenticate(this.Username, this.Password, this.IsAdmin))
                    results.Add(new ValidationResult("Invalid username or password."));
            }

            return results;
        }
    }
}