using System;
using System.Collections.Generic;

namespace FishInABox.Models.DataModel
{
    public partial class RECORD_PET_SIZE
    {
        public RECORD_PET_SIZE()
        {
            RECORD_PET = new HashSet<RECORD_PET>();
            SHIPMENT_ITEM = new HashSet<SHIPMENT_ITEM>();
            TANK_LOG = new HashSet<TANK_LOG>();
        }

        public int IdPk { get; set; }
        public string Description { get; set; }

        public ICollection<RECORD_PET> RECORD_PET { get; set; }
        public ICollection<SHIPMENT_ITEM> SHIPMENT_ITEM { get; set; }
        public ICollection<TANK_LOG> TANK_LOG { get; set; }
    }
}
