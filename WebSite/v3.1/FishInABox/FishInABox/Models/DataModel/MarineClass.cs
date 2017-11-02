using System;
using System.Collections.Generic;

namespace FishInABox.Models.DataModel
{
    public partial class MarineClass
    {
        public MarineClass()
        {
            MarineSpecies = new HashSet<MarineSpecies>();
        }

        public int IdPk { get; set; }
        public string Text { get; set; }
        public string Schedule4 { get; set; }
        public bool? Flag { get; set; }

        public ICollection<MarineSpecies> MarineSpecies { get; set; }
    }
}
