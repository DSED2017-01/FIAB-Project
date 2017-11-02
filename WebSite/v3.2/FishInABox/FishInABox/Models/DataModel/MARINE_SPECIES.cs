using System;
using System.Collections.Generic;

namespace FishInABox.Models.DataModel
{
    public partial class MARINE_SPECIES
    {
        public MARINE_SPECIES()
        {
            RECORD_PET = new HashSet<RECORD_PET>();
            SHIPMENT_ITEM = new HashSet<SHIPMENT_ITEM>();
            TANK_LOG = new HashSet<TANK_LOG>();
        }

        public int IdPk { get; set; }
        public int ClassFk { get; set; }
        public int SpeciesFk { get; set; }
        public string Scientific { get; set; }
        public string Common { get; set; }
        public string Text { get; set; }
        public bool? Flag { get; set; }
        public int? FamilyFk { get; set; }

        public MARINE_CLASS ClassFkNavigation { get; set; }
        public MARINE_FAMILY FamilyFkNavigation { get; set; }
        public ICollection<RECORD_PET> RECORD_PET { get; set; }
        public ICollection<SHIPMENT_ITEM> SHIPMENT_ITEM { get; set; }
        public ICollection<TANK_LOG> TANK_LOG { get; set; }
    }
}
