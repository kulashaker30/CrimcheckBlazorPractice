using System;
namespace OnlineClinic.Core.DTOs
{
    public class SpecializationGetDto
    {
        public int Id { get; set; }  

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime? DateModified { get; set; }
    }
}