using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FishInABox.Models
{
    [Table("RECORD_PACKING")]
    public partial class RecordPacking
    {
        [Key]
        [Column("ID_PK")]
        public int IdPk { get; set; }
        [Required]
        [Column("DESCRIPTION")]
        [StringLength(25)]
        public string Description { get; set; }
    }
}
