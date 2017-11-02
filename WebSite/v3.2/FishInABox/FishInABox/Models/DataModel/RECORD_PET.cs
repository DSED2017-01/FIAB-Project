using System;
using System.Collections.Generic;

namespace FishInABox.Models.DataModel
{
    public partial class RECORD_PET
    {
        public int IdPk { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public int? SpeciesFk { get; set; }
        public int SizeFk { get; set; }
        public int? GroupFk { get; set; }

        public RECORD_GROUP GroupFkNavigation { get; set; }
        public RECORD_PET_SIZE SizeFkNavigation { get; set; }
        public MARINE_SPECIES SpeciesFkNavigation { get; set; }
    }
}
