using System;
using System.Collections.Generic;

namespace FishInABox.Models.DataModel
{
    public partial class ShipmentOrder
    {
        public ShipmentOrder()
        {
            ShipmentItem = new HashSet<ShipmentItem>();
        }

        public int IdPk { get; set; }
        public DateTime? Timestamp { get; set; }
        public int ShipmentFk { get; set; }
        public string Text { get; set; }

        public Shipment ShipmentFkNavigation { get; set; }
        public ICollection<ShipmentItem> ShipmentItem { get; set; }
    }
}
