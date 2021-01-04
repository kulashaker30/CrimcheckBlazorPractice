using System.Collections.Generic;

namespace OnlineClinic.Core.DTOs
{
    public class ValidationError
    {
        public string Property { get; set; }

        public List<string> ErrorMessages { get; set; }
    }
}