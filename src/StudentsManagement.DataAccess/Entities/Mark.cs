﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentsManagement.DataAccess.Entities
{
    [Table("Marks")]
    public class Mark : BaseEntity
    {
        [Required]
        public Guid StudentId { get; set; }

        [Required]
        public Guid AttestationId { get; set; }

        public int? Value { get; set; }

        public bool? NotAttended { get; set; }

        public bool? NotAllowed { get; set; }
    }
}
