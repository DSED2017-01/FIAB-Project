using System;
using System.Collections.Generic;

namespace FishInABox.Models.DataModel
{
    public partial class RecordPetSize
    {
        public RecordPetSize()
        {
            RecordPet = new HashSet<RecordPet>();
            ShipmentItem = new HashSet<ShipmentItem>();
            TankLog = new HashSet<TankLog>();
        }

        public int IdPk { get; set; }
        public string Description { get; set; }

        public ICollection<RecordPet> RecordPet { get; set; }
        public ICollection<ShipmentItem> ShipmentItem { get; set; }
        public ICollection<TankLog> TankLog { get; set; }
    }
}
