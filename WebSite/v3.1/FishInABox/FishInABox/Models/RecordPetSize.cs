using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FishInABox.Models
{
    [Table("RECORD_PET_SIZE")]
    public partial class RecordPetSize
    {
        public RecordPetSize()
        {
            RecordPet = new HashSet<RecordPet>();
            ShipmentItem = new HashSet<ShipmentItem>();
            TankLog = new HashSet<TankLog>();
        }

        [Key]
        [Column("ID_PK")]
        public int IdPk { get; set; }
        [Required]
        [Column("DESCRIPTION")]
        [StringLength(25)]
        public string Description { get; set; }

        [InverseProperty("SizeFkNavigation")]
        public ICollection<RecordPet> RecordPet { get; set; }
        [InverseProperty("SizeFkNavigation")]
        public ICollection<ShipmentItem> ShipmentItem { get; set; }
        [InverseProperty("SizeFkNavigation")]
        public ICollection<TankLog> TankLog { get; set; }
    }
}
