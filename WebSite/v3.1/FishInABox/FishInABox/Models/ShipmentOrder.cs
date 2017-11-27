using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FishInABox.Models
{
    [Table("SHIPMENT_ORDER")]
    public partial class ShipmentOrder
    {
        public ShipmentOrder()
        {
            ShipmentItem = new HashSet<ShipmentItem>();
        }

        [Key]
        [Column("ID_PK")]
        public int IdPk { get; set; }
        [Column("TIMESTAMP", TypeName = "datetime")]
        public DateTime? Timestamp { get; set; }
        [Column("SHIPMENT_FK")]
        public int ShipmentFk { get; set; }
        [Column("TEXT")]
        [StringLength(20)]
        public string Text { get; set; }

        [ForeignKey("ShipmentFk")]
        [InverseProperty("ShipmentOrder")]
        public Shipment ShipmentFkNavigation { get; set; }
        [InverseProperty("RecordFkNavigation")]
        public ICollection<ShipmentItem> ShipmentItem { get; set; }
    }
}
