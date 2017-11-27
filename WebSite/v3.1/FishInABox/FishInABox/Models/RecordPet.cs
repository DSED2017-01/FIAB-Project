using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FishInABox.Models
{
    [Table("RECORD_PET")]
    public partial class RecordPet
    {
        [Key]
        [Column("ID_PK")]
        public int IdPk { get; set; }
        [Required]
        [Column("CODE")]
        [StringLength(10)]
        public string Code { get; set; }
        [Column("DESCRIPTION")]
        [StringLength(100)]
        public string Description { get; set; }
        [Column("SPECIES_FK")]
        public int? SpeciesFk { get; set; }
        [Column("SIZE_FK")]
        public int SizeFk { get; set; }
        [Column("GROUP_FK")]
        public int? GroupFk { get; set; }

        [ForeignKey("GroupFk")]
        [InverseProperty("RecordPet")]
        public RecordGroup GroupFkNavigation { get; set; }
        [ForeignKey("SizeFk")]
        [InverseProperty("RecordPet")]
        public RecordPetSize SizeFkNavigation { get; set; }
        [ForeignKey("SpeciesFk")]
        [InverseProperty("RecordPet")]
        public MarineSpecies SpeciesFkNavigation { get; set; }
    }
}
