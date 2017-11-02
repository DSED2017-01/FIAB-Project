using System;
using System.Collections.Generic;

namespace FishInABox.Models.DataModel
{
    public partial class MARINE_CLASS
    {
        public MARINE_CLASS()
        {
            MARINE_SPECIES = new HashSet<MARINE_SPECIES>();
        }

        public int IdPk { get; set; }
        public string Text { get; set; }
        public string Schedule4 { get; set; }
        public bool? Flag { get; set; }

        public ICollection<MARINE_SPECIES> MARINE_SPECIES { get; set; }
    }
}
