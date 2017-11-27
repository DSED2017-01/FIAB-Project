using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FishInABox.Models
{
    [Table("RECORD_GROUP")]
    public partial class RecordGroup
    {
        public RecordGroup()
        {
            RecordPet = new HashSet<RecordPet>();
        }

        [Key]
        [Column("ID_PK")]
        public int IdPk { get; set; }
        [Required]
        [Column("DESCRIPTION")]
        [StringLength(25)]
        public string Description { get; set; }

        [InverseProperty("GroupFkNavigation")]
        public ICollection<RecordPet> RecordPet { get; set; }
    }
}
