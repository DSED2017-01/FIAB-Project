using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FishInABox.Models
{
    [Table("SHIPMENT")]
    public partial class Shipment
    {
        public Shipment()
        {
            ShipmentOrder = new HashSet<ShipmentOrder>();
        }

        [Key]
        [Column("ID_PK")]
        public int IdPk { get; set; }
        [Column("TIMESTAMP", TypeName = "datetime")]
        public DateTime? Timestamp { get; set; }
        [Column("EXPORT_FK")]
        public int? ExportFk { get; set; }
        [Column("ETD", TypeName = "datetime")]
        public DateTime? Etd { get; set; }
        [Column("ETA", TypeName = "datetime")]
        public DateTime? Eta { get; set; }
        [Column("COMMENT")]
        [StringLength(100)]
        public string Comment { get; set; }

        [InverseProperty("ShipmentFkNavigation")]
        public ICollection<ShipmentOrder> ShipmentOrder { get; set; }
    }
}
