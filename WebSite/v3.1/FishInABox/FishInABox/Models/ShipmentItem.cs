using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FishInABox.Models
{
    [Table("SHIPMENT_ITEM")]
    public partial class ShipmentItem
    {
        [Key]
        [Column("ID_PK")]
        public int IdPk { get; set; }
        [Column("RECORD_FK")]
        public int RecordFk { get; set; }
        [Column("SPECIES_FK")]
        public int SpeciesFk { get; set; }
        [Column("SIZE_FK")]
        public int SizeFk { get; set; }
        [Column("SPECIES_TEXT")]
        [StringLength(100)]
        public string SpeciesText { get; set; }
        [Column("SPECIES_TEXT_2")]
        [StringLength(100)]
        public string SpeciesText2 { get; set; }
        [Column("TEXT")]
        [StringLength(20)]
        public string Text { get; set; }
        [Column("QUANTITY")]
        public int Quantity { get; set; }
        [Column("QUARANTINE_FK")]
        public int? QuarantineFk { get; set; }

        [ForeignKey("RecordFk")]
        [InverseProperty("ShipmentItem")]
        public ShipmentOrder RecordFkNavigation { get; set; }
        [ForeignKey("SizeFk")]
        [InverseProperty("ShipmentItem")]
        public RecordPetSize SizeFkNavigation { get; set; }
        [ForeignKey("SpeciesFk")]
        [InverseProperty("ShipmentItem")]
        public MarineSpecies SpeciesFkNavigation { get; set; }
    }
}
