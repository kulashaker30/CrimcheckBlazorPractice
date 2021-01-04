using System;

namespace OnlineClinic.Core.DTOs
{
    public class DoctorGetDto
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string DoctorFullname { get; set; }

        public string SpecializationName { get; set; }

        public int PersonId { get; set; }

        public int SpecializationId { get; set; }
    }
}