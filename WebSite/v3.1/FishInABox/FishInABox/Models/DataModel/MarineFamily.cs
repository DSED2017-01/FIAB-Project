using System;
using System.Collections.Generic;

namespace FishInABox.Models.DataModel
{
    public partial class MarineFamily
    {
        public MarineFamily()
        {
            MarineSpecies = new HashSet<MarineSpecies>();
        }

        public int IdPk { get; set; }
        public string Text { get; set; }
        public string Schedule3 { get; set; }
        public bool? Flag { get; set; }

        public ICollection<MarineSpecies> MarineSpecies { get; set; }
    }
}
