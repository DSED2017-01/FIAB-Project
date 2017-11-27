using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FishInABox.Models
{
    [Table("MARINE_CLASS")]
    public partial class MarineClass
    {
        public MarineClass()
        {
            MarineSpecies = new HashSet<MarineSpecies>();
        }

        [Key]
        [Column("ID_PK")]
        public int IdPk { get; set; }
        [Column("TEXT")]
        [StringLength(40)]
        public string Text { get; set; }
        [Required]
        [Column("SCHEDULE4")]
        [StringLength(40)]
        public string Schedule4 { get; set; }
        [Column("FLAG")]
        public bool Flag { get; set; }

        [InverseProperty("ClassFkNavigation")]
        public ICollection<MarineSpecies> MarineSpecies { get; set; }
    }
}
