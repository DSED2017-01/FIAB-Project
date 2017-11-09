using System;
using System.Collections.Generic;

namespace FishInABox.Models.DataModel
{
    public partial class ShipmentItem
    {
        public int IdPk { get; set; }
        public int RecordFk { get; set; }
        public int SpeciesFk { get; set; }
        public int SizeFk { get; set; }
        public string SpeciesText { get; set; }
        public string SpeciesText2 { get; set; }
        public string Text { get; set; }
        public int Quantity { get; set; }
        public int? QuarantineFk { get; set; }

        public ShipmentOrder RecordFkNavigation { get; set; }
        public RecordPetSize SizeFkNavigation { get; set; }
        public MarineSpecies SpeciesFkNavigation { get; set; }
    }
}
