using System;
using System.Collections.Generic;

namespace FishInABox.Models.DataModel
{
    public partial class MarineSpecies
    {
        public MarineSpecies()
        {
            RecordPet = new HashSet<RecordPet>();
            ShipmentItem = new HashSet<ShipmentItem>();
            TankLog = new HashSet<TankLog>();
        }

        public int IdPk { get; set; }
        public int ClassFk { get; set; }
        public int SpeciesFk { get; set; }
        public string Scientific { get; set; }
        public string Common { get; set; }
        public string Text { get; set; }
        public bool? Flag { get; set; }
        public int? FamilyFk { get; set; }

        public MarineClass ClassFkNavigation { get; set; }
        public MarineFamily FamilyFkNavigation { get; set; }
        public ICollection<RecordPet> RecordPet { get; set; }
        public ICollection<ShipmentItem> ShipmentItem { get; set; }
        public ICollection<TankLog> TankLog { get; set; }
    }
}
