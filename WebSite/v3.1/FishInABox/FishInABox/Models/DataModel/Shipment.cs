using System;
using System.Collections.Generic;

namespace FishInABox.Models.DataModel
{
    public partial class Shipment
    {
        public Shipment()
        {
            ShipmentOrder = new HashSet<ShipmentOrder>();
        }

        public int IdPk { get; set; }
        public DateTime? Timestamp { get; set; }
        public int? ExportFk { get; set; }
        public DateTime? Etd { get; set; }
        public DateTime? Eta { get; set; }
        public string Comment { get; set; }

        public ICollection<ShipmentOrder> ShipmentOrder { get; set; }
    }
}
