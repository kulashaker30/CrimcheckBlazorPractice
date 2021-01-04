
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineClinic.Data.Entities
{
    public class Person: Entity
    {
        public string FirstName { get; set; }

        public string MiddleName { get; set ;}

        public string LastName { get; set; }

        public DateTime DOB { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public string ZipCode { get; set; }

        [NotMapped]

        public string Fullname { get => $"{FirstName} {MiddleName.Substring(0, 1)}. {LastName}"; }

        public virtual ICollection<Doctor> Doctors { get; set; }
    }
}