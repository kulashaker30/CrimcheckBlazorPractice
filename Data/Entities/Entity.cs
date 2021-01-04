using System;

namespace OnlineClinic.Data.Entities
{
    public class Entity
    {
        public int Id { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime? DateModified { get; set; }
    }
}