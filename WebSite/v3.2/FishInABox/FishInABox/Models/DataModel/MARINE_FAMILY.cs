using System;
using System.Collections.Generic;

namespace FishInABox.Models.DataModel
{
    public partial class MARINE_FAMILY
    {
        public MARINE_FAMILY()
        {
            MARINE_SPECIES = new HashSet<MARINE_SPECIES>();
        }

        public int IdPk { get; set; }
        public string Text { get; set; }
        public string Schedule3 { get; set; }
        public bool? Flag { get; set; }

        public ICollection<MARINE_SPECIES> MARINE_SPECIES { get; set; }
    }
}
