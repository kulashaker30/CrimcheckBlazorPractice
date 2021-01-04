using System;

namespace OnlineClinic.Core.DTOs
{
    public class PersonGetDto
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set ;}

        public string LastName { get; set; }
        
        public DateTime DOB { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public string ZipCode { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime? DateModified { get; set; }

        public string Fullname  { get => $"{FirstName} {MiddleName.Substring(0, 1)}. {LastName}"; }
    }
}