using System;
using System.Collections.Generic;

namespace FishInABox.Models.DataModel
{
    public partial class SHIPMENT
    {
        public SHIPMENT()
        {
            SHIPMENT_ORDER = new HashSet<SHIPMENT_ORDER>();
        }

        public int IdPk { get; set; }
        public DateTime? Timestamp { get; set; }
        public int? ExportFk { get; set; }
        public DateTime? Etd { get; set; }
        public DateTime? Eta { get; set; }
        public string Comment { get; set; }

        public ICollection<SHIPMENT_ORDER> SHIPMENT_ORDER { get; set; }
    }
}
