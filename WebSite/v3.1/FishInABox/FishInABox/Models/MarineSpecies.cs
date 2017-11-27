using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FishInABox.Models
{
    [Table("MARINE_SPECIES")]
    public partial class MarineSpecies
    {
        public MarineSpecies()
        {
            RecordPet = new HashSet<RecordPet>();
            ShipmentItem = new HashSet<ShipmentItem>();
            TankLog = new HashSet<TankLog>();
        }

        [Key]
        [Column("ID_PK")]
        public int IdPk { get; set; }
        [Column("CLASS_FK")]
        public int ClassFk { get; set; }
        [Column("SPECIES_FK")]
        public int SpeciesFk { get; set; }
        [Required]
        [Column("SCIENTIFIC")]
        [StringLength(40)]
        public string Scientific { get; set; }
        [Required]
        [Column("COMMON")]
        [StringLength(80)]
        public string Common { get; set; }
        [Column("TEXT")]
        [StringLength(50)]
        public string Text { get; set; }
        [Column("FLAG")]
        public bool Flag { get; set; }
        [Column("FAMILY_FK")]
        public int? FamilyFk { get; set; }

        [ForeignKey("ClassFk")]
        [InverseProperty("MarineSpecies")]
        public MarineClass ClassFkNavigation { get; set; }
        [ForeignKey("FamilyFk")]
        [InverseProperty("MarineSpecies")]
        public MarineFamily FamilyFkNavigation { get; set; }
        [InverseProperty("SpeciesFkNavigation")]
        public ICollection<RecordPet> RecordPet { get; set; }
        [InverseProperty("SpeciesFkNavigation")]
        public ICollection<ShipmentItem> ShipmentItem { get; set; }
        [InverseProperty("SpeciesFkNavigation")]
        public ICollection<TankLog> TankLog { get; set; }
    }
}
