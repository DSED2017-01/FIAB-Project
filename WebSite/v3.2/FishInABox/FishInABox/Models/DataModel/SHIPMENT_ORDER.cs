using System;
using System.Collections.Generic;

namespace FishInABox.Models.DataModel
{
    public partial class SHIPMENT_ORDER
    {
        public SHIPMENT_ORDER()
        {
            SHIPMENT_ITEM = new HashSet<SHIPMENT_ITEM>();
        }

        public int IdPk { get; set; }
        public DateTime? Timestamp { get; set; }
        public int ShipmentFk { get; set; }
        public string Text { get; set; }

        public SHIPMENT ShipmentFkNavigation { get; set; }
        public ICollection<SHIPMENT_ITEM> SHIPMENT_ITEM { get; set; }
    }
}
