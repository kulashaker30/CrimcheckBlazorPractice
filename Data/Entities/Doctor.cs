
using System;
using System.Collections.Generic;

namespace OnlineClinic.Data.Entities
{
    public class Doctor: Entity
    {
        public string Title { get; set; }

        public virtual int PersonId { get; set; }

        public virtual int SpecializationId { get; set; }

        public string EmailAddress { get; set; }

        public string EncryptedPassword { get; set; }

        public virtual Person Person { get; set; }

        public virtual Specialization Specialization { get; set; }
    }
}