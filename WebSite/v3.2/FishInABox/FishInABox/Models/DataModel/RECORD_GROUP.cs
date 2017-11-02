using System;
using System.Collections.Generic;

namespace FishInABox.Models.DataModel
{
    public partial class RECORD_GROUP
    {
        public RECORD_GROUP()
        {
            RECORD_PET = new HashSet<RECORD_PET>();
        }

        public int IdPk { get; set; }
        public string Description { get; set; }

        public ICollection<RECORD_PET> RECORD_PET { get; set; }
    }
}
