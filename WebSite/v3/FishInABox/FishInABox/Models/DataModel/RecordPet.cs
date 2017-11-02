using System;
using System.Collections.Generic;

namespace FishInABox.Models.DataModel
{
    public partial class RecordPet
    {
        public int IdPk { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public int? SpeciesFk { get; set; }
        public int SizeFk { get; set; }
        public int? GroupFk { get; set; }

        public RecordGroup GroupFkNavigation { get; set; }
        public RecordPetSize SizeFkNavigation { get; set; }
        public MarineSpecies SpeciesFkNavigation { get; set; }
    }
}
