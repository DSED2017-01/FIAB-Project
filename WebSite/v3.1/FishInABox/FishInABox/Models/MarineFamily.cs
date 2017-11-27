using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FishInABox.Models
{
    [Table("MARINE_FAMILY")]
    public partial class MarineFamily
    {
        public MarineFamily()
        {
            MarineSpecies = new HashSet<MarineSpecies>();
        }

        [Key]
        [Column("ID_PK")]
        public int IdPk { get; set; }
        [Column("TEXT")]
        [StringLength(25)]
        public string Text { get; set; }
        [Required]
        [Column("SCHEDULE3")]
        [StringLength(25)]
        public string Schedule3 { get; set; }
        [Column("FLAG")]
        public bool Flag { get; set; }

        [InverseProperty("FamilyFkNavigation")]
        public ICollection<MarineSpecies> MarineSpecies { get; set; }
    }
}
