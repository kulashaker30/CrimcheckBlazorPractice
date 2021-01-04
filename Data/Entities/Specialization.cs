
using System;
using System.Collections.Generic;

namespace OnlineClinic.Data.Entities
{
    public class Specialization: Entity
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public virtual ICollection<Doctor> Doctors { get; set; }
    }
}